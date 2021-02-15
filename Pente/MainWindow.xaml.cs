using Microsoft.Win32;
using Pente.GameLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
        List<Player> playersList = new List<Player>();
        private FileStream stream;
        IFormatter formatter = new BinaryFormatter();
        GameSave gs;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {

            if (!((bool)numP1.IsChecked | (bool)numP2.IsChecked | (bool)numP3.IsChecked | (bool)numP4.IsChecked)) { MessageBox.Show("Please select number of players"); return; }

            this.Hide();

            if(numOfPlayers == 1)
            {
                playersList.Add(new Player(txtName1.Text, 1, 0));
                playersList.Add(new Player(txtName2.Text, 2, 0, true));
            }
            else if(numOfPlayers == 2)
            {
                playersList.Add(new Player(txtName1.Text, 1, 0));
                playersList.Add(new Player(txtName2.Text, 2, 0, (bool)p2.IsChecked));
            }
            else if (numOfPlayers == 3)
            {
                playersList.Add(new Player(txtName1.Text, 1, 0));
                playersList.Add(new Player(txtName2.Text, 2, 0, (bool)p2.IsChecked));
                playersList.Add(new Player(txtName3.Text, 3, 0, (bool)p2.IsChecked));
            }
            else if (numOfPlayers == 4)
            {
                playersList.Add(new Player(txtName1.Text, 1, 0));
                playersList.Add(new Player(txtName2.Text, 2, 0, (bool)p2.IsChecked));
                playersList.Add(new Player(txtName3.Text, 3, 0, (bool)p2.IsChecked));
                playersList.Add(new Player(txtName4.Text, 4, 0, (bool)p2.IsChecked));
            }




            playWindow = new PlayWindow(playersList, this);
            //playersList.Clear();
            Hide();
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

            p1.IsChecked = false;
            p1.IsEnabled = false;
            txtName1.IsEnabled = true;

            p2.IsChecked = true;
            p2.IsEnabled = false;
            txtName2.IsEnabled = true;

            p3.IsChecked = false;
            p3.IsEnabled = false;
            txtName3.IsEnabled = false;

            p4.IsChecked = false;
            p4.IsEnabled = false;
            txtName4.IsEnabled = false;


        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            numOfPlayers = 2;

            p1.IsChecked = false;
            p1.IsEnabled = false;
            txtName1.IsEnabled = true;

            p2.IsChecked = false;
            p2.IsEnabled = true;
            txtName2.IsEnabled = true;

            p3.IsChecked = false;
            p3.IsEnabled = false;
            txtName3.IsEnabled = false;

            p4.IsChecked = false;
            p4.IsEnabled = false;
            txtName4.IsEnabled = false;
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            numOfPlayers = 3;
            p1.IsChecked = false;
            p1.IsEnabled = false;
            txtName1.IsEnabled = true;

            p2.IsChecked = false;
            p2.IsEnabled = true;
            txtName2.IsEnabled = true;

            p3.IsChecked = false;
            p3.IsEnabled = true;
            txtName3.IsEnabled = true;

            p4.IsChecked = false;
            p4.IsEnabled = false;
            txtName4.IsEnabled = false;
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            numOfPlayers = 4;

            p1.IsChecked = false;
            p1.IsEnabled = false;
            txtName1.IsEnabled = true;

            p2.IsChecked = false;
            p2.IsEnabled = true;
            txtName2.IsEnabled = true;

            p3.IsChecked = false;
            p3.IsEnabled = true;
            txtName3.IsEnabled = true;

            p4.IsChecked = false;
            p4.IsEnabled = true;
            txtName4.IsEnabled = true;
        }

        private void p1_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void p2_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void p3_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void p4_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void openSave_Click(object sender, RoutedEventArgs e)
        {
            System.IO.Directory.CreateDirectory(@"\PenteGames");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\PenteGames";
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Pente Saves|*.pente";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    stream = new FileStream(@$"{openFileDialog.FileName}", FileMode.Open, FileAccess.Read);
                    gs = (GameSave)formatter.Deserialize(stream);
                    playWindow = new PlayWindow(gs, openFileDialog.FileName, this);
                    Hide();
                    playWindow.Show();
                    stream.Close();
                }
                catch
                {
                    MessageBox.Show("Something went wrong, try again.");
                }
            }
            
        }

    }
}
