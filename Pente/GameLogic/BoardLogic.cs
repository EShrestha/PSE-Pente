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
                // If we are checking for a win, it does not matter if the winning row is open ended or not
                if (checkForWin) { return true; }

                // Tria checks
                if (numOfPieces == 4)
                {
                    // [Start piece], [location on the board]

                    // Left, left edge                             
                    try{ var x = matrix[startRow, startCol - 1]; }catch{ return (matrix[startRow, startCol + 3].color == 0); }
                    // Right, right edge
                    try {var x = matrix[startRow, startCol + 1]; } catch { return (matrix[startRow, startCol - 3].color == 0); }

                    // Means piece is middle piece
                    if(matrix[startRow, startCol+1].color == pieceColor && matrix[startRow, startCol - 1].color == pieceColor)
                    {
                        // Middle, any where on the board
                        try { if(matrix[startRow, startCol - 2].color == 0){ return true; } } // if 2 left of the piece is 0 return true
                        catch { if (matrix[startRow, startCol + 2].color == 0) { return true; } } // if checking 2 left of the piece is error means it is on the left edge so check right

                        // Same as above but reflected horizontally
                        try { if (matrix[startRow, startCol + 2].color == 0) { return true; } } 
                        catch { if (matrix[startRow, startCol - 2].color == 0) { return true; } }
                    }
                    // Means piece is not the middle piece
                    else
                    {
                        // Means it is the right piece
                        if(matrix[startRow, startCol - 1].color == pieceColor)
                        {
                            try { if (matrix[startRow, startCol - 3].color == 0) { return true; } } catch { }
                            try { if (matrix[startRow, startCol + 1].color == 0) { return true; } } catch { }

                        }
                        // Means it is the left piece
                        else
                        {
                            try { if (matrix[startRow, startCol + 3].color == 0) { return true; } } catch { }
                            try { if (matrix[startRow, startCol - 1].color == 0) { return true; } } catch { }
                        }
                    }
                    //Safety
                    return false;

                }
                // Tesera check
                else
                {

                    // [Start piece], [location on the board]

                    // Left, left edge                             
                    try { var x = matrix[startRow, startCol - 1]; } catch { return (matrix[startRow, startCol + 4].color == 0); }
                    // Right, right edge
                    try { var x = matrix[startRow, startCol + 1]; } catch { return (matrix[startRow, startCol - 4].color == 0); }

                    // Means piece is the middle two pieces
                    if (matrix[startRow, startCol + 1].color == pieceColor && matrix[startRow, startCol - 1].color == pieceColor)
                    {
                        //Means it is left middle
                        try { if (matrix[startRow, startCol + 2].color == pieceColor) 
                        {
                            try { if (matrix[startRow, startCol + 3].color == 0) { return true; } } catch { }
                            try { if (matrix[startRow, startCol - 2].color == 0) { return true; } } catch { }

                        } } catch { }

                        //Means it is right middle
                        try
                        {
                            if (matrix[startRow, startCol + 2].color == pieceColor)
                            {
                                try { if (matrix[startRow, startCol - 3].color == 0) { return true; } } catch { }
                                try { if (matrix[startRow, startCol + 2].color == 0) { return true; } } catch { }

                            }
                        }catch { }
                    }
                    // Means it is far left/right piece
                    else
                    {
                        // Means it is the right piece
                        if (matrix[startRow, startCol - 1].color == pieceColor)
                        {
                            try { if (matrix[startRow, startCol - 4].color == 0) { return true; } } catch { }
                            try { if (matrix[startRow, startCol + 1].color == 0) { return true; } } catch { }

                        }
                        // Means it is the left piece
                        else
                        {
                            try { if (matrix[startRow, startCol + 4].color == 0) { return true; } } catch { }
                            try { if (matrix[startRow, startCol - 1].color == 0) { return true; } } catch { }
                        }
                    }

                    //Safety
                    return false;
                }
            }
            else
            {
                pieceCount = 0; count = 0;
            }


            while (count != 2)
            {
                //// Vertical 
                for (int row = startRow; (row - 1 >= -1 && row < matrix.GetLength(0) && Math.Abs(startRow - row) <= numOfPieces); row += prefix)
                {
                    if (matrix[row, startCol].color == pieceColor) { pieceCount++; } else { break; }
                }
                count++;
                prefix *= -1;
            }
            if (pieceCount == numOfPieces)
            {
                // If we are checking for a win, it does not matter if the winning row is open ended or not
                if (checkForWin) { return true; }

                // Tria checks
                if (numOfPieces == 4)
                {
                    // [Start piece], [location on the board]

                    // Left, left edge                             
                    try { var x = matrix[startRow - 1, startCol]; } catch { return (matrix[startRow + 3, startCol].color == 0); }
                    // Right, right edge
                    try { var x = matrix[startRow + 1, startCol]; } catch { return (matrix[startRow - 3, startCol].color == 0); }

                    // Means piece is middle piece
                    if (matrix[startRow + 1, startCol].color == pieceColor && matrix[startRow - 1, startCol].color == pieceColor)
                    {
                        // Middle, any where on the board
                        try { if (matrix[startRow - 2, startCol].color == 0) { return true; } } // if 2 left of the piece is 0 return true
                        catch { if (matrix[startRow + 2, startCol].color == 0) { return true; } } // if checking 2 left of the piece is error means it is on the left edge so check right

                        // Same as above but reflected horizontally
                        try { if (matrix[startRow + 2, startCol].color == 0) { return true; } }
                        catch { if (matrix[startRow - 2, startCol].color == 0) { return true; } }
                    }
                    // Means piece is not the middle piece
                    else
                    {
                        // Means it is the right piece
                        if (matrix[startRow - 1, startCol].color == pieceColor)
                        {
                            try { if (matrix[startRow - 3, startCol].color == 0) { return true; } } catch { }
                            try { if (matrix[startRow + 1, startCol].color == 0) { return true; } } catch { }

                        }
                        // Means it is the left piece
                        else
                        {
                            try { if (matrix[startRow + 3, startCol].color == 0) { return true; } } catch { }
                            try { if (matrix[startRow - 1, startCol].color == 0) { return true; } } catch { }
                        }
                    }
                    //Safety
                    return false;

                }
                // Tesera check
                else
                {

                    // [Start piece], [location on the board]

                    // Left, left edge                             
                    try { var x = matrix[startRow - 1, startCol]; } catch { return (matrix[startRow + 4, startCol].color == 0); }
                    // Right, right edge
                    try { var x = matrix[startRow + 1, startCol]; } catch { return (matrix[startRow - 4, startCol].color == 0); }

                    // Means piece is the middle two pieces
                    if (matrix[startRow + 1, startCol].color == pieceColor && matrix[startRow - 1, startCol].color == pieceColor)
                    {
                        //Means it is left middle
                        try
                        {
                            if (matrix[startRow, startCol + 2].color == pieceColor)
                            {
                                try { if (matrix[startRow + 3, startCol].color == 0) { return true; } } catch { }
                                try { if (matrix[startRow - 2, startCol].color == 0) { return true; } } catch { }

                            }
                        }
                        catch { }

                        //Means it is right middle
                        try
                        {
                            if (matrix[startRow + 2, startCol].color == pieceColor)
                            {
                                try { if (matrix[startRow - 3, startCol].color == 0) { return true; } } catch { }
                                try { if (matrix[startRow + 2, startCol].color == 0) { return true; } } catch { }

                            }
                        }
                        catch { }
                    }
                    // Means it is far left/right piece
                    else
                    {
                        // Means it is the right piece
                        if (matrix[startRow - 1, startCol].color == pieceColor)
                        {
                            try { if (matrix[startRow - 4, startCol].color == 0) { return true; } } catch { }
                            try { if (matrix[startRow + 1, startCol].color == 0) { return true; } } catch { }

                        }
                        // Means it is the left piece
                        else
                        {
                            try { if (matrix[startRow + 4, startCol].color == 0) { return true; } } catch { }
                            try { if (matrix[startRow - 1, startCol].color == 0) { return true; } } catch { }
                        }
                    }

                    //Safety
                    return false;
                }
            }
            else
            {
                pieceCount = 0; count = 0;
            }

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
                for (int r = startRow; ((c - 1 >= -1 && c < matrix.GetLength(1)) && (r - 1 >= -1 && r < matrix.GetLength(0))); r -= prefix, c += prefix)
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
