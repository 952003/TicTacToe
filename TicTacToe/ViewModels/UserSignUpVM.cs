
using System.ComponentModel.DataAnnotations;

namespace TicTacToe.ViewModels
{
    public class UserSignUpVM
    {
        [Required]
        [StringLength(50, MinimumLength =1)]
        public string Login { get; set; }

        [Required]
        [StringLength(50 , MinimumLength =1)]
        public string Password { get; set; }

        [Required]
        [StringLength(50 , MinimumLength =1)]
        [Compare("Password", ErrorMessage ="Wrong password")]
        public string ConfirmPassword { get; set; }
    }
}
