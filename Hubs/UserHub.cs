using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
    // Clients.All.SendAsync    <- send to all connected clients
    // Clients.Caller.SendAsync <- send to only caller
    // Clients.Others.SendAsync <- sendt to all other than caller
    // Clients.Client(connectionId) <- send to only particular client
    // Clients.Clients(connectionIdA,connectionIdB) <- send to only provided connectionIds
    // Clients.AllExcept(connectionIdA,connectionIdB) <- send to all except provided connectionIds
    // Clients.User("userId/Account from Identity").SendAsync("") <- send to all connections from particular user
    // Clients.User("useraccount1", "useraccount2").SendAsync(") <- send to all connections of provided users
    // Clients.AddToGroupAsync("connectionId", groupname) <- adds connection to group
    // Clients.RemoveFromGroupAsync("connectionId", groupname) <- removes connection to group
    // Clients.Group(groupname).SendAsync() <- send to all connections in group
    // Clients.OthersInGroup(groupname).SendAsync <- send to all except the caller in group
    // Clients.Groups("group1","group2").SendAsync <- send to specified groups
    // Clients.GroupExcept("group1","user1").SendAsync <- send to all in group except specified user

    // From Clients: Send vs Invoke
    // Send invokes hub method on the server using specified name and arguments and does not wait for response from the receiver
    // Invoke invokes hub method on the server using specified name and arguments and DOES wait for response
    public class UserHub:Hub
    {
        public static int TotalViews { get; set; }
        public static int TotalUsers { get; set; }

        public override Task OnConnectedAsync()
        {
            TotalUsers++;
            Clients.All.SendAsync("updateTotalUsers", TotalUsers).GetAwaiter().GetResult();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            TotalUsers--;
            Clients.All.SendAsync("updateTotalUsers", TotalUsers).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }
        public async Task<string> NewWindowLoaded(string val)
        {
            TotalViews++;
            //send update to all clients
            await Clients.All.SendAsync("updateTotalViews", TotalViews);
            return $"total views from {val} - {TotalViews}";
        }
    }
}
