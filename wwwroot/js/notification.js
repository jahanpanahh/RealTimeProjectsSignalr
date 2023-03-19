let lblNotificationCount = document.getElementById("notificationCounter");
let sendBtn = document.getElementById("sendButton");
let notificationInput = document.getElementById("notificationInput");
let messageList = document.getElementById("messageList");

var connectionNotification = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Information) // Loglevel can be Trace,Information,Error and so on
    .withUrl("/hubs/notification", signalR.HttpTransportType.WebSockets).build();  // User Transporttype can be configured to WebSockets, or ServerSentEvents or LongPolling

sendBtn.disabled = true;
sendBtn.addEventListener("click", function (event) {
    connectionNotification.send("LogMessage", notificationInput.value).then(function () {
        notificationInput.value = "";
    });
    event.preventDefault();
})

connectionNotification.on("MessageReceived", (count, messages) => {
    lblNotificationCount.innerHTML = "<span>(" + count + ")<span/>";
    messageList.innerHTML = "";
    for (let i = count - 1; i >= 0; i--) {
        var li = document.createElement("li");
        li.textContent = "Notification - " + messages[i];
        messageList.appendChild(li);
    }
    
})
function fulfilled() {
    sendBtn.disabled = false;
    connectionNotification.send("LoadMessages");
    console.log("Connection to Notification Hub Successful");
}

function rejected() {
    console.log("Connection to Notification Hub Failed");
}

connectionNotification.start().then(fulfilled, rejected)