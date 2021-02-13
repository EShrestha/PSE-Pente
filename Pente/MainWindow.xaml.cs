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
        int numOfPlayers = 1;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            playWindow = new PlayWindow(numOfPlayers, this);
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

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            numOfPlayers = 1;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            numOfPlayers = 2;
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            numOfPlayers = 3;
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            numOfPlayers = 4;
        }
    }
}
