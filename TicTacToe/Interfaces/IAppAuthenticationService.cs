using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Interfaces
{
    public interface IAppAuthenticationService
    {
        Task<bool> SignInAsync(string username, string password);

        Task<bool> SignUpAsync(string username, string password);

        Task SignOutAsync();
    }
}
