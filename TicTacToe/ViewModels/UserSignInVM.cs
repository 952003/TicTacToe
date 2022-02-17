using System.ComponentModel.DataAnnotations;

namespace TicTacToe.ViewModels
{
    public class UserSignInVM
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
