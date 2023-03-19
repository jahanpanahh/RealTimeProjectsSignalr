using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
    public class NotificationHub:Hub
    {
        public static List<string> Messages = new List<string>();

        public async Task LogMessage(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                Messages.Add(message);
                await Clients.All.SendAsync("MessageReceived", Messages.Count(), Messages.ToArray());
            }
        }

        public async Task LoadMessages()
        {
            await Clients.All.SendAsync("MessageReceived", Messages.Count(), Messages.ToArray());
        }
    }
}
