using System;
using System.Collections.Generic;
using System.Text;


namespace Pente.GameLogic
{
    public class BoardLogic
    {

        // Checks if x number of pieces are lined up together, so this can be used to check for tira(3), tesra(4), or win check(5)
        public bool xPiecesInSuccession(Cell[,] matrix, int startRow, int startCol, int pieceColor, int numOfPieces, bool checkForWin = false)
        {
            byte count = 0;
            int prefix = 1;
            byte pieceCount = 0;
            numOfPieces++; // adjusting because starting piece is counted twice

            //// Horizontal
            while (count != 2)
            {
                for (int col = startCol; (col - 1 >= -1 && col < matrix.GetLength(1) && Math.Abs(startCol - col) <= numOfPieces); col += prefix)
                {
                    if (matrix[startRow, col].color == pieceColor) { pieceCount++; } else { break; }
                }
                count++;
                prefix *= -1;
            }
            if (pieceCount == numOfPieces)
            {
                // Tria checks
                if (numOfPieces == 4)
                {
                    //Checking if placed piece is the middle piece
                    if (matrix[startRow,startCol - 1].color == pieceColor && matrix[startRow, startCol + 1].color == pieceColor) 
                    {
                        // if true, means piece is no at the edge of the board
                        if (startCol - 2 >= 0 && startCol + 2 < matrix.GetLength(0))
                        {
                            if (matrix[startRow, startCol - 2].color == 0 || matrix[startRow, startCol + 2].color == 0)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        // means we are at the very right edge
                        else if (startCol - 2 >=0){
                            if (matrix[startRow, startCol - 2].color == 0)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        // means we ar at the very left edge
                        else if ( startCol + 2 < matrix.GetLength(0))
                        {
                            if (matrix[startRow, startCol + 2].color == 0)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    } 
                }
                
            } else 
            { 
                pieceCount = 0; count = 0; 
            }


            while (count != 2)
            {
                //// Vertical 
                for (int row = startRow; (row - 1 >= -1 && row < matrix.GetLength(0) && Math.Abs(startRow - row) <= numOfPieces); row+=prefix)
                {
                    if (matrix[row, startCol].color == pieceColor) { pieceCount++; } else { break; }
                }
                count++;
                prefix *= -1;
            }
            if (pieceCount == numOfPieces) { return true; } else { pieceCount = 0; count = 0; }

            while (count != 2)
            {
                //// Diagonal (top left <--> bottom right)
                    int c = startCol;
                for (int r = startRow; ((c - 1 >= -1 && c < matrix.GetLength(1)) && (r - 1 >= -1 && r < matrix.GetLength(0))); r += prefix, c += prefix)
                {
                    if (matrix[r, c].color == pieceColor) { pieceCount++; } else { break; }
                }
                 count++;
                prefix *= -1;
            }
            if (pieceCount == numOfPieces) { return true; } else { pieceCount = 0; count = 0; }

            while (count != 2)
            {
                //// Diagonal (top right <--> bottom left)
                int c = startCol;
                for (int r = startRow; ((c - 1 >= -1 && c < matrix.GetLength(1)) && (r - 1 >= -1 && r < matrix.GetLength(0))); r-=prefix, c+=prefix)
                {
                    if (matrix[r, c].color == pieceColor) { pieceCount++; } else { break; }
                }
                count++;
                prefix *= -1;
            }
            if (pieceCount == numOfPieces) { return true; }

            return false;
        }


        public bool isCapture(ref Cell[,] matrix, int startRow, int startCol, int color)
        {
            return true;
        }


        public bool aiMakeMove(ref Cell[,] matrix)
        {

            return true; // should be false
        }


    }


}
