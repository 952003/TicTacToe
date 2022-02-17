using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Models
{
    public class GameInstance
    {
        private char moveValue = 'x';

        private int moveCount = 0;

        private char?[] map = new char?[9];

        private Dictionary<char, UserConnection> charUser = new Dictionary<char, UserConnection>();

        private List<UserConnection> players = new List<UserConnection>();

        public string Id { get; }

        public bool IsReadyToStart { get; private set; }

        public UserConnection CurrentPlayer { get; private set; }

        public GameInstance(string id)
        {
            this.Id = id;
        }

        public MoveResult MakeMove(int position, string connectionId)
        {
            if (connectionId != CurrentPlayer.ConnectionId || position < 0 || position > 8 || map[position] != null || moveCount >= 9)
                return new MoveResult(false);
            map[position] = moveValue;
            SwapMoveValue();
            SwapCurrentPlayer();
            MoveResult res = CalcResult();
            return res;
        }

        public void StartGame()
        {
            Random rnd = new Random();
            var index = rnd.Next(0, 2);
            CurrentPlayer = players[index];
            charUser.Add('x', CurrentPlayer);
            charUser.Add('o', players.FirstOrDefault(u => u != CurrentPlayer));
        }

        public bool RegistPlayer(string connectionId, User user)
        {
            if (players.Count >= 2 || string.IsNullOrEmpty(connectionId) || user is null)
                return false;
            AddConnection(connectionId, user);
            if (players.Count == 2)
                IsReadyToStart = true;
            return true;
        }

        public IEnumerable<UserConnection> GetPlayers()
        {
            return players.ToList();
        }

        private void AddConnection(string connectionId, User user)
        {
            var player = players.FirstOrDefault(p => p.UserName == user.Login);
            if (player != null)
                players.Remove(player);
            var connection = new UserConnection(user.Login, connectionId);
            players.Add(connection);
        }

        private void SwapMoveValue()
        {
            if (moveValue == 'x')
                moveValue = 'o';
            else
                moveValue = 'x';
            moveCount++;
        }

        private void SwapCurrentPlayer()
        {
            if (CurrentPlayer == players[0])
                CurrentPlayer = players[1];
            else
                CurrentPlayer = players[0];
        }

        private MoveResult CalcResult()
        {
            if (!CheckLines(map, 3, out char? value))
                if (!CheckColumns(map, 3, out value))
                    if (!CheckDiagonals(map, 3, out value))
                        if (moveCount < 9)
                            return new MoveResult(true);
                        else
                            return new MoveResult();
            return new MoveResult($"{charUser[value.Value].UserName}");
        }

        private bool CheckLines(char?[] array, int rowLength, out char? value)
        {
            bool isMatched = false;
            value = null;
            for (int i = 0; i < array.Length / rowLength; i++)
            {
                for (int j = 1; j < rowLength; j++)
                {
                    if (array[i * rowLength + j] != null && array[i * rowLength + j] == array[i * rowLength + j - 1])
                    {
                        isMatched = true;
                        value = array[i * rowLength + j];
                    }
                    else
                    {
                        value = null;
                        isMatched = false;
                        break;
                    }
                }
                if (isMatched)
                    break;
            }
            return isMatched;
        }

        private bool CheckColumns(char?[] array, int rowLenght, out char? value)
        {
            bool isMatched = false;
            value = null;
            for (int i = 0; i < array.Length / rowLenght; i++)
            {
                for (int j = 1; j < rowLenght; j++)
                {
                    if (array[i + rowLenght * j] != null && array[i + rowLenght * j] == array[i + rowLenght * (j - 1)])
                    {
                        value = array[i + rowLenght * j];
                        isMatched = true;
                    }
                    else
                    {
                        value = null;
                        isMatched = false;
                        break;
                    }
                }
                if (isMatched)
                    break;
            }
            return isMatched;
        }

        private bool CheckDiagonals(char?[] array, int rank, out char? value)
        {
            bool isMatched = false;
            value = null;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 1; j < rank; j++)
                {
                    int k = (int)Math.Pow(-1, i);
                    if (array[rank * j + j * k + (rank - 1) * i] != null && array[rank * j + j * k + (rank - 1) * i] == array[rank * (j - 1) + (j - 1) * k + (rank - 1) * i])
                    {
                        value = array[rank * j + j * k + (rank - 1) * i];
                        isMatched = true;
                    }
                    else
                    {
                        value = null;
                        isMatched = false;
                        break;
                    }
                }
                if (isMatched)
                    break;
            }
            return isMatched;
        }
    }
}
