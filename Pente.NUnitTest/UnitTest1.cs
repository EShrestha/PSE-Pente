using NUnit.Framework;
using Pente.GameLogic;

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

        // Tests if the ai made a valid move
        [Test]
        public void aiMakeMove__true()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();

            // Act
            //bool isValid = bl.aiMakeMove(ref mat);

            // Assert
            //Assert.AreEqual(true, isValid);
        }


        // Tests if there is a tria
        [Test]
        public void xPiecesInSuccession_3Horizontal_true()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[0, 0].color = 1; // Adding white(1) piece to row 0 col 0
            mat[0, 1].color = 1;
            mat[0, 2].color = 1;
            int startRow = 0;
            int startCol = 0;

            // Act
            bool isTria = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 3);

            // Assert
            Assert.AreEqual(true, isTria);
        }

        [Test]
        public void xPiecesInSuccession_3Vertical_true()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[0, 0].color = 1; // Adding white(1) piece to row 0 col 0
            mat[1, 0].color = 1;
            mat[2, 0].color = 1;
            int startRow = 0;
            int startCol = 0;

            // Act
            bool isTria = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 3);

            // Assert
            Assert.AreEqual(true, isTria);
        }

        [Test]
        public void xPiecesInSuccession_3Diagonal_true()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[0, 0].color = 1; // Adding white(1) piece to row 0 col 0
            mat[1, 1].color = 1;
            mat[2, 2].color = 1;
            int startRow = 0;
            int startCol = 0;

            // Act
            bool isTria = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 3);

            // Assert
            Assert.AreEqual(true, isTria);
        }

        [Test]
        public void xPiecesInSuccession_4Horizontal_true()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[0, 0].color = 1; // Adding white(1) piece to row 0 col 0
            mat[0, 1].color = 1;
            mat[0, 2].color = 1;
            mat[0, 3].color = 1;
            int startRow = 0;
            int startCol = 0;

            // Act
            bool isTesera = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 4);

            // Assert
            Assert.AreEqual(true, isTesera);
        }

        [Test]
        public void xPiecesInSuccession_4Vertical_true()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[0, 0].color = 1; // Adding white(1) piece to row 0 col 0
            mat[1, 0].color = 1;
            mat[2, 0].color = 1;
            mat[3, 0].color = 1;
            int startRow = 0;
            int startCol = 0;

            // Act
            bool isTesera = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 4);

            // Assert
            Assert.AreEqual(true, isTesera);
        }

        [Test]
        public void xPiecesInSuccession_4Diagonal_true()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[0, 0].color = 1; // Adding white(1) piece to row 0 col 0
            mat[1, 1].color = 1;
            mat[2, 2].color = 1;
            mat[3, 3].color = 1;
            int startRow = 0;
            int startCol = 0;

            // Act
            bool isTesera = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 4);

            // Assert
            Assert.AreEqual(true, isTesera);
        }




        [Test]
        public void isCapture_capHorizontal_true()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[0, 0].color = 1; // Adding white(1) piece to row 0 col 0
            mat[0, 1].color = 2;
            mat[0, 2].color = 2;
            mat[0, 3].color = 1;
            int startRow = 0;
            int startCol = 3;

            // Act
            //bool isCap = bl.isCapture(ref mat, startRow, startCol, 1);

            // Assert
            //Assert.AreEqual(true, isCap);
        }
        [Test]
        public void isCapture_capVertical_true()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[0, 0].color = 1; // Adding white(1) piece to row 0 col 0
            mat[1, 0].color = 2;
            mat[2, 0].color = 2;
            mat[3, 0].color = 1;
            int startRow = 3;
            int startCol = 0;

            // Act
            //bool isCap = bl.isCapture(ref mat, startRow, startCol, 1);

            // Assert
            // Assert.AreEqual(true, isCap);
        }

        [Test]
        public void isCapture_capDiagonal_true()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[0, 0].color = 1; // Adding white(1) piece to row 0 col 0
            mat[1, 1].color = 2;
            mat[2, 2].color = 2;
            mat[3, 3].color = 1;
            int startRow = 3;
            int startCol = 3;

            // Act
            // bool isCap = bl.isCapture(ref mat, startRow, startCol, 1);

            // Assert
            // Assert.AreEqual(true, isCap);
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
            bool isWin = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 5);

            // Assert
            Assert.AreEqual(true, isWin);
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
            bool isWin = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 5);

            // Assert
            Assert.AreEqual(true, isWin);
        }

        [Test]
        public void xPiecesInSuccession_winDiagonal_true()
        {
            // Arrange
            BoardLogic bl = new BoardLogic();
            Board b = new Board(19, 19);
            Cell[,] mat = b.getBoard();
            mat[0, 0].color = 1; // Adding white(1) piece to row 0 col 0
            mat[1, 1].color = 1;
            mat[2, 2].color = 1;
            mat[3, 3].color = 1;
            mat[4, 4].color = 1;
            int startRow = 0;
            int startCol = 0;

            // Act
            bool isWin = bl.xPiecesInSuccession(mat, startRow, startCol, 1, 5);

            // Assert
            Assert.AreEqual(true, isWin);
        }

        [Test]
        public void serializeBoard_successful_true()
        {
            PlayWindow w = new PlayWindow();
            w.setupBoard();
            w.matrix[0, 0].color = 2;
            w.matrix[4, 5].color = 1;
            w.matrix[4, 4].color = 4;
            w.matrix[4, 6].color = 1;
            w.matrix[13, 4].color = 3;
            w.matrix[3, 11].color = 2;
            w.matrix[6, 6].color = 3;
            w.matrix[7, 6].color = 3;
            w.matrix[8, 6].color = 3;

            bool isSaved = w.saveGame();

            Assert.AreEqual(true, )
        }


    }
}