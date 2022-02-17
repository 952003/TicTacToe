using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    public class UserConnection
    {
        public string ConnectionId { get;}

        public string UserName { get;}

        public UserConnection(string userName, string connectionId)
        {
            this.ConnectionId = connectionId;
            this.UserName = userName;
        }
    }
}
