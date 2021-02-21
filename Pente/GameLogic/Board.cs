using System;
using System.Collections.Generic;
using System.Text;

namespace Pente.GameLogic
{

    // What a pente board has
    public class Board
    {
        Cell[,] matrix; // A matrix
        
        // Creating the board with number of rows and cols
        public Board(int rows, int cols)
        {
            matrix = new Cell[rows, cols];
        }

        // Simply returns the board
        public Cell[,] getBoard()
        {
            return matrix;
        }

        
    }
}
