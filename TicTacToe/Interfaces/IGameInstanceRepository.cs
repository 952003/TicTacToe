using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Models;

namespace TicTacToe.Interfaces
{
    public interface IGameInstanceRepository
    {
        Task Create(GameInstance entity);

        Task<GameInstance> Get(int id);

        Task<IEnumerable<GameInstance>> GetAll();

        Task Delete(int id);
    }
}
