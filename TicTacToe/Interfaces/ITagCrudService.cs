using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Models;

namespace TicTacToe.Interfaces
{
    public interface ITagCrudService
    {
        Task<int> CreateAsync(Tag tag);

        Task DeleteAsync(int id);

        Task<Tag> GetAsync(int id);

        Task<Tag> GetAsync(string value);

        Task<IEnumerable<Tag>> GetAll();
    }
}
