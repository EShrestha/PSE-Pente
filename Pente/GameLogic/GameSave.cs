using System;
using System.Collections.Generic;
using System.Text;

namespace Pente.GameLogic
{

    [Serializable]
    public class GameSave
    {
        public readonly int[,] matrix = new int[19,19];
        public List<Player> playersList;
        public Player currentPlayer;
        public int lastX;
        public int lastY;

        public GameSave( ref Cell[,] m, List<Player> playersList, Player currentPlayer, int lastX, int lastY)
        {
            storeMatrix(m);
            this.playersList = playersList;
            this.currentPlayer = currentPlayer;
            this.lastX = lastX;
            this.lastY = lastY;
        }

        void storeMatrix(Cell[,] m)
        {
            for (int r = 0; r < m.GetLength(0); r++){
                for(int c = 0; c < m.GetLength(1); c++)
                {
                    matrix[r, c] = m[r, c].color;
                }
            }
        }
    }
}
