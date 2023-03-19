var cloakSpan = document.getElementById("cloakCounter");
var stoneSpan = document.getElementById("stoneCounter");
var wandSpan = document.getElementById("wandCounter");

//create Signalr connection
var connectionDeathlyHallows = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Information) // Loglevel can be Trace,Information,Error and so on
    .withUrl("/hubs/deathlyhallows").build();  // User Transporttype can be configured to WebSockets, or ServerSentEvents or LongPolling

connectionDeathlyHallows.on("updateDeathlyHallowCount", (cloak,stone,wand) => {
    cloakSpan.innerText = cloak.toString();
    stoneSpan.innerText = stone.toString();
    wandSpan.innerText = wand.toString();
});

function fulfilled() {
    console.log("Connection to DeathlyHallow Hub Successful");
    connectionDeathlyHallows.invoke("GetRaceStatus").then(value => {
        console.log(value);
        cloakSpan.innerText = value.cloak.toString();
        stoneSpan.innerText = value.stone.toString();
        wandSpan.innerText = value.wand.toString()
    })
}

function rejected() {
    console.log("Connection to DeathlyHallow Hub Failed");
}

connectionDeathlyHallows.start().then(fulfilled, rejected)