using System;
using System.Collections.Generic;
using System.Linq;
namespace TicTacToe.Models
{
    public class GameSession
    {
        public SessionData Data { get; }
        public GameInstance Instance { get; }

        public GameSession(SessionData gameDate)
        {
            this.Data = gameDate;
            this.Instance = new GameInstance(gameDate.Id.ToString());
        }

        public GameSession(SessionData gameData, GameInstance gameInstance)
        {
            this.Data = gameData;
            this.Instance = gameInstance;
        }

        public bool IsAlive()
        {
            if(Instance is null|| Data is null)
            {
                return false;
            }
            return true;
        }
    }
}
