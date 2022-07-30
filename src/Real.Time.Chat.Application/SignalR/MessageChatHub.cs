using Microsoft.AspNetCore.SignalR;

namespace Real.Time.Chat.Application.SignalR
{
    public class MessageChatHub : Hub
    {
        public static IDictionary<string, string> users = new Dictionary<string, string>();

        public async override Task OnConnectedAsync()
        {
            var username = Context.GetHttpContext().Request.Query["username"];
            users.Add(Context.ConnectionId, username);
            await Groups.AddToGroupAsync(Context.ConnectionId, username);
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception? e) => await base.OnDisconnectedAsync(e);
    }
}
