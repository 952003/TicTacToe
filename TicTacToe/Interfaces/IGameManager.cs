using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Models;
using TicTacToe.ViewModels;

namespace TicTacToe.Interfaces
{
    public interface IGameManager
    {
        Task<int> OpenGameAsync(CreateGameVM createGameVM, string userName);

        Task CloseGameAsync(int id);

        Task<bool> TryEnterGameAsync(string connectionId, string userName, int gameId);

        Task<bool> TryStartGameAsync(GameSession game);
    }
}
