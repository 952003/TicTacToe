using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace TicTacToe.Models
{
    [Table("Sessions")]
    public partial class SessionData
    {
        public SessionData()
        {
            SessionTags = new HashSet<SessionTag>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CreatorId { get; set; }
        public bool Started { get; set; }

        public virtual User Creator { get; set; }
        public virtual ICollection<SessionTag> SessionTags { get; set; }

        public SessionData(string name, User userCreator)
        {
            this.Name = name;
            this.Creator = userCreator;
        }
    }
}
