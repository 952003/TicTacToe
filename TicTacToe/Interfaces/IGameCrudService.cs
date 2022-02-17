using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Models;

namespace TicTacToe.Interfaces
{
    public interface IGameCrudService
    {
        Task CreateAsync(string gameName, User userCreator);

        Task UpdateAsync(GameSession game);

        Task DeleteAsync(int id);

        Task<GameSession> GetGameAsync(int id);

        Task<GameSession> GetGameAsync(string name);

        Task<IEnumerable<GameSession>> GetAllGamesAsync();
    }
}
