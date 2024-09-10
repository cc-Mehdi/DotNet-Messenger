
# DotNet-Messenger: A Real-Time Messenger with ASP.NET Core SignalR

## Project Overview
**DotNet-Messenger** is a real-time messaging application built using ASP.NET Core and SignalR. It allows users to send and receive messages instantly. SignalR enables real-time web functionality, facilitating bi-directional communication between server and client. This project showcases how to integrate SignalR into an ASP.NET Core application for building a simple yet functional chat application.

### Features:
- Real-time messaging with SignalR
- Multi-user support (multiple users can join the conversation)
- Lightweight, fast, and easy to extend
- Clean and modern UI

## Technologies Used
- **ASP.NET Core** - Backend web framework
- **SignalR** - Real-time bi-directional communication
- **Entity Framework Core** - Data access and ORM (Optional if using a database for message persistence)
- **JavaScript/TypeScript** - Frontend scripting
- **Bootstrap/CSS** - Frontend styling

## Getting Started

### Prerequisites
- .NET 6 SDK or later
- Visual Studio 2022 (or your preferred IDE)
- Node.js (for package management of frontend dependencies)
  
### Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/yourusername/DotNet-Messenger.git
    cd DotNet-Messenger
    ```

2. Install dependencies:
    - **For Backend (ASP.NET Core):**
        ```bash
        dotnet restore
        ```
    - **For Frontend (Optional for additional libraries):**
        ```bash
        npm install
        ```

3. Build and run the project:
    ```bash
    dotnet run
    ```

4. Navigate to `https://localhost:5001` in your browser to access the app.

## Project Structure
DotNet-Messenger/ │ ├── Controllers/ │ └── ChatController.cs # Handles API requests │ ├── Hubs/ │ └── ChatHub.cs # SignalR Hub for real-time messaging │ ├── Models/ │ └── Message.cs # Message model (optional if using database) │ ├── wwwroot/ # Static files (JavaScript, CSS, etc.) │ └── js/ │ └── chat.js # JavaScript for SignalR front-end │ └── css/ │ └── site.css # Styles for the application │ ├── Pages/ │ └── Chat.cshtml # Razor page for chat UI │ ├── appsettings.json # Configuration settings ├── Program.cs # Entry point of the application ├── Startup.cs # Configuration and middleware └── DotNet-Messenger.csproj # Project file


### How It Works

1. **SignalR Hub**: The `ChatHub.cs` handles all real-time communication between the clients and the server. The hub listens for messages sent by clients and broadcasts them to all connected clients.
   
2. **Frontend Integration**: The frontend uses SignalR's JavaScript library to connect to the SignalR hub and send/receive messages in real-time. The connection is initiated in `chat.js`.

3. **Backend API**: If you choose to persist messages, the controller (`ChatController.cs`) will be responsible for handling database CRUD operations using Entity Framework.

### Example Code Snippets

#### SignalR Hub Example

```csharp
public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
```

## JavaScript (Frontend) Example

const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveMessage", function (user, message) {
    const msg = user + ": " + message;
    const li = document.createElement("li");
    li.textContent = msg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    const user = document.getElementById("userInput").value;
    const message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});


### Future Enhancements:
- Add user authentication and authorization
- Store chat history in a database
- Implement private messaging
- Improve the UI with additional features (e.g., message timestamps, user status)

### Contributing:
Feel free to submit issues or pull requests to improve this project. Contributions are welcome!

### License:
This project is licensed under the MIT License. See the LICENSE file for details.



