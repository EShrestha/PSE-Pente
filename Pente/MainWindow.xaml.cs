using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pente
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    // test comment (Damon)
    public partial class MainWindow : Window
    {
        PlayWindow playWindow;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            playWindow = new PlayWindow(2, this);
            playWindow.Show();
        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(playWindow != null)
                playWindow.Close();
        }

        public int X()
        {
            return 1;
        }

        // THIS IS TEST 12
    }
}
