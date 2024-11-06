using BoardGame.Interfaces;
using BoardGame.Resources;
using SudokuGame;
using SudokuGame.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
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
using System.Configuration;
using BoardGame.Utilities;
using MaterialDesignThemes.Wpf;
using SudokuGame.Pages;
using System.Media;
using System.Net.Http;
using System.Net;
using System.Text.RegularExpressions;

namespace SudokuGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public bool IsButtonVisible { get; set; } = true;
       // SoundPlayer soundPlayer = null;
      //  private GamePage gamePage;

        public MainWindow()
        {
         
            //int[] nums = new int[] { -1, 0, 1, 2, -1, -4 };
            //nums = nums.OrderBy(x => x).ToArray();
            InitializeComponent();

            
            //string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            //string soundFilePath = System.IO.Path.Combine("..", "..", "Sounds", "background-sound.wav");
            //soundPlayer = new SoundPlayer(soundFilePath);
            //Task.Run(() => soundPlayer.PlayLooping());

        }


        public void BackMainWinodw()
        {
            //Task.Run(() => soundPlayer.PlayLooping());
            //spMainButtons.Visibility = Visibility.Visible;
            //this.Title = "Sudoku Gold";
           
            //contentFrame.Content = null;
        }

        //private void RunGame()
        //{

        //    Application.Current.Dispatcher.Invoke(() =>
        //    {
        //        soundPlayer.Stop();
        //        contentFrame.Navigate(new GamePage());
        //        spMainButtons.Visibility = Visibility.Hidden;
        //        IsButtonVisible = false;
        //        Mouse.OverrideCursor = Cursors.Wait;
        //        Task.Delay(200);
        //        Mouse.OverrideCursor = null;
        //    });
        //}


        private void Main_Menu_Options(object sender, RoutedEventArgs e)
        {

            // var viewModel = new CustomMaterialDesignPopupViewModel();

            //switch ((sender as Button).Name)
            //{
            //    case "buPlayGame":
            //        {
            //            RunGame();
            //            break;
            //        }
            //    case "buHall":
            //        break;
            //    case "buSettings":
            //        break;
            //    case "buExit":
            //        Application.Current.Shutdown();
            //        break;
            //}

        }

        private void GameWindow_Closed(object sender, EventArgs e)
        {

            //gameWindow.Dispose(); // Dispose the game window to clean up resources
            this.Visibility = Visibility.Visible;
            this.spMainButtons.Visibility = Visibility.Visible;

        }
    }

}
