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


    public enum GameAction
    {
        New_Game,
        Reset_Game,
        Number_Optional_Or_Erase,
    }



    [AddINotifyPropertyChangedInterface]
    public partial class UserControlBoardGame : UserControl
    {

        private int indexColor;
        SoundPlayer sound;
        public bool IsPanelColorVisable { get; set; } = true;

        //BoardViewModel viewModel;

        public UserControlBoardGame(BoardViewModel boradVM/*ICombinedGameBoardProvider boardGame, Action actionAfterEndGame*/)
        {
            InitializeComponent();

            string soundFilePath = /*System.IO.*/Path.Combine("..", "..", "..", "BoardGame/Assets/Sounds/right-left-arrow.wav");
            sound = new SoundPlayer(soundFilePath);
            indexColor = 0;
            DataContext = boradVM;
        }

       

        private void Change_Background_Color(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            SolidColorBrush buttonColor = (SolidColorBrush)button.Background;
            Resources["CustomColor0"] = buttonColor;
        }


        private void ArrowClick(object sender, MouseButtonEventArgs e)
        {

            sound.Play();
            Button temp = null;
            int newIndex = 0, startIndex = 2, endIndex = 5;

            Action swap = () =>
            {
                int t = startIndex;
                startIndex = endIndex;
                endIndex = t;
            };

            if ((sender as Button).Name == "buRight")
            {
                // Move to the right
                // 3 this is child button color start
                newIndex = Math.Abs((++indexColor + 3) % 9);
            }
            else
            {
                // Move to the left
                swap();
                // 7 this is child button color end
                newIndex = Math.Abs((indexColor-- + 8) % 9);
            }
            temp = stPanelColor.Children[startIndex] as Button;
            stPanelColor.Children.RemoveAt(startIndex);
            temp.Background = Resources[$"CustomColor{newIndex}"] as SolidColorBrush;
            stPanelColor.Children.Insert(endIndex, temp);
            indexColor %= 9;

        }



        public void RefocusCell()
        {

            //if (viewModel.focusedTextBox != null)
            //{

            //    // viewModel.focusedTextBox.GotFocus += viewModel.TextBox_GotFocus;
            //}
        }

    }


}




/*
 *  List<Action<int, int, int>> actionsList = new List<Action<int, int, int>>();
            Action<int, int, int> add = (int num1, int num2, int num3) =>
            {
                Debug.WriteLine($"{num1} + {num2} + {num3} = {num1 + num2 + num3}");
            };
            Action<int, int, int> mul = null;
            mul = (int num, int num2, int num3) =>
            {
                if (num <= 0)
                {
                    return;
                }
                Debug.WriteLine(num + " * " + num2 + " * " + num3 + " = " + num * num2 * num3);
                mul(--num, num2, num3);
            };

            Action<int, int, int> fib = null;
            fib = (int index, int adv, int next) =>
            {
                if (index == -1)
                {
                    Console.WriteLine();
                    return;
                }
                int current = adv;
                adv += next;
                next = current;
                Debug.Write(current + " ");
                fib(--index, adv, next);
            };

            actionsList.Add(fib);
            actionsList.Add(add);
            actionsList.Add(mul);

            //fib
            actionsList[0](10, 1, 0);

            //other 
            for (int i = 1; i < actionsList.Count; i++)
            {
                int a = new Random().Next(i, 14);
                int b = new Random().Next(i, 14);
                int c = new Random().Next(i, 14);
                actionsList[i](a, b, c);
            }


 */