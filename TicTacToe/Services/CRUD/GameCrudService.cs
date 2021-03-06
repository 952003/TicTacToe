using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Interfaces;
using TicTacToe.Models;

namespace TicTacToe.Services.CRUD
{
    public class GameCrudService : IGameCrudService
    {
        private readonly IUnitOfWork unitOfWork;

        public GameCrudService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(string gameName, User userCreator)
        {
            if (await unitOfWork.DbContext.Sessions.AnyAsync(s => s.Name == gameName))
                return;
            SessionData gameData = new SessionData(gameName, userCreator);
            unitOfWork.DbContext.Sessions.Add(gameData);
            await unitOfWork.DbContext.SaveChangesAsync();
            GameSession game = new GameSession(gameData);
            await unitOfWork.GameStore.Create(game.Instance);
        }

        public async Task UpdateAsync(GameSession game)
        {
            unitOfWork.DbContext.Entry(game.Data).State = EntityState.Modified;
            await unitOfWork.DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int gameId)
        {
            var gameData = await unitOfWork.DbContext.Sessions.Include(t => t.Creator).FirstOrDefaultAsync(d => d.Id == gameId);
            if(gameData != null)
            {
                unitOfWork.DbContext.Sessions.Remove(gameData);
                unitOfWork.DbContext.SaveChanges();
            }
            await unitOfWork.GameStore.Delete(gameId);
        }

        public async Task<GameSession> GetGameAsync(int id)
        {
            var gameData = await unitOfWork.DbContext.Sessions.FindAsync(id);
            var gameInstance = await unitOfWork.GameStore.Get(id);
            var game = new GameSession(gameData, gameInstance);
            return game;
        }

        public async Task<GameSession> GetGameAsync(string name)
        {
            var gameData = await unitOfWork.DbContext.Sessions.FirstOrDefaultAsync(g => g.Name == name);
            var gameInstance = await unitOfWork.GameStore.Get(gameData.Id);
            var game = new GameSession(gameData, gameInstance);
            return game;
        }

        public async Task<IEnumerable<GameSession>> GetAllGamesAsync()
        {
            var games = new List<GameSession>();
            foreach(var gd in unitOfWork.DbContext.Sessions.Include(e => e.Creator))
            {
                var gi = await GetGameInstance(gd.Id);
                games.Add(new GameSession(gd, gi));
            }
            return games;
        }

        private async Task<GameInstance> GetGameInstance(int id)
        {
            var gameInstance = await unitOfWork.GameStore.Get(id);
            return gameInstance;
        }
    }
}
