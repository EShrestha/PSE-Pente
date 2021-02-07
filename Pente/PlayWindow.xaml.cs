using Pente.GameLogic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pente
{
    /// <summary>
    /// Interaction logic for PlayWindow.xaml
    /// </summary>
    /// 
    public struct Cell
    {
        public Button btn;
        public Byte color;

        public Cell(Button btn, Byte color = 0)
        {
            this.btn = btn;
            this.color = color;

        }
    }

    public partial class PlayWindow : Window
    {
        Window mainWindow;
        private int numOfPlayers;
        private const int SCALE = 39;
        private const int WIDTH = 741;
        private const int HEIGHT = 741;
        private const int wDs = WIDTH / SCALE;
        private const int hDs = HEIGHT / SCALE;
        public int maxPixel = hDs;
        public byte turn = 1;
        BoardLogic boardLogic = new BoardLogic();

        Cell[,] matrix = new Cell[hDs, wDs];

     
        public PlayWindow( int players, Window sentWindow=null)
        {
            mainWindow = sentWindow;
            numOfPlayers = players;
            InitializeComponent();
            setupBoard();

        }

        public PlayWindow()
        {
            InitializeComponent();
            setupBoard();
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



                    Cell cell = new Cell(btn);
                    matrix[i, j] = cell;


                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);
                    grid.Children.Add(btn);

                    btnCount++;
                }

            }
        }

        //Event that runs for any button on the board that is clicked
        private void btn_Event(object sender, RoutedEventArgs e)
        {
            Button cell = (sender as Button);
            int row = Grid.GetRow(cell);
            int col = Grid.GetColumn(cell);
            if (matrix[row,col].color==0) // 0 means no color
            {
                string color = (turn == 1 ? "White" : (turn == 2) ? "Black" : (turn == 3) ? "Green" : "Blue"); // Setting color
                if(turn != numOfPlayers) { turn++; } else { turn=1; } // Incrementing turn
                matrix[row,col].color = turn; // Players turn number represents color
                // Updating button image with the players color
                matrix[row,col].btn.Content = new Image
                {
                    Source = new BitmapImage(new Uri($"/Resources/cross{color}.png", UriKind.RelativeOrAbsolute)),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Stretch = Stretch.Fill,
                };

                if(boardLogic.xPiecesInSuccession(matrix,row,col,turn,5)) { MessageBox.Show($"{color} won the game!!!"); } // Checkin for tria;
                else if(boardLogic.xPiecesInSuccession(matrix,row,col,turn,4)) { MessageBox.Show($"{color} has a tesra!"); } // Checkin for tria;
                else if(boardLogic.xPiecesInSuccession(matrix,row,col,turn,3)) { MessageBox.Show($"{color} has a tria!"); } // Checkin for tria;
                matrix[row, col].btn.IsEnabled = false;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
