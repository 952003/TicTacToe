using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Hubs
{
    public class MainHub : Hub
    {
        public static Dictionary<string, (int, int)> userMove = new Dictionary<string, (int, int)>();

        public async Task MakeMove(int x, int y)
        {
            userMove.Add(Context.ConnectionId, (x, y));
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
