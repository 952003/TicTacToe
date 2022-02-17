using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Models;
using TicTacToe.ViewModels;

namespace TicTacToe.Interfaces
{
    public interface IUserCrudService
    {
        Task CreateAsync(UserSignUpVM createUserVM);

        Task<User> GetByLoginAsync(string login);

        Task<IEnumerable<User>> GetAll();
    }
}
