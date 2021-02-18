using NUnit.Framework;
using Pente.GameLogic;
using System.Collections.Generic;

namespace Pente.NUnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        // Tests if the board is 
        [Test]
        public void is19x19_19x19_true()
        {
            // Arrange
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            // Act
            int numOfRows = mat.GetLength(0);
            int numOfCols = mat.GetLength(1);


            // Assert
            Assert.AreEqual(19, numOfRows);
            Assert.AreEqual(19, numOfCols);
        }


        public void aiMakeMove__false()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();

            // Act
            (int x, int y) = bl.aiMakeMove(ref mat, 2, -2, -2, new System.Windows.Threading.DispatcherTimer());

            // Assert
            Assert.AreEqual(x, -1);
            Assert.AreEqual(y, -1);
        }


        // Tests if there is a tria
        [Test]
        public void xPiecesInSuccession_3Horizontal_true()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[1, 1].color = 1; // Adding white(1) piece to row 0 col 0
            mat[1, 2].color = 1;
            mat[1, 3].color = 1;
            int startRow = 1;
            int startCol = 1;

            // Act
            bool isTria = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 3);

            // Assert
            Assert.AreEqual(true, isTria);
        }

        [Test]
        public void xPiecesInSuccession_3Horizontal_false()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[1, 1].color = 1; // Adding white(1) piece to row 0 col 0
            mat[1, 2].color = 1;
            mat[1, 3].color = 2;
            int startRow = 1;
            int startCol = 2;

            // Act
            bool isTria = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 3);

            // Assert
            Assert.AreEqual(false, isTria);
        }

        [Test]
        public void xPiecesInSuccession_3Vertical_true()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[1, 1].color = 1; // Adding white(1) piece to row 0 col 0
            mat[2, 1].color = 1;
            mat[3, 1].color = 1;
            int startRow = 1;
            int startCol = 1;

            // Act
            bool isTria = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 3);

            // Assert
            Assert.AreEqual(true, isTria);
        }

        [Test]
        public void xPiecesInSuccession_3Vertical_false()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[1, 1].color = 1; // Adding white(1) piece to row 0 col 0
            mat[2, 1].color = 1;
            mat[3, 1].color = 1;
            int startRow = 1;
            int startCol = 2;

            // Act
            bool isTria = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 3);

            // Assert
            Assert.AreEqual(false, isTria);
        }

        [Test]
        public void xPiecesInSuccession_3Diagonal_true()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[1, 1].color = 1; // Adding white(1) piece to row 0 col 0
            mat[2, 2].color = 1;
            mat[3, 3].color = 1;
            int startRow = 1;
            int startCol = 1;

            // Act
            bool isTria = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 3);

            // Assert
            Assert.AreEqual(true, isTria);
        }

        [Test]
        public void xPiecesInSuccession_3Diagonal_false()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[1, 1].color = 1; // Adding white(1) piece to row 0 col 0
            mat[2, 2].color = 1;
            mat[3, 3].color = 1;
            int startRow = 1;
            int startCol = 2;

            // Act
            bool isTria = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 3);

            // Assert
            Assert.AreEqual(false, isTria);
        }

        [Test]
        public void xPiecesInSuccession_4Horizontal_true()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[1, 1].color = 1; // Adding white(1) piece to row 0 col 0
            mat[1, 2].color = 1;
            mat[1, 3].color = 1;
            mat[1, 4].color = 1;
            int startRow = 1;
            int startCol = 1;

            // Act
            bool isTesera = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 4);

            // Assert
            Assert.AreEqual(true, isTesera);
        }

        [Test]
        public void xPiecesInSuccession_4Horizontal_false()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[1, 1].color = 1; // Adding white(1) piece to row 0 col 0
            mat[1, 2].color = 1;
            mat[1, 3].color = 1;
            mat[1, 4].color = 2;
            int startRow = 1;
            int startCol = 1;

            // Act
            bool isTesera = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 4);

            // Assert
            Assert.AreEqual(false, isTesera);
        }


        [Test]
        public void xPiecesInSuccession_4Vertical_true()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[1, 1].color = 1; // Adding white(1) piece to row 0 col 0
            mat[2, 1].color = 1;
            mat[3, 1].color = 1;
            mat[4, 1].color = 1;
            int startRow = 1;
            int startCol = 1;

            // Act
            bool isTesera = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 4);

            // Assert
            Assert.AreEqual(true, isTesera);
        }

        [Test]
        public void xPiecesInSuccession_4Vertical_false()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[1, 1].color = 1; // Adding white(1) piece to row 0 col 0
            mat[2, 1].color = 1;
            mat[3, 1].color = 1;
            mat[4, 1].color = 2;
            int startRow = 1;
            int startCol = 1;

            // Act
            bool isTesera = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 4);

            // Assert
            Assert.AreEqual(false, isTesera);
        }

        [Test]
        public void xPiecesInSuccession_4Diagonal_true()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[1, 1].color = 1; // Adding white(1) piece to row 0 col 0
            mat[2, 2].color = 1;
            mat[3, 3].color = 1;
            mat[4, 4].color = 1;
            int startRow = 1;
            int startCol = 1;

            // Act
            bool isTesera = bl.xPiecesInSuccession(mat, startRow, startCol, 1,4);

            // Assert
            Assert.AreEqual(true, isTesera);
        }

        public void xPiecesInSuccession_4Diagonal_false()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[1, 1].color = 1; // Adding white(1) piece to row 0 col 0
            mat[2, 2].color = 1;
            mat[3, 3].color = 1;
            mat[4, 4].color = 2;
            int startRow = 1;
            int startCol = 1;

            // Act
            bool isTesera = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 4);

            // Assert
            Assert.AreEqual(false, isTesera);
        }

        [Test]
        public void isCapture_capDiagonal_true()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[1, 1].color = 1; // Adding white(1) piece to row 0 col 0
            mat[2, 2].color = 2;
            mat[3, 3].color = 2;
            mat[4, 4].color = 1;
            int lastX = 3;
            int lastY = 3;
            int startRow = 4;
            int startCol = 4;

            // Act
           bool isCap = bl.isCapture(ref mat, lastX, lastY, startRow, startCol, 1, true);

            // Assert
           Assert.AreEqual(true, isCap);
        }

        [Test]
        public void isCapture_capVertical_true()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[1, 0].color = 1; // Adding white(1) piece to row 0 col 0
            mat[2, 0].color = 2;
            mat[3, 0].color = 2;
            mat[4, 0].color = 1;
            int lastX = 3;
            int lastY = 0;
            int startRow = 4;
            int startCol = 0;

            // Act
            bool isCap = bl.isCapture(ref mat, lastX, lastY, startRow, startCol, 1, true);

            // Assert
            Assert.AreEqual(true, isCap);
        }

        [Test]
        public void isCapture_capHorizontal_true()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[0, 1].color = 1; // Adding white(1) piece to row 0 col 0
            mat[0, 2].color = 2;
            mat[0, 3].color = 2;
            mat[0, 4].color = 1;
            int lastX = 0;
            int lastY = 3;
            int startRow = 0;
            int startCol = 4;

            // Act
            bool isCap = bl.isCapture(ref mat, lastX, lastY, startRow, startCol, 1, true);

            // Assert
            Assert.AreEqual(true, isCap);
        }

        [Test]
        public void isCapture_capDiagonal_false()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[1, 1].color = 1; // Adding white(1) piece to row 0 col 0
            mat[2, 2].color = 2;
            mat[3, 3].color = 2;
            mat[4, 4].color = 1;
            int lastX = 3;
            int lastY = 3;
            int startRow = 3;
            int startCol = 3;

            // Act
            bool isCap = bl.isCapture(ref mat, lastX, lastY, startRow, startCol, 1, true);

            // Assert
            Assert.AreEqual(false, isCap);
        }

        [Test]
        public void isCapture_capVertical_false()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[1, 0].color = 1; // Adding white(1) piece to row 0 col 0
            mat[2, 0].color = 2;
            mat[3, 0].color = 2;
            mat[4, 0].color = 1;
            int lastX = 3;
            int lastY = 0;
            int startRow = 3;
            int startCol = 0;

            // Act
            bool isCap = bl.isCapture(ref mat, lastX, lastY, startRow, startCol, 1, true);

            // Assert
            Assert.AreEqual(false, isCap);
        }

        [Test]
        public void isCapture_capHorizontal_false()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[0, 1].color = 1; // Adding white(1) piece to row 0 col 0
            mat[0, 2].color = 2;
            mat[0, 3].color = 2;
            mat[0, 4].color = 1;
            int lastX = 0;
            int lastY = 3;
            int startRow = 0;
            int startCol = 3;

            // Act
            bool isCap = bl.isCapture(ref mat, lastX, lastY, startRow, startCol, 1, true);

            // Assert
            Assert.AreEqual(false, isCap);
        }


        [Test]
        public void xPiecesInSuccession_winHorizontal_true()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[0, 0].color = 1; // Adding white(1) piece to row 0 col 0
            mat[0, 1].color = 1;
            mat[0, 2].color = 1;
            mat[0, 3].color = 1;
            mat[0, 4].color = 1;
            int startRow = 0;
            int startCol = 0;

            // Act
            bool isWin = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 5, true);

            // Assert
            Assert.AreEqual(true, isWin);
        }

        [Test]
        public void xPiecesInSuccession_winHorizontal_false()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[0, 0].color = 1; // Adding white(1) piece to row 0 col 0
            mat[0, 1].color = 1;
            mat[0, 2].color = 1;
            mat[0, 3].color = 1;
            mat[0, 4].color = 2;
            int startRow = 0;
            int startCol = 0;

            // Act
            bool isWin = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 5, true);

            // Assert
            Assert.AreEqual(false, isWin);
        }

        [Test]
        public void xPiecesInSuccession_winVertical_true()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[0, 0].color = 1; // Adding white(1) piece to row 0 col 0
            mat[1, 0].color = 1;
            mat[2, 0].color = 1;
            mat[3, 0].color = 1;
            mat[4, 0].color = 1;
            int startRow = 0;
            int startCol = 0;

            // Act
            bool isWin = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 5, true);

            // Assert
            Assert.AreEqual(true, isWin);
        }

        [Test]
        public void xPiecesInSuccession_winVertical_false()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[0, 0].color = 1; // Adding white(1) piece to row 0 col 0
            mat[1, 0].color = 1;
            mat[2, 0].color = 1;
            mat[3, 0].color = 1;
            mat[4, 0].color = 2;
            int startRow = 0;
            int startCol = 0;

            // Act
            bool isWin = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 5, true);

            // Assert
            Assert.AreEqual(false, isWin);
        }

        [Test]
        public void xPiecesInSuccession_winDiagonal_true()
        {

            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[1, 1].color = 1; // Adding white(1) piece to row 0 col 0
            mat[2, 2].color = 1;
            mat[3, 3].color = 1;
            mat[4, 4].color = 1;
            mat[5, 5].color = 1;
            int startRow = 1;
            int startCol = 1;

            // Act
            bool isWin = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 5, true);

            // Assert
            Assert.AreEqual(true, isWin);
        }

        [Test]
        public void xPiecesInSuccession_winDiagonal_false()
        {

            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[1, 1].color = 1; // Adding white(1) piece to row 0 col 0
            mat[2, 2].color = 1;
            mat[3, 3].color = 1;
            mat[4, 4].color = 1;
            mat[5, 5].color = 2;
            int startRow = 1;
            int startCol = 1;

            // Act
            bool isWin = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 5, true);

            // Assert
            Assert.AreEqual(false, isWin);
        }

        [Test]
        public void serializeboard_success_true()
        {
            //arrange
            //setup board with a pretty random arrangement of pieces
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            List<Player> playerslist = new List<Player>();
            playerslist.Add(new Player("a", 1, 3));
            playerslist.Add(new Player("b", 2, 2, false, false));
            Cell[,] matrix = b.getBoard();
            matrix[0, 0].color = 1;
            matrix[5, 7].color = 3;
            matrix[0, 13].color = 3;
            matrix[11, 2].color = 3;
            matrix[10, 10].color = 2;
            matrix[4, 9].color = 4;
            matrix[4, 5].color = 1;
            matrix[4, 6].color = 4;
            matrix[4, 11].color = 2;
            matrix[4, 0].color = 2;
            matrix[4, 3].color = 3;

            //act
            bool isSaved = bl.saveGame(ref matrix, playerslist, new Player("a", 1, 3),4,3,"",true);

            //assert
            Assert.AreEqual(true, isSaved);
        }
    }
}