using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace TicTacToe.Models
{
    public partial class SessionTag
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int TagId { get; set; }

        public virtual SessionData Session { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
