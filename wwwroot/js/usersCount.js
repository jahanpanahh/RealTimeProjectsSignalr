var connectionUserCount = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Information) // Loglevel can be Trace,Information,Error and so on
    .withUrl("/hubs/userCount", signalR.HttpTransportType.WebSockets).build();  // User Transporttype can be configured to WebSockets, or ServerSentEvents or LongPolling

connectionUserCount.on("updateTotalViews", (value) => {
    var newCountSpan = document.getElementById("totalViewsCounter");
    newCountSpan.innerText = value.toString();
});

connectionUserCount.on("updateTotalUsers", (value) => {
    var newCountSpan = document.getElementById("totalUsersCounter");
    newCountSpan.innerText = value.toString();
});

function newWindowLoadedOnClient() {
    connectionUserCount.invoke("NewWindowLoaded", "Hiren").then((value) => console.log(value));
}

function fulfilled() {
    console.log("Connection to User Hub Successful");
    newWindowLoadedOnClient();
}

function rejected() {
    console.log("Connection to User Hub Failed");
}

connectionUserCount.start().then(fulfilled, rejected)