using System;
using System.Collections.Generic;

namespace TicTacToe.Models
{
    public class User
    {
        public User()
        {
            Sessions = new HashSet<SessionData>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual ICollection<SessionData> Sessions { get; set; }
    }
}
