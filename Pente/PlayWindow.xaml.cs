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

    [Serializable]
    public struct Cell
    {
        public Button btn;
        public int color;

        public Cell(Button btn, Byte color = 0)
        {
            this.btn = btn;
            this.color = color;

        }

        public void clearCell()
        {
            this.color = 0;
            btn.Content = new Image
            {
                Source = new BitmapImage(new Uri("/Resources/cross.png", UriKind.RelativeOrAbsolute)),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Stretch = Stretch.Fill,
            };
            this.btn.IsEnabled = true;
        }
    }


    public partial class PlayWindow : Window
    {
        Window mainWindow;
        DispatcherTimer timer;
        private int numOfPlayers;
        private const int SCALE = 39;
        private const int WIDTH = 741;
        private const int HEIGHT = 741;
        public const int wDs = WIDTH / SCALE;
        public const int hDs = HEIGHT / SCALE;
        public int maxPixel = hDs;
        public byte turn = 1;
        BoardLogic boardLogic = new BoardLogic();


        Board b = new Board(hDs, wDs);
        public Cell[,] matrix;
        //public Cell[,] matrix = new Cell[hDs, wDs];

        int lastUsersSpotX;
        int lastUsersSpotY;
        int[] numOfCapturesOfEachPlayer = { 0, 0, 0, 0 };
        int maxTurnTime = 30;
        int turnSecondsElapsed;
        List<Player> playersList;
        Player currentPlayer;
        bool gameFinished = false;
        string saveGamePath = "";

     
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
            mainWindow = sentWindow;
            playersList = gs.playersList;
            currentPlayer = gs.currentPlayer;
            saveGamePath = filePath;
            lastUsersSpotX = gs.lastX;
            lastUsersSpotY = gs.lastY;

            matrix = b.getBoard();
            InitializeComponent();
            setupBoard();
            setupBoardFromSave(gs.matrix);

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

        public void setupBoardFromSave(int[,] mat)
        {
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

                    btn.ToolTip = "You sure you want to place it here??";

                    Cell cell = new Cell(btn);
                    matrix[i, j] = cell;


                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);
                    grid.Children.Add(btn);

                    btnCount++;
                }
            }


            turnSecondsElapsed = maxTurnTime;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.IsEnabled = true;
            timer.Tick += new EventHandler(onTime); ;
        }

        //Event that runs for any button on the board that is clicked
        private void btn_Event(object sender, RoutedEventArgs e)
        {
            if (gameFinished) return;
            Button cell = (sender as Button);
            int row = Grid.GetRow(cell);
            int col = Grid.GetColumn(cell);
            if (matrix[row,col].color==0 && !currentPlayer.isAi) // 0 means no color
            {
                string name = currentPlayer.name;
                int turn = currentPlayer.color;
                string color = (currentPlayer.color == 1 ? "White" : (currentPlayer.color == 2) ? "Black" : (currentPlayer.color == 3) ? "Green" : (currentPlayer.color == 4) ? "Blue" : ""); // Setting color
                                                                                                                                                                                              //System.Diagnostics.Debug.WriteLine($"Current player color: {turn} - {color}");

                this.matrix[row,col].color = turn; // Players turn number represents color

                matrix[row, col].btn.Content = new Image
                {
                    Source = new BitmapImage(new Uri($"/Resources/cross{color}.png", UriKind.RelativeOrAbsolute)),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Stretch = Stretch.Fill,
                };

                if (boardLogic.isCapture(ref matrix, lastUsersSpotX, lastUsersSpotY, row, col, turn)) { currentPlayer.numOfCaptures++; if(currentPlayer.numOfCaptures >=5 ) { timer.Stop();  MessageBox.Show($"{name} won the game with 5 captures!!!"); postWin(); } }
                if(boardLogic.xPiecesInSuccession(matrix,row,col, turn, 5, true)) { timer.Stop(); MessageBox.Show($"{name} won the game!!!"); postWin(); } // Checkin for game win;
                else if(boardLogic.xPiecesInSuccession(matrix,row,col, turn, 4)) { MessageBox.Show($"{name} has a tesera!"); } // Checkin for tesera;
                else if(boardLogic.xPiecesInSuccession(matrix,row,col, turn, 3)) { MessageBox.Show($"{name} has a tria!"); } // Checkin for tria;

                matrix[row, col].btn.IsEnabled = false;

                lastUsersSpotX = row;
                lastUsersSpotY = col;
                turnSecondsElapsed = maxTurnTime;

                timer.Stop();
                if (!gameFinished) {updateCurrentPlayer(); }
                
                string colorNext = (currentPlayer.color == 1 ? "White" : (currentPlayer.color == 2) ? "Black" : (currentPlayer.color == 3) ? "Green" : (currentPlayer.color == 4) ? "Blue" : "");
                turnImage.Source = new BitmapImage(new Uri(@$"/Resources/{colorNext}.png", UriKind.Relative));
                txtCaptures.Content = $"Captures: {currentPlayer.numOfCaptures}";

            }

        }



        void postWin()
        {
            timer.Stop();
            gameFinished = true;
        }


        void updateCurrentPlayer()
        {
            timer.Start();
            if (playersList.IndexOf(currentPlayer) + 1 == playersList.Count)
            {
               
                currentPlayer = playersList[0];
                
            }
            else
            {
                currentPlayer = playersList[playersList.IndexOf(currentPlayer) + 1];
            }
        }



        public void onTime(object sender, EventArgs e)
        {

            if (currentPlayer.isAi)
            {
                (int x, int y) = boardLogic.aiMakeMove(ref matrix, currentPlayer.color, lastUsersSpotX, lastUsersSpotY, timer);
                if (x + y != -2)
                {

                    if (boardLogic.isCapture(ref matrix, lastUsersSpotX, lastUsersSpotY, x, y, turn)) { currentPlayer.numOfCaptures++; if (currentPlayer.numOfCaptures >= 5) { timer.Stop(); MessageBox.Show($"{currentPlayer.name} won the game with 5 captures!!!"); postWin(); } }
                    if (boardLogic.xPiecesInSuccession(matrix, x, y, currentPlayer.color, 5, true)) { timer.Stop(); MessageBox.Show($"{currentPlayer.name} won the game!!!"); postWin(); } // Checkin for game win;
                    else if (boardLogic.xPiecesInSuccession(matrix, x, y, currentPlayer.color, 4)) { timer.Stop(); MessageBox.Show($"{currentPlayer.name} has a tesera!"); } // Checkin for tesera;
                    else if (boardLogic.xPiecesInSuccession(matrix, x, y, currentPlayer.color, 3)) { timer.Stop(); MessageBox.Show($"{currentPlayer.name} has a tria!"); } // Checkin for tria;

                    matrix[x, y].btn.IsEnabled = false;


                    lastUsersSpotX = x;
                    lastUsersSpotY = y;
                    turnSecondsElapsed = maxTurnTime;

                    if (!gameFinished) {  updateCurrentPlayer();}
                   
                    string colorNxt = (currentPlayer.color == 1 ? "White" : (currentPlayer.color == 2) ? "Black" : (currentPlayer.color == 3) ? "Green" : (currentPlayer.color == 4) ? "Blue" : "");
                    turnImage.Source = new BitmapImage(new Uri(@$"/Resources/{colorNxt}.png", UriKind.Relative));
                    txtCaptures.Content = $"Captures: {currentPlayer.numOfCaptures}";


                }
                else
                {
                    MessageBox.Show("Something went wrong with the AI");

                }
            }


            txtTimer.Content = $"{turnSecondsElapsed--}"; 
            if(turnSecondsElapsed < 0)
            {
                MessageBox.Show($"{currentPlayer.name} missed their turn :(");
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

                turnSecondsElapsed = maxTurnTime;
            }
        }

        bool askToClose()
        {
            if (MessageBox.Show("Do you really want to exit?", "Yes", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if(MessageBox.Show("Would you like to save the game?", "Save", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (saveGame()) { MessageBox.Show("Game saved!"); }
                }

                timer.Stop();
                return true;
            }
            return false;
        }


        public bool saveGame()
        {
            try
            {

                string time = DateTime.Now.ToString("T");
                time = time.Replace(':', '-');

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


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
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

            playersList.Clear();
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
