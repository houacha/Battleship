using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class GameBoard
    {
        string[,] gameBoard;
        public GameBoard()
        {
            gameBoard = new string[10, 10];
        }
        public void GameboardIcon()
        {
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    gameBoard[i, j] = "[ ]";
                }
            }
        }
    }
}
