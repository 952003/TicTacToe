using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Interfaces;

namespace TicTacToe.Services.DataContext
{
    public class UnitOfWork : IUnitOfWOrk
    {
        public AppDbContext DbContext { get; }

        public IGameInstanceRepository GameStore { get; }

        public UnitOfWork(AppDbContext dbContext, IGameInstanceRepository gameInstanceRepository)
        {
            this.DbContext = dbContext;
            this.GameStore = gameInstanceRepository;
        }
    }
}
