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
    struct Cell
    {
        public Button btn;
        SolidColorBrush color;

        public Cell(Button btn, SolidColorBrush color = null)
        {
            this.btn = btn;
            this.color = color;

        }
    }

    public partial class PlayWindow : Window
    {
        Window mainWindow;
        private int numOfPlayers;
        private const int SCALE = 51;
        private const int WIDTH = 1000;
        private const int HEIGHT = 1000;
        private const int wDs = WIDTH / SCALE;
        private const int hDs = HEIGHT / SCALE;
        public int maxPixel = hDs;
        

        Cell[,] matrix = new Cell[hDs, wDs];

     
        public PlayWindow( int players, Window sentWindow=null)
        {
            mainWindow = sentWindow;
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
            for (int i = 0; i < HEIGHT / SCALE; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < WIDTH / SCALE; i++)
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
                    btn.Content = new Image
                    {
                        Source = new BitmapImage(new Uri("/Resources/cross.png", UriKind.RelativeOrAbsolute)),
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Stretch = Stretch.Fill,
                    };


                    ////matrix[i, j] = btn;



                    Cell cell = new Cell(btn);
                    matrix[i, j] = cell;


                    Grid.SetColumn(btn, j);
                    Grid.SetRow(btn, i);
                    grid.Children.Add(btn);

                    btnCount++;
                }

            }
        }

        //Event that runs for a button that is clicked
        private void btn_Event(object sender, RoutedEventArgs e)
        {
            Button cell = (sender as Button);
            matrix[Grid.GetRow(cell), Grid.GetColumn(cell)].btn.Content = new Image
            {
                Source = new BitmapImage(new Uri("/Resources/crossWhite.png", UriKind.RelativeOrAbsolute)),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Stretch = Stretch.Fill,
            };
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

        public int testMeth()
        {
            return 1;
        }
    }
}
