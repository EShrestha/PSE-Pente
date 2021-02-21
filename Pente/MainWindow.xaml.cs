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

    // Main menu of the game.
    // Responsible for getting the game information.
    // After the game information is gathered, play window is shown which has the board
    public partial class MainWindow : Window 
    {
        PlayWindow playWindow;
        int numOfPlayers = 1;
        List<Player> playersList = new List<Player>(); // Holds the list of current players and their details
        private FileStream stream;  // Used for deserilization
        IFormatter formatter = new BinaryFormatter(); // Used for deserilization
        GameSave gs; // Game save file incase user wants to open a save file


        public MainWindow()
        {
            InitializeComponent();
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {

            if (!((bool)numP1.IsChecked | (bool)numP2.IsChecked | (bool)numP3.IsChecked | (bool)numP4.IsChecked)) { MessageBox.Show("Please select number of players"); return; }

            this.Hide();

            // If only one human player is playing
            if(numOfPlayers == 1)
            {
                // Creating a new player with witht he parameters (name, color, number of captures, is player AI?)
                playersList.Add(new Player(txtName1.Text, 1, 0));
                playersList.Add(new Player(txtName2.Text, 2, 0, true)); // If there is only one human player there must be an ai
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



            // Creating a new play window, the game will carry on there
            playWindow = new PlayWindow(playersList, this);
            Hide(); // Hiding the main menu while the play window is up
            playWindow.Show();
        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            // Closing the main menu
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // Does nothing be is required to compile
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(playWindow != null) // When closing main menu, we need to close the play window as well if one exists
                playWindow.Close();
        }
         
        // Updates what the settings of the game bases on what radio button is pressed
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


        // If user wants to open a previously saved game
        private void openSave_Click(object sender, RoutedEventArgs e)
        {
            // Creating \Pentegames directory so there is no error
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
                    // Get the file path chosen by the user
                    stream = new FileStream(@$"{openFileDialog.FileName}", FileMode.Open, FileAccess.Read);
                    // Deserializing the game save file to a GameSave object
                    gs = (GameSave)formatter.Deserialize(stream);
                    // Opening the play window with the GameSave object
                    playWindow = new PlayWindow(gs, openFileDialog.FileName, this);
                    // Hiding main menu withle play window is up
                    Hide();
                    // Displaying the play window
                    playWindow.Show();
                    // CLosing the stream to free up resources
                    stream.Close();
                }
                catch
                {
                    // If an error occurs while opening save file
                    MessageBox.Show("Something went wrong, try again.");
                }
            }
            
        }

        private void p1_Checked(object sender, RoutedEventArgs e)
        {
            // Does nothing be is required to compile
        }

        private void p2_Checked(object sender, RoutedEventArgs e)
        {
            // Does nothing be is required to compile
        }

        private void p3_Checked(object sender, RoutedEventArgs e)
        {
            // Does nothing be is required to compile
        }

        private void p4_Checked(object sender, RoutedEventArgs e)
        {
            // Does nothing be is required to compile
        }
    }
}
