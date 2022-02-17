using System.ComponentModel.DataAnnotations;

namespace TicTacToe.ViewModels
{
    public class CreateGameVM
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public string Tags { get; set; }
    }
}
