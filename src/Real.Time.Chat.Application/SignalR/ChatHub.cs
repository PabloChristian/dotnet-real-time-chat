using Microsoft.AspNetCore.SignalR;

namespace Real.Time.Chat.Application.SignalR
{
    public class ChatHub : Hub
    {
        public static IDictionary<string, string> users = new Dictionary<string, string>();

        public async override Task OnConnectedAsync()
        {
            var email = Context.GetHttpContext().Request.Query["email"];
            users.Add(Context.ConnectionId, email);
            await Groups.AddToGroupAsync(Context.ConnectionId, email);
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception? e) => await base.OnDisconnectedAsync(e);
    }
}
