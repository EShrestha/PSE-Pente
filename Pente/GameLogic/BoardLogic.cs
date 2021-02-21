using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

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
                            if (matrix[startRow, startCol - 2].color == pieceColor)
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
                            if (matrix[startRow - 2, startCol].color == pieceColor)
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


        public bool isCapture(ref Cell[,] matrix, int lastUsersSpotX, int lastUsersSpotY, int startRow, int startCol, int pieceColor, bool test = false)
        {
            if(lastUsersSpotX == startRow && lastUsersSpotY == startCol) { return false; }
            // if current piece is not next to the last placed piece, no need to check
            if (Math.Abs(lastUsersSpotX-startRow) > 1 || Math.Abs(lastUsersSpotY - startCol) > 1) { return false; }

            // Getting x and y direction
            int xDir = lastUsersSpotX - startRow;
            int yDir = lastUsersSpotY - startCol;

            // Checking edge
            try { var x = matrix[startRow+(3*xDir), startCol+(3*yDir)]; } catch { return false; }



                // Means middle two pieces are of the same color so capture is true
                if(matrix[startRow + (1 * xDir), startCol + (1 * yDir)].color != pieceColor && matrix[startRow + (2 * xDir), startCol + (2 * yDir)].color != pieceColor
                    && matrix[startRow + (3 * xDir), startCol + (3 * yDir)].color == pieceColor)
                {
                    if (!test)
                    {
                        matrix[startRow + (1 * xDir), startCol + (1 * yDir)].clearCell();
                        matrix[startRow + (2 * xDir), startCol + (2 * yDir)].clearCell();
                    }
                    // Removing middle pieces
                    return true;
                }


            return false;
        }



        public (int x, int y) aiMakeMove(ref Cell[,] matrix, int color,int lastX, int lastY, DispatcherTimer timer)
        {
            System.Diagnostics.Debug.WriteLine($"After enter 0,0 color is: {matrix[0, 0].color}");
            
            var colRange = (Left: 0, Right: 0);
            var rowRange = (Up: 0, Down: 0);
            colRange.Left = lastY;
            colRange.Right = matrix.GetLength(1) - lastY;            
            rowRange.Up = lastX;
            rowRange.Down = matrix.GetLength(0) - lastX;
            int enemyColor = matrix[lastX, lastY].color;

            // Tria prevention
            for (int rS = -1; rS <= 1; rS++)
            {
                for (int cS = -1; cS <= 1; cS++)
                {
                    if (rS != 0 || cS != 0)
                    {
                        try
                        {
                            if (matrix[lastX + (1 * rS), lastY + (1 * cS)].color == enemyColor
                                    && matrix[lastX + (2 * rS), lastY + (2 * cS)].color == enemyColor)
                            {
                                if (matrix[lastX + (3 * rS), lastY + (3 * cS)].color == 0)
                                {
                                    WriteAiMove(ref matrix, lastX + (3 * rS), lastY + (3 * cS), color, ref lastX, ref lastY, timer);
                                    return (lastX + (3 * rS), lastY + (3 * cS));
                                }
                                else if (matrix[lastX + (-1 * rS), lastY + (-1 * cS)].color == 0)
                                {
                                    WriteAiMove(ref matrix, lastX + (-1 * rS), lastY + (-1 * cS), color, ref lastX, ref lastY, timer);
                                    return (lastX + (-1 * rS), lastY + (-1 * cS));
                                }
                            }
                        }
                        catch { }
                    }
                }
            }

            // Tesera prevention
            for (int rS = -1; rS <= 1; rS++)
            {
                for (int cS = -1; cS <= 1; cS++)
                {
                    if (rS != 0 || cS != 0)
                    {
                        try
                        {
                            if (matrix[lastX + (1 * rS), lastY + (1 * cS)].color == enemyColor
                                && matrix[lastX + (2 * rS), lastY + (2 * cS)].color == enemyColor
                                && matrix[lastX + (3 * rS), lastY + (3 * cS)].color == enemyColor)
                            {
                                if (matrix[lastX + (4 * rS), lastY + (4 * cS)].color == 0)
                                {
                                    WriteAiMove(ref matrix, lastX + (4 * rS), lastY + (4 * cS), color, ref lastX, ref lastY, timer);
                                    return (lastX + (4 * rS), lastY + (4 * cS));
                                }
                                else if (matrix[lastX + (-1 * rS), lastY + (-1 * cS)].color == 0)
                                {
                                    WriteAiMove(ref matrix, lastX + (-1 * rS), lastY + (-1 * cS), color, ref lastX, ref lastY, timer);
                                    return (lastX + (-1 * rS), lastY + (-1 * cS));
                                }

                            }
                        }
                        catch { }
                    }
                }
            }

            // Win prevention
            for (int rS = -1; rS <= 1; rS++)
            {
                for (int cS = -1; cS <= 1; cS++)
                {
                    if (rS != 0 || cS != 0)
                    {
                        try
                        {
                            if (matrix[lastX + (1 * rS), lastY + (1 * cS)].color == enemyColor
                                && matrix[lastX + (2 * rS), lastY + (2 * cS)].color == enemyColor
                                && matrix[lastX + (3 * rS), lastY + (3 * cS)].color == enemyColor
                                && matrix[lastX + (4 * rS), lastY + (4 * cS)].color == enemyColor)
                            {
                                if(matrix[lastX + (5 * rS), lastY + (5 * cS)].color == 0)
                                {
                                    WriteAiMove(ref matrix, lastX + (3 * rS), lastY + (3 * cS), color, ref lastX, ref lastY, timer);
                                    return (lastX + (3 * rS), lastY + (3 * cS));
                                }
                                else if(matrix[lastX + (-1 * rS), lastY + (-1 * cS)].color == 0)
                                {
                                    WriteAiMove(ref matrix, lastX + (3 * rS), lastY + (3 * cS),  color, ref lastX, ref lastY, timer);
                                    return (lastX + (3 * rS), lastY + (3 * cS));
                                }
                                
                            }
                        }
                        catch { }
                    }
                }
            }


            timer.Stop();
            for (int row = lastX; row > 0; row--)
            {
                for(int col = lastY; col > 0; col--)
                {
                    System.Diagnostics.Debug.Write($"Checking [{row},{col}] = {matrix[row, col].color} ");
                    if (matrix[row, col].color == 0)
                    {
                        System.Diagnostics.Debug.Write($"WROTE IN HERE: [{row},{col}] = {matrix[row, col].color} ");
                        WriteAiMove(ref matrix, row, col, color, ref lastX, ref lastY, timer);
                        return (row, col);
                    }
                }
                System.Diagnostics.Debug.WriteLine("C\n");
            }
            timer.Start();
            return (-1, -1); // Means Ai failed to find a move


        }

        // Writes the move of the ai to the board
        void WriteAiMove(ref Cell[,] matrix, int row, int col, int color, ref int lastX, ref int lastY, DispatcherTimer timer)
        {
            string clr = (color == 1 ? "White" : (color == 2) ? "Black" : (color == 3) ? "Green" : (color == 4) ? "Blue" : "");
            matrix[row, col].color = color;
            System.Diagnostics.Debug.WriteLine($"[{row},{col}] has the color {matrix[row, col].color}");
            matrix[row, col].btn.Content = new Image
            {
                Source = new BitmapImage(new Uri($"/Resources/cross{clr}.png", UriKind.RelativeOrAbsolute)),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Stretch = Stretch.Fill,
            };
            matrix[row, col].btn.IsEnabled = false;
            lastX = row;
            lastY = col;
            timer.Start();
  
        }

        public bool saveGame(ref Cell[,] matrix, List<Player> playersList, Player currentPlayer, int lastUsersSpotX, int lastUsersSpotY, string saveGamePath, bool test = false)
        {
            try
            {
                // Save files name is the current time
                string time = DateTime.Now.ToString("T");

                time = time.Replace(':', '-');

                // Creates a GameSave object to be serialized
                GameSave gs = new GameSave(ref matrix, playersList, currentPlayer, lastUsersSpotX, lastUsersSpotY);

                IFormatter formatter = new BinaryFormatter();
                System.IO.Directory.CreateDirectory(@"\PenteGames");
                string path = saveGamePath.Equals("") ? @$"\PenteGames\{time}.pente" : saveGamePath;
                Stream stream = new FileStream(@$"{path}", FileMode.Create, FileAccess.Write);

                formatter.Serialize(stream, gs);
                stream.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }


    }


}
