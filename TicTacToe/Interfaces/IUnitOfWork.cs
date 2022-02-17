using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Services.DataContext;

namespace TicTacToe.Interfaces
{
    public interface IUnitOfWork
    {
        public AppDbContext DbContext { get; }
        public IGameInstanceRepository GameStore { get; }
    }
}
