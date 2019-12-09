using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.SignalR
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            string userName = Context.User.Identity.Name;
            string NowDate = DateTime.Now.ToString();

            await Clients.All.SendAsync("ReceiveMessage", userName, message, NowDate);
        }
        public async Task ConnectUser(string user)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, $"{user} just connected");
        }
        public async override Task OnConnectedAsync()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", "Messenger", "Loading history...");
        }
    }
}
