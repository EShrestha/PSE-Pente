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
            if (checkForWin && pieceCount >= numOfPieces) { return true; }
            if (pieceCount == numOfPieces)
            {
                // Tria checks
                if (numOfPieces == 4)
                {
                    // [Start piece], [location on the board]

                    // Left, left edge                             
                    try{ var x = matrix[startRow, startCol - 1]; }catch{ return false; }
                    // Right, right edge
                    try {var x = matrix[startRow, startCol + 1]; } catch { return false; }

                    // Means piece is middle piece
                    if(matrix[startRow, startCol+1].color == pieceColor && matrix[startRow, startCol - 1].color == pieceColor)
                    {
                        // Middle, any where on the board
                        try { var x = (matrix[startRow, startCol - 2].color == 0 && matrix[startRow, startCol + 2].color == 0); } catch { return false; }
                        try { if(matrix[startRow, startCol - 2].color == 0) { return true; } } // if 2 left of the piece is 0 return true
                        catch { if (matrix[startRow, startCol + 2].color == 0 ) { return true; } } // if checking 2 left of the piece is error means it is on the left edge so check right
                    }
                    // Means piece is not the middle piece
                    else
                    {
                        // Means it is the right piece
                        if(matrix[startRow, startCol - 1].color == pieceColor)
                        {
                            try { if (matrix[startRow, startCol - 3].color == 0 && matrix[startRow, startCol + 1].color == 0) { return true; } } catch { }
  
                        }
                        // Means it is the left piece
                        else
                        {
                            try { if (matrix[startRow, startCol + 3].color == 0 && matrix[startRow, startCol - 1].color == 0) { return true; } } catch { }
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
                    try { var x = matrix[startRow, startCol - 1]; } catch { return false; }
                    // Right, right edge
                    try { var x = matrix[startRow, startCol + 1]; } catch { return false; }

                    // Means piece is the middle two pieces
                    if (matrix[startRow, startCol + 1].color == pieceColor && matrix[startRow, startCol - 1].color == pieceColor)
                    {
                        // x X x x
                        //Means it is left middle
                        try { if (matrix[startRow, startCol + 2].color == pieceColor) 
                        {
                            try { if (matrix[startRow, startCol + 3].color == 0 && matrix[startRow, startCol - 2].color == 0) { return true; } } catch { }
                        } } catch { }

                        // x x X x
                        //Means it is right middle
                        try
                        {
                            if (matrix[startRow, startCol + 2].color == pieceColor)
                            {
                                try { if (matrix[startRow, startCol - 3].color == 0 && matrix[startRow, startCol + 2].color == 0) { return true; } } catch { }
                            }
                        }catch { }
                    }
                    // Means it is far left/right piece
                    else
                    {
                        // x x x X
                        // Means it is the right piece
                        if (matrix[startRow, startCol - 1].color == pieceColor)
                        {
                            try { if (matrix[startRow, startCol - 4].color == 0 && matrix[startRow, startCol + 1].color == 0) { return true; } } catch { }
                        }
                        // X x x x
                        // Means it is the left piece
                        else
                        {
                            try { if (matrix[startRow, startCol + 4].color == 0 && matrix[startRow, startCol - 1].color == 0) { return true; } } catch { }
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
            if (checkForWin && pieceCount >= numOfPieces) { return true; }
            if (pieceCount == numOfPieces)
            {
                // Tria checks
                if (numOfPieces == 4)
                {
                    // [Start piece], [location on the board]

                    // Left, left edge                             
                    try { var x = matrix[startRow - 1, startCol]; } catch { return false; }
                    // Right, right edge
                    try { var x = matrix[startRow + 1, startCol]; } catch { return false; }

                    // Means piece is middle piece
                    if (matrix[startRow + 1, startCol].color == pieceColor && matrix[startRow - 1, startCol].color == pieceColor)
                    {
                        // Middle, any where on the board
                        try { var x = (matrix[startRow - 2, startCol].color == 0 && matrix[startRow + 2, startCol].color == 0); } catch { return false; }
                        try { if (matrix[startRow - 2, startCol].color == 0) { return true; } } // if 2 left of the piece is 0 return true
                        catch { if (matrix[startRow + 2, startCol].color == 0) { return true; } } // if checking 2 left of the piece is error means it is on the left edge so check right
                    }
                    // Means piece is not the middle piece
                    else
                    {
                        // Means it is the right piece
                        if (matrix[startRow - 1, startCol].color == pieceColor)
                        {
                            try { if (matrix[startRow - 3, startCol].color == 0 && matrix[startRow + 1, startCol].color == 0) { return true; } } catch { }

                        }
                        // Means it is the left piece
                        else
                        {
                            try { if (matrix[startRow + 3, startCol].color == 0 && matrix[startRow - 1, startCol].color == 0) { return true; } } catch { }
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
                    try { var x = matrix[startRow - 1, startCol]; } catch { return false; }
                    // Right, right edge
                    try { var x = matrix[startRow + 1, startCol]; } catch { return false; }

                    // Means piece is the middle two pieces
                    if (matrix[startRow + 1, startCol].color == pieceColor && matrix[startRow - 1, startCol].color == pieceColor)
                    {
                        // x X x x
                        //Means it is left middle
                        try
                        {
                            if (matrix[startRow + 2, startCol].color == pieceColor)
                            {
                                try { if (matrix[startRow + 3, startCol].color == 0 && matrix[startRow - 2, startCol].color == 0) { return true; } } catch { }
                            }
                        }
                        catch { }

                        // x x X x
                        //Means it is right middle
                        try
                        {
                            if (matrix[startRow + 2, startCol].color == pieceColor)
                            {
                                try { if (matrix[startRow - 3, startCol].color == 0 && matrix[startRow + 2, startCol].color == 0) { return true; } } catch { }
                            }
                        }
                        catch { }
                    }
                    // Means it is far left/right piece
                    else
                    {
                        // x x x X
                        // Means it is the right piece
                        if (matrix[startRow - 1, startCol].color == pieceColor)
                        {
                            try { if (matrix[startRow - 4, startCol].color == 0 && matrix[startRow + 1, startCol].color == 0) { return true; } } catch { }
                        }
                        // X x x x
                        // Means it is the left piece
                        else
                        {
                            try { if (matrix[startRow + 4, startCol].color == 0 && matrix[startRow - 1, startCol].color == 0) { return true; } } catch { }
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
            if (checkForWin && pieceCount >= numOfPieces) { return true; }
            if (pieceCount == numOfPieces)
            {
                // Tria checks
                if (numOfPieces == 4)
                {
                    // [Start piece], [location on the board]

                    // Left, top edge                           
                    try { var x = matrix[startRow - 1, startCol]; } catch { return false; }
                    // Right, bottom edge
                    try { var x = matrix[startRow + 1, startCol]; } catch { return false; }
                    // Left, left edge                           
                    try { var x = matrix[startRow, startCol-1]; } catch { return false; }
                    // Right, right edge
                    try { var x = matrix[startRow, startCol+1]; } catch { return false; }

                    // Means piece is middle piece
                    if (matrix[startRow + 1, startCol+1].color == pieceColor && matrix[startRow - 1, startCol - 1].color == pieceColor)
                    {
                        // Middle, any where on the board
                        try { var x = (matrix[startRow - 2, startCol - 2].color == 0 && matrix[startRow + 2, startCol + 2].color == 0); } catch { return false; }
                        try { if (matrix[startRow - 2, startCol - 2].color == 0) { return true; } } // if 2 left of the piece is 0 return true
                        catch { if (matrix[startRow + 2, startCol + 2].color == 0) { return true; } } // if checking 2 left of the piece is error means it is on the left edge so check right
                    }
                    // Means piece is not the middle piece
                    else
                    {
                        // Means it is the right piece
                        if (matrix[startRow - 1, startCol - 1].color == pieceColor)
                        {
                            try { if (matrix[startRow - 3, startCol - 3].color == 0 && matrix[startRow + 1, startCol + 1].color == 0) { return true; } } catch { }

                        }
                        // Means it is the left piece
                        else
                        {
                            try { if (matrix[startRow + 3, startCol + 3].color == 0 && matrix[startRow - 1, startCol - 1].color == 0) { return true; } } catch { }
                        }
                    }
                    //Safety
                    return false;
                }
                // Tesera check
                else
                {

                    // [Start piece], [location on the board]

                    // Left, top edge                           
                    try { var x = matrix[startRow - 1, startCol]; } catch { return false; }
                    // Right, bottom edge
                    try { var x = matrix[startRow + 1, startCol]; } catch { return false; }
                    // Left, left edge                           
                    try { var x = matrix[startRow, startCol - 1]; } catch { return false; }
                    // Right, right edge
                    try { var x = matrix[startRow, startCol + 1]; } catch { return false; }

                    // Means piece is the middle two pieces
                    if (matrix[startRow + 1, startCol + 1].color == pieceColor && matrix[startRow - 1, startCol - 1].color == pieceColor)
                    {
                        // x X x x
                        //Means it is left middle
                        try
                        {
                            if (matrix[startRow + 2, startCol + 2].color == pieceColor)
                            {
                                try { if (matrix[startRow + 3, startCol + 3].color == 0 && matrix[startRow - 2, startCol - 2].color == 0) { return true; } } catch { }
                            }
                        }
                        catch { }

                        // x x X x
                        //Means it is right middle
                        try
                        {
                            if (matrix[startRow + 2, startCol + 2].color == pieceColor)
                            {
                                try { if (matrix[startRow - 3, startCol - 3].color == 0 && matrix[startRow + 2, startCol + 2].color == 0) { return true; } } catch { }
                            }
                        }
                        catch { }
                    }
                    // Means it is far left/right piece
                    else
                    {
                        // x x x X
                        // Means it is the right piece
                        if (matrix[startRow - 1, startCol - 1].color == pieceColor)
                        {
                            try { if (matrix[startRow - 4, startCol - 4].color == 0 && matrix[startRow + 1, startCol + 1].color == 0) { return true; } } catch { }
                        }
                        // X x x x
                        // Means it is the left piece
                        else
                        {
                            try { if (matrix[startRow + 4, startCol + 4].color == 0 && matrix[startRow - 1, startCol - 1].color == 0) { return true; } } catch { }
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
                //// Diagonal (top right <--> bottom left)
                int c = startCol;
                for (int r = startRow; ((c - 1 >= -1 && c < matrix.GetLength(1)) && (r - 1 >= -1 && r < matrix.GetLength(0))); r -= prefix, c += prefix)
                {
                    if (matrix[r, c].color == pieceColor) { pieceCount++; } else { break; }
                }
                count++;
                prefix *= -1;
            }
            if (checkForWin && pieceCount >= numOfPieces) { return true; }
            if (pieceCount == numOfPieces)
            {
                // Tria checks
                if (numOfPieces == 4)
                {
                    // [Start piece], [location on the board]

                    // Left, top edge                           
                    try { var x = matrix[startRow - 1, startCol]; } catch { return false; }
                    // Right, bottom edge
                    try { var x = matrix[startRow + 1, startCol]; } catch { return false; }
                    // Left, left edge                           
                    try { var x = matrix[startRow, startCol - 1]; } catch { return false; }
                    // Right, right edge
                    try { var x = matrix[startRow, startCol + 1]; } catch { return false; }

                    // Means piece is middle piece
                    if (matrix[startRow + 1, startCol - 1].color == pieceColor && matrix[startRow - 1, startCol + 1].color == pieceColor)
                    {
                        // Middle, any where on the board
                        try { var x = (matrix[startRow - 2, startCol + 2].color == 0 && matrix[startRow + 2, startCol - 2].color == 0); } catch { return false; }
                        try { if (matrix[startRow - 2, startCol + 2].color == 0) { return true; } } // if 2 left of the piece is 0 return true
                        catch { if (matrix[startRow + 2, startCol - 2].color == 0) { return true; } } // if checking 2 left of the piece is error means it is on the left edge so check right
                    }
                    // Means piece is not the middle piece
                    else
                    {
                        // Means it is the right piece
                        if (matrix[startRow - 1, startCol + 1].color == pieceColor)
                        {
                            try { if (matrix[startRow - 3, startCol + 3].color == 0 && matrix[startRow + 1, startCol - 1].color == 0) { return true; } } catch { }

                        }
                        // Means it is the left piece
                        else
                        {
                            try { if (matrix[startRow + 3, startCol - 3].color == 0 && matrix[startRow - 1, startCol + 1].color == 0) { return true; } } catch { }
                        }
                    }
                    //Safety
                    return false;
                }
                // Tesera check
                else
                {

                    // [Start piece], [location on the board]

                    // Left, top edge                           
                    try { var x = matrix[startRow - 1, startCol]; } catch { return false; }
                    // Right, bottom edge
                    try { var x = matrix[startRow + 1, startCol]; } catch { return false; }
                    // Left, left edge                           
                    try { var x = matrix[startRow, startCol - 1]; } catch { return false; }
                    // Right, right edge
                    try { var x = matrix[startRow, startCol + 1]; } catch { return false; }

                    // Means piece is the middle two pieces
                    if (matrix[startRow + 1, startCol - 1].color == pieceColor && matrix[startRow - 1, startCol + 1].color == pieceColor)
                    {
                        // x X x x
                        //Means it is left middle
                        try
                        {
                            if (matrix[startRow + 2, startCol - 2].color == pieceColor)
                            {
                                try { if (matrix[startRow + 3, startCol - 3].color == 0 && matrix[startRow - 2, startCol + 2].color == 0) { return true; } } catch { }
                            }
                        }
                        catch { }

                        // x x X x
                        //Means it is right middle
                        try
                        {
                            if (matrix[startRow - 2, startCol + 2].color == pieceColor)
                            {
                                try { if (matrix[startRow - 3, startCol + 3].color == 0 && matrix[startRow + 2, startCol - 2].color == 0) { return true; } } catch { }
                            }
                        }
                        catch { }
                    }
                    // Means it is far left/right piece
                    else
                    {
                        // x x x X
                        // Means it is the right piece
                        if (matrix[startRow - 1, startCol + 1].color == pieceColor)
                        {
                            try { if (matrix[startRow - 4, startCol + 4].color == 0 && matrix[startRow + 1, startCol - 1].color == 0) { return true; } } catch { }
                        }
                        // X x x x
                        // Means it is the left piece
                        else
                        {
                            try { if (matrix[startRow + 4, startCol - 4].color == 0 && matrix[startRow - 1, startCol + 1].color == 0) { return true; } } catch { }
                        }
                    }

                    //Safety
                    return false;
                }
            }

            // Final safety
            return false;
        }


        public bool isCapture(ref Cell[,] matrix, int lastUsersSpotX, int lastUsersSpotY, int startRow, int startCol, int pieceColor)
        {
            // if current piece is not next to the last placed piece, no need to check
            if(Math.Abs(lastUsersSpotX-startRow) > 1 || Math.Abs(lastUsersSpotY - startCol) > 1) { return false; }

            // Getting x and y direction
            int xDir = lastUsersSpotX - startRow;
            int yDir = lastUsersSpotY - startCol;

            // Checking edge
            try { var x = matrix[startRow+(3*xDir), startCol+(3*yDir)]; } catch { return false; }

            // Means middle two pieces are of the same color so capture is true
            if((matrix[startRow + (1 * xDir), startCol + (1 * yDir)].color == matrix[startRow + (2 * xDir), startCol + (2 * yDir)].color)
                && matrix[startRow + (3 * xDir), startCol + (3 * yDir)].color == pieceColor)
            {
                // Removing middle pieces
                matrix[startRow + (1 * xDir), startCol + (1 * yDir)].clearCell();
                matrix[startRow + (2 * xDir), startCol + (2 * yDir)].clearCell();
                return true;
            }

            return false;
        }


        public bool aiMakeMove(ref Cell[,] matrix)
        {

            return true; // should be false
        }


    }


}
