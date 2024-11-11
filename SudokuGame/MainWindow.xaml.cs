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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GameWindow_Closed(object sender, EventArgs e)
        {

            //gameWindow.Dispose(); // Dispose the game window to clean up resources
            this.Visibility = Visibility.Visible;
            this.spMainButtons.Visibility = Visibility.Visible;

        }
    }

}
