using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Models;

namespace TicTacToe.Interfaces
{
    public interface ISessionTagCrudService
    {
        Task CreateAsync(int sessionId, int tagId);

        Task<IEnumerable<SessionData>> GetSessionByTagAsync(IEnumerable<int> tagId);

        Task<IEnumerable<Tag>> GetTagsBySessionAsync(int sessionId);

        Task<IQueryable<SessionTag>> GetAll();
    }
}
