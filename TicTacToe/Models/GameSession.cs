using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    public class GameSession
    {
        public GameInstance Instance { get; }

        public SessionData Data { get; }

        public GameSession(SessionData gameDate)
        {
            this.Data = gameDate;
            this.Instance = new GameInstance(gameDate.Id.ToString());
        }

        public GameSession(SessionData gameData, GameInstance gameInstance)
        {
            this.Date = gameData;
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
