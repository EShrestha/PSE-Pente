using System;
using System.Collections.Generic;
using System.Text;

namespace Pente.GameLogic
{
    public class Board
    {


        Cell[,] matrix;
        
        public Board(int rows, int cols)
        {
            matrix = new Cell[rows, cols];
        }

        public Cell[,] getBoard()
        {
            return matrix;
        }

        
    }
}
