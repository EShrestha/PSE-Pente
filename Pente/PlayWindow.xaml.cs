using Pente.GameLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pente
{
    /// <summary>
    /// Interaction logic for PlayWindow.xaml
    /// </summary>
    /// 

    
    // Cell is the actual cell on the board the player is able to click
    [Serializable]
    public struct Cell
    {
        // Each cell has a button and what color is on that cell, when color is 0 it means it is blank
        public Button btn; 
        public int color;

        // When a new cell is created a button must be passed in, the color will be blank by default
        public Cell(Button btn, Byte color = 0)
        {
            this.btn = btn;
            this.color = color;
        }

        // When called, resets cell so it is blank
        public void clearCell()
        {
            this.color = 0;
            // Reseting images so no piece is on it
            btn.Content = new Image
            {
                Source = new BitmapImage(new Uri("/Resources/cross.png", UriKind.RelativeOrAbsolute)),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Stretch = Stretch.Fill,
            };
            this.btn.IsEnabled = true; // Making teh cell clickable again
        }
    }


    public partial class PlayWindow : Window
    {
        Window mainWindow; // What window it came from
        DispatcherTimer timer; // Used for timing each players turn
        private int numOfPlayers; // Holds how many players are in this current game

        // The numbers related to creating a 19x19 board
        private const int SCALE = 39;
        private const int WIDTH = 741; // Width of the board
        private const int HEIGHT = 741; // Height of the board
        public const int wDs = WIDTH / SCALE; // Width divided by scale
        public const int hDs = HEIGHT / SCALE; // Height divided by scale
        public int maxPixel = hDs;

        public byte turn = 1; // Keeps track of players turn
        BoardLogic boardLogic = new BoardLogic(); // Board logic holds all of the logic of the game such as tria checks, tesera checks...

        // Making a new board
        Board b = new Board(hDs, wDs);
        public Cell[,] matrix;

        int lastUsersSpotX; // Keeps track of the last players x coordinate
        int lastUsersSpotY; // Keeps track of the last players y coordinate
        int maxTurnTime = 30; // How many seconds each turn has
        int turnSecondsElapsed; // Keeps track of how much time has elapsed
        List<Player> playersList; // Has a list of all of the players in the current game with all of their details
        Player currentPlayer; // Holds the player who is up
        bool gameFinished = false; // Is the game active or has it ended?
        string saveGamePath = ""; // Used if the user chooses to open a save file, ensures the old file is written over if they save it again

     
        public PlayWindow(List<Player> players, Window sentWindow=null)
        {
            mainWindow = sentWindow;
            numOfPlayers = players.Count;
            playersList = players;
            currentPlayer = playersList[0];
            matrix = b.getBoard();

            InitializeComponent();
            setupBoard();

        }

        public PlayWindow(GameSave gs, string filePath = "", Window sentWindow = null)
        {
            // Setting all of the variables required for the game
            mainWindow = sentWindow;
            playersList = gs.playersList;
            currentPlayer = gs.currentPlayer;
            saveGamePath = filePath;
            lastUsersSpotX = gs.lastX;
            lastUsersSpotY = gs.lastY;

            // Getting fresh board
            matrix = b.getBoard();
            InitializeComponent();
            setupBoard(); // Setting up the board
            setupBoardFromSave(gs.matrix); // Updating the board based on the game save file

            // Updating graphic that show the color of the current player
            int spot = currentPlayer.color;
            string color = (spot == 1 ? "White" : (spot == 2) ? "Black" : (spot == 3) ? "Green" : (spot == 4) ? "Blue" : "");
            turnImage.Source = new BitmapImage(new Uri(@$"/Resources/{color}.png", UriKind.Relative)); 
            txtCaptures.Content = $"Captures: {currentPlayer.numOfCaptures}";

        }

        // For tests only
        public PlayWindow()
        {
            matrix = b.getBoard();
            InitializeComponent();
            setupBoard();
        }


        // If user wants to load a save game, this function is called so that
        // the all of the save information is displayed correctly in the play window board
        public void setupBoardFromSave(int[,] mat)
        {

            // Goes through each spot on the board and copies every detail that was in the precious save files board
            for(int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    if(mat[r,c] != 0)
                    {
                        int spot = mat[r, c];
                        //System.Diagnostics.Debug.WriteLine($"At {r},{c} color was {spot}");
                        string color = (spot == 1 ? "White" : (spot == 2) ? "Black" : (spot == 3) ? "Green" : (currentPlayer.color == 4) ? "Blue" : "");
                        matrix[r, c].color = spot;
                        matrix[r,c].btn.Content = new Image
                        {
                            Source = new BitmapImage(new Uri($"/Resources/cross{color}.png", UriKind.RelativeOrAbsolute)),
                            VerticalAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Stretch = Stretch.Fill,
                        };
                        matrix[r, c].btn.IsEnabled = false;
                    }
                }
            }
        }

        // Respoinsible for populating the 19x19 board with Cells and making sure each Cell has a button and picture assigned
        public void setupBoard()
        {
            // Adding rows and columns in the grid
            for (int i = 0; i < hDs; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < wDs; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());

            }

            // Actually adding the buttons to the grid
            int btnCount = 1;
            Style style = this.FindResource("btnStyle1") as Style; // Getting style template for buttons
            for (int i = 0; i < hDs; i++)
            {
                for (int j = 0; j < wDs; j++)
                {
                    Button btn = new Button();
                    btn.Click += btn_Event;
                    //btn.MouseEnter += btn_Event;

                    btn.FontSize = SCALE / 3;
                    btn.Background = Brushes.Wheat;
                    btn.Name = "Button" + btnCount.ToString();
                    btn.FontSize = 85;
                    btn.Style = style;

                    // Pente has cetratin cells that are marked to make it easier for players to see, such as the middle button
                    if (( (i == 3 | i==9 | i==15 ) && (j == 3 | j == 9 | j == 15)) | (i==9 && j==9))
                    {
                        btn.Content = new Image
                        {
                            Source = new BitmapImage(new Uri("/Resources/crossSpecial.png", UriKind.RelativeOrAbsolute)),
                            VerticalAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Stretch = Stretch.Fill,
                        };
                    }
                    else
                    {
                        btn.Content = new Image
                        {
                            Source = new BitmapImage(new Uri("/Resources/cross.png", UriKind.RelativeOrAbsolute)),
                            VerticalAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Stretch = Stretch.Fill,
                        };
                    }

                    // Creating a new cell with the button
                    Cell cell = new Cell(btn);
                    // Adding cell to the matrix location
                    matrix[i, j] = cell;

                    // Finally adding the button on the grid so the player can see
                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);
                    grid.Children.Add(btn);

                    btnCount++;
                }
            }

            // Every thing needed for the timer to work
            turnSecondsElapsed = maxTurnTime;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000); // 1 second per tick
            timer.IsEnabled = true;
            timer.Tick += new EventHandler(onTime); // Every tick, call the onTime method
        }

        //Event that runs for any button on the board that is clicked
        private void btn_Event(object sender, RoutedEventArgs e)
        {
            if (gameFinished) return; // If the game has already ended, users can't place any more pieces
            Button cell = (sender as Button);
            // Getting the clicked buttons coordinates
            int row = Grid.GetRow(cell);
            int col = Grid.GetColumn(cell);

            // If the current player is not and ai and the button is free
            if (matrix[row,col].color==0 && !currentPlayer.isAi) // 0 means no color
            {
                // Getting current player information
                string name = currentPlayer.name;
                int colorNum = currentPlayer.color;
                string color = (currentPlayer.color == 1 ? "White" : (currentPlayer.color == 2) ? "Black" : (currentPlayer.color == 3) ? "Green" : (currentPlayer.color == 4) ? "Blue" : ""); // Setting color
                                                                                                                                                                                              //System.Diagnostics.Debug.WriteLine($"Current player color: {turn} - {color}");

                this.matrix[row,col].color = colorNum; // Players turn number represents color

                matrix[row, col].btn.Content = new Image
                {
                    Source = new BitmapImage(new Uri($"/Resources/cross{color}.png", UriKind.RelativeOrAbsolute)),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Stretch = Stretch.Fill,
                };


                // Doning checks for capture, win, tesera, and tria
                if (boardLogic.isCapture(ref matrix, lastUsersSpotX, lastUsersSpotY, row, col, colorNum)) { currentPlayer.numOfCaptures++; if(currentPlayer.numOfCaptures >=5 ) { timer.Stop();  MessageBox.Show($"{name} won the game with 5 captures!!!"); postWin(); } }
                if(boardLogic.xPiecesInSuccession(matrix,row,col, colorNum, 5, true)) { timer.Stop(); MessageBox.Show($"{name} won the game!!!"); postWin(); } // Checkin for game win;
                else if(boardLogic.xPiecesInSuccession(matrix,row,col, colorNum, 4)) { MessageBox.Show($"{name} has a tesera!"); } // Checkin for tesera;
                else if(boardLogic.xPiecesInSuccession(matrix,row,col, colorNum, 3)) { MessageBox.Show($"{name} has a tria!"); } // Checkin for tria;

                // Disabling the button once the user has clicked the button
                matrix[row, col].btn.IsEnabled = false;

                // Keeping track of what x and y coordinate the current player chose
                lastUsersSpotX = row;
                lastUsersSpotY = col;
                turnSecondsElapsed = maxTurnTime; // resetting the timer

                timer.Stop();
                if (!gameFinished) {updateCurrentPlayer(); } // Advancing onto next player
                
                // Updating the graphic for the next player, which will now become the current player
                string colorNext = (currentPlayer.color == 1 ? "White" : (currentPlayer.color == 2) ? "Black" : (currentPlayer.color == 3) ? "Green" : (currentPlayer.color == 4) ? "Blue" : "");
                turnImage.Source = new BitmapImage(new Uri(@$"/Resources/{colorNext}.png", UriKind.Relative));
                txtCaptures.Content = $"Captures: {currentPlayer.numOfCaptures}";

            }

        }


        // Excuted after a player has won
        void postWin()
        {
            timer.Stop();
            gameFinished = true;
        }


        void updateCurrentPlayer()
        {
            timer.Start();
            // If last player, make next player the first player on the list
            if (playersList.IndexOf(currentPlayer) + 1 == playersList.Count)
            {
                currentPlayer = playersList[0];
            }
            else // Means not the last player so just move up the list
            {
                currentPlayer = playersList[playersList.IndexOf(currentPlayer) + 1];
            }
        }


        // Is executed every time the timer ticks
        public void onTime(object sender, EventArgs e)
        {
            // Checks if the current player is an AI
            if (currentPlayer.isAi)
            {
                (int x, int y) = boardLogic.aiMakeMove(ref matrix, currentPlayer.color, lastUsersSpotX, lastUsersSpotY, timer);
                if (x + y != -38) // If not true, error has occured
                {
                    // Doning checks for capture, win, tesera, and tria for ai
                    if (boardLogic.isCapture(ref matrix, lastUsersSpotX, lastUsersSpotY, x, y, currentPlayer.color)) { currentPlayer.numOfCaptures++; if (currentPlayer.numOfCaptures >= 5) { timer.Stop(); MessageBox.Show($"{currentPlayer.name} won the game with 5 captures!!!"); postWin(); } }
                    if (boardLogic.xPiecesInSuccession(matrix, x, y, currentPlayer.color, 5, true)) { timer.Stop(); MessageBox.Show($"{currentPlayer.name} won the game!!!"); postWin(); } // Checkin for game win;
                    else if (boardLogic.xPiecesInSuccession(matrix, x, y, currentPlayer.color, 4)) { timer.Stop(); MessageBox.Show($"{currentPlayer.name} has a tesera!"); } // Checkin for tesera;
                    else if (boardLogic.xPiecesInSuccession(matrix, x, y, currentPlayer.color, 3)) { timer.Stop(); MessageBox.Show($"{currentPlayer.name} has a tria!"); } // Checkin for tria;

                    //matrix[x, y].btn.IsEnabled = false;

                    // Keeping track of coordinates
                    //lastUsersSpotX = x;
                    //lastUsersSpotY = y;
                    turnSecondsElapsed = maxTurnTime;

                    if (!gameFinished) {  updateCurrentPlayer();}
                   
                    string colorNxt = (currentPlayer.color == 1 ? "White" : (currentPlayer.color == 2) ? "Black" : (currentPlayer.color == 3) ? "Green" : (currentPlayer.color == 4) ? "Blue" : "");
                    turnImage.Source = new BitmapImage(new Uri(@$"/Resources/{colorNxt}.png", UriKind.Relative));
                    txtCaptures.Content = $"Captures: {currentPlayer.numOfCaptures}";


                }
                else
                {
                    timer.Stop();
                    updateCurrentPlayer();
                    MessageBox.Show("Something went wrong with the AI");

                }
            }

            // Updating the graphic so the users know how much time they have left
            txtTimer.Content = $"{turnSecondsElapsed--}"; 
            if(turnSecondsElapsed < 0) // Means the current player ran out of time
            {
                MessageBox.Show($"{currentPlayer.name} missed their turn :(");
                //// Not needed if just skipping turn if timer runs out 
                //Player temp;
                //if (playersList.IndexOf(currentPlayer) + 1 == playersList.Count)
                //{
                //    temp = playersList[0];
                //}
                //else
                //{
                //    temp = playersList[playersList.IndexOf(currentPlayer) + 1];
                //}
                //playersList.Remove(currentPlayer);
                //currentPlayer = temp;
                updateCurrentPlayer();
               
                if (playersList.Count == 1)
                {
                    MessageBox.Show($"{playersList[0].name} won the game!!!");
                    postWin();
                }

                // Reseting the timer
                turnSecondsElapsed = maxTurnTime;
            }
        }

        // Asks if the user would like to quit, if so would they like to save the current game
        bool askToClose()
        {
            if (MessageBox.Show("Do you really want to exit?", "Yes", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if(MessageBox.Show("Would you like to save the game?", "Save", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // If user want to save game, a saveGame object is created and serialized 
                    if (boardLogic.saveGame(ref matrix,playersList,currentPlayer,lastUsersSpotX,lastUsersSpotY,saveGamePath)) { MessageBox.Show("Game saved!"); }
                }

                timer.Stop();
                return true;
            }
            return false;
        }


        


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Only ask to save if the game is not yet finished
            if (!gameFinished)
            {
                if (!askToClose()) { e.Cancel = true; }
                else { mainWindow.Show(); }
            }
            else
            {
                mainWindow.Show();
            }
            
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // Clearing the players from the list
            playersList.Clear();
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            // Required to compile
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            // Required to compile
        }

    }
}
