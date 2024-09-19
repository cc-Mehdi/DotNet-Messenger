"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

// Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    // Create a new list item (li)
    var li = document.createElement("li");
    li.classList.add("p-1", "flex", "items-center", "my-1");

    // Add user avatar
    var img = document.createElement("img");
    img.src = "https://avatars.githubusercontent.com/u/57840939?v=4";
    img.alt = "user image";
    img.classList.add("rounded-full", "max-w-14", "max-h-14", "me-3");
    img.loading = "lazy"; // Lazy loading attribute

    // Create a container for the message
    var messageContainer = document.createElement("div");
    messageContainer.classList.add("bg-blue-900", "text-white", "w-fit", "p-2", "rounded-md", "flex", "flex-col");

    // Create and append the user element
    var userSpan = document.createElement("span");
    userSpan.classList.add("text-blue-300", "text-sm");
    userSpan.textContent = user;  // Use textContent to avoid injecting HTML
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
    debugger;
    if (document.getElementById("noMessageBox").classList.contains("flex"))
        document.getElementById("noMessageBox").classList.add("hidden");


    var user = "Mehdi Gholami";
    var message = document.getElementById("messageInput").value;
    document.getElementById("messageInput").value = "";

    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
}
