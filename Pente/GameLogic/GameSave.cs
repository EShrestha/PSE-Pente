using System;
using System.Collections.Generic;
using System.Text;

namespace Pente.GameLogic
{
    // What a save file has
    [Serializable]
    public class GameSave
    {
        // Details of the game
        public readonly int[,] matrix = new int[19,19];
        public List<Player> playersList;
        public Player currentPlayer;
        public int lastX;
        public int lastY;

        // Used to create new game save file
        public GameSave( ref Cell[,] m, List<Player> playersList, Player currentPlayer, int lastX, int lastY)
        {
            storeMatrix(m);
            this.playersList = playersList;
            this.currentPlayer = currentPlayer;
            this.lastX = lastX;
            this.lastY = lastY;
        }


        // Storing the current board in the save game matrix
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
