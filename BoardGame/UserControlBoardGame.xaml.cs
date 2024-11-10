using BoardGame.Converters;
using BoardGame.Interfaces;
using BoardGame.Models;
using BoardGame.Properties;
using BoardGame.Utilities;
using BoardGame.VIewModels;
using Microsoft.Xaml.Behaviors;
using Newtonsoft.Json.Linq;
using PropertyChanged;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;


namespace SudokuGame.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlBoardGame.xaml
    /// </summary>


    //[AddINotifyPropertyChangedInterface]
    public partial class UserControlBoardGame : UserControl
    {

       // private int indexColor;
       // SoundPlayer sound;
        public bool IsPanelColorVisable { get; set; } = true;

        //BoardViewModel viewModel;

        public UserControlBoardGame(BoardViewModel boradVM/*ICombinedGameBoardProvider boardGame, Action actionAfterEndGame*/)
        {
            InitializeComponent();

           // string soundFilePath = /*System.IO.*/Path.Combine("..", "..", "..", "BoardGame/Assets/Sounds/right-left-arrow.wav");
          //  sound = new SoundPlayer(soundFilePath);
          //  indexColor = 0;
            DataContext = boradVM;
        }



        //private void Change_Background_Color(object sender, RoutedEventArgs e)
        //{
        //    Button button = (Button)sender;
        //    SolidColorBrush buttonColor = (SolidColorBrush)button.Background;
        //    Resources["CustomColor0"] = buttonColor;
        //}


        //private void ArrowClick(object sender, MouseButtonEventArgs e)
        //{

        //    sound.Play();
        //    Button temp = null;
        //    int newIndex = 0, startIndex = 2, endIndex = 5;

        //    Action swap = () =>
        //    {
        //        int t = startIndex;
        //        startIndex = endIndex;
        //        endIndex = t;
        //    };

        //    if ((sender as Button).Name == "buRight")
        //    {
        //        // Move to the right
        //        // 3 this is child button color start
        //        newIndex = Math.Abs((++indexColor + 3) % 9);
        //    }
        //    else
        //    {
        //        // Move to the left
        //        swap();
        //        // 7 this is child button color end
        //        newIndex = Math.Abs((indexColor-- + 8) % 9);
        //    }
        //    temp = stPanelColor.Children[startIndex] as Button;
        //    stPanelColor.Children.RemoveAt(startIndex);
        //    temp.Background = Resources[$"CustomColor{newIndex}"] as SolidColorBrush;
        //    stPanelColor.Children.Insert(endIndex, temp);
        //    indexColor %= 9;

        //}

    }


}

