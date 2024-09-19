"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

// Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

function BindOtherUserMessage(user, message) {
    // Create a new list item (li)
    var li = document.createElement("li");
    li.classList.add("p-1", "flex", "items-center", "my-1");

    // Add user avatar
    var img = document.createElement("img");
    img.src = user.pictureAddress;
    img.alt = "user image";
    img.classList.add("rounded-full", "max-w-14", "max-h-14", "me-3");
    img.loading = "lazy";

    // Create a container for the message
    var messageContainer = document.createElement("div");
    messageContainer.classList.add("bg-blue-900", "text-white", "w-fit", "p-2", "rounded-md", "flex", "flex-col");

    // Create and append the user element
    var userSpan = document.createElement("span");
    userSpan.classList.add("text-blue-300", "text-sm");
    userSpan.textContent = user.username;
    messageContainer.appendChild(userSpan);

    // Create and append the message element
    var messageSpan = document.createElement("span");
    messageSpan.classList.add("text-lg");
    messageSpan.textContent = message;  // Use textContent to ensure HTML isn't executed
    messageContainer.appendChild(messageSpan);

    // Append the image and message container to the list item
    li.appendChild(img);
    li.appendChild(messageContainer);

    // Append the list item to the messages list
    document.getElementById("messagesList").appendChild(li);
}
function BindCurrentUserMessage(user, message) {
    // Create a new list item (li)
    var li = document.createElement("li");
    li.classList.add("p-1", "flex", "items-center", "my-1", "w-full", "justify-end", "p-2");

    // Create a container for the message
    var messageContainer = document.createElement("div");
    messageContainer.classList.add("bg-blue-700", "text-white", "w-fit", "p-2", "rounded-md", "flex", "flex-col");

    // Create and append the user element
    var userSpan = document.createElement("span");
    userSpan.classList.add("text-blue-300", "text-sm");
    userSpan.textContent = user.username;  // Use textContent to avoid injecting HTML
    messageContainer.appendChild(userSpan);

    // Create and append the message element
    var messageSpan = document.createElement("span");
    messageSpan.classList.add("text-lg");
    messageSpan.textContent = message;  // Use textContent to ensure HTML isn't executed
    messageContainer.appendChild(messageSpan);

    // Append the image and message container to the list item
    li.appendChild(messageContainer);

    document.getElementById("messagesList").appendChild(li);
}

connection.on("ReceiveMessage", function (user, message) {
    // Fetch the UserToken cookie value using fetch
    fetch('/api/v1/Cookies/GetCookie?cookieName=UserToken', {
        method: 'GET',
        credentials: 'include' // Ensure cookies are sent with the request
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.text(); // Use .text() since it's not a JSON response
        })
        .then(userPublicId => {
            if (userPublicId == user.publicId) {
                BindCurrentUserMessage(user, message);
            } else {
                BindOtherUserMessage(user, message);
            }
        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    SendMessage(event);
});

// TODO: add this block of code when you want to customize the send message with enter or not in setting
//document.getElementById("messageInput").addEventListener("keypress", function (event) {
//    if (event.key === "Enter") {
//        SendMessage(event);
//    }
//});

function SendMessage(event) {
    try {
        if (document.getElementById("noMessageBox").classList.contains("flex"))
            document.getElementById("noMessageBox").classList.add("hidden");
    } catch (e) { }

    // Fetch the UserToken cookie value using fetch
    fetch('/api/v1/Cookies/GetCookie?cookieName=UserToken', {
        method: 'GET',
        credentials: 'include' // Ensure cookies are sent with the request
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.text(); // Use .text() since it's not a JSON response
        })
        .then(userPublicId => {
            // Now that we have userPublicId, fetch the user information
            return fetch('/api/v1/Users/GetUserByPublicId/' + userPublicId, {
                method: 'GET',
                credentials: 'include' // Ensure cookies are sent with the request
            });
        })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json(); // Parse the response as JSON
        })
        .then(userData => {
            var message = document.getElementById("messageInput").value;
            document.getElementById("messageInput").value = "";

            connection.invoke("SendMessage", userData, message).catch(function (err) {
                return console.error(err.toString());
            });

            SaveMessageToDB(message);
        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });

    event.preventDefault();
}

function SaveMessageToDB(userMessageContent) {
    var userMessage = {
        content: userMessageContent
    };

    $.ajax({
        url: "/api/v1/messages",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(userMessage)
    });
}
