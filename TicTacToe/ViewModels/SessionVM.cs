using System.Collections.Generic;

namespace TicTacToe.ViewModels
{
    public class SessionVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public IList<string> Tags { get; set; }
    }
}
