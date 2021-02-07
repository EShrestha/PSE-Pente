using System;
using System.Collections.Generic;
using System.Text;


namespace Pente.GameLogic
{
    public class BoardLogic
    {
        public bool checkTriaHorVer(Cell[,] matrix, int row, int col, byte color)
        {
            int pieceCount = 0;

            // Checking right of piece for more like pieces
            for(int c = col; (c<matrix.GetLength(1) && col-c < 3); c++)
            {   
                if(matrix[row, c].color == color) { pieceCount++; } else { break; }
            }
            // Checking left of piece for more like pieces
            for (int c = col; c >= 0; c--)
            {
                if (matrix[row, c].color == color) { pieceCount++; } else { break; }
            }
            // If there are 3 pieces horizontal it is a tria
            if (pieceCount == 4) { return true; } else { pieceCount = 0; } // == 4 because the place piece is counted twice



            // Checking right of piece for more like pieces
            for (int r = row; r < matrix.GetLength(1); r++)
            {

                if (matrix[r, col].color == color) { pieceCount++; } else { break; }
            }
            // Checking left of piece for more like pieces
            for (int r = row; r >= 0; r--)
            {
                if (matrix[r, col].color == color) { pieceCount++; } else { break; }
            }

            // If there are 3 pieces vertical it is a tria
            if (pieceCount == 4) { return true; } else { pieceCount = 0; } // == 4 because the place piece is counted twice


            return false;
        }

        // Checks if x number of pieces are lined up together, so this can be used to check for tira(3), tesra(4), or win check(5)
        public bool xPiecesInSuccession(Cell[,] matrix, int startRow, int startCol, int pieceColor, int numOfPieces)
        {
            byte count = 0;
            int prefix = 1;
            byte pieceCount = 0;
            do
            {
                //// Horizontal
                for (int col = startCol; (col-1>=-1 && col<matrix.GetLength(1) && Math.Abs(startCol - col) <= numOfPieces); col+=prefix)
                {
                    if (matrix[startRow, col].color == pieceColor) { pieceCount++; } else { break; }
                }
                    if (pieceCount == numOfPieces) { return true; } else { pieceCount = 0; }

                //// Vertical 
                for (int row = startRow; (row - 1 >= -1 && row < matrix.GetLength(0) && Math.Abs(startRow - row) <= numOfPieces); row+=prefix)
                {
                    if (matrix[row, startCol].color == pieceColor) { pieceCount++; } else { break; }
                }
                if (pieceCount == numOfPieces) { return true; } else { pieceCount = 0; }

                //// Diagonal (top left <--> bottom right)
                     int c = startCol;
                for (int r = startRow; ((c - 1 >= -1 && c < matrix.GetLength(1)) && (r - 1 >= -1 && r < matrix.GetLength(0))); r += prefix, c += prefix)
                {
                    if (matrix[r, c].color == pieceColor) { pieceCount++; } else { break; }
                }
                if (pieceCount == numOfPieces) { return true; } else { pieceCount = 0; }

                //// Diagonal (top right <--> bottom left)
                         c = startCol;
                for (int r = startRow; ((c - 1 >= -1 && c < matrix.GetLength(1)) && (r - 1 >= -1 && r < matrix.GetLength(0))); r-=prefix, c+=prefix)
                {
                    if (matrix[r, c].color == pieceColor) { pieceCount++; } else { break; }
                }
                if (pieceCount == numOfPieces) { return true; } else { pieceCount = 0; }


                count++;
                prefix = -1;
            } while (count != 2);


            return false;
        }




    }
}
