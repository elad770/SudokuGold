using BoardGame.Converters;
using BoardGame.Interfaces;
using BoardGame.Models;
using BoardGame.Properties;
using BoardGame.VIewModels;
using Newtonsoft.Json.Linq;
using PropertyChanged;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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


        BoardViewModel viewModel;
        public UserControlBoardGame(ICombinedGameBoardProvider boardGame, string level, Action actionAfterEndGame)
        {
            InitializeComponent();

            string soundFilePath = System.IO.Path.Combine("..", "..", "..", "BoardGame/Assets/Sounds/right-left-arrow.wav");
            sound = new SoundPlayer(soundFilePath);

            indexColor = 0;
            viewModel = new BoardViewModel(boardGame, level, actionAfterEndGame);
            GenerateBoard();
            DataContext = viewModel;
            // DataContext = this;
        }


        public void GenerateBoard()
        {

            // Create the grid
            Grid grid = new Grid();

            // Create the rows and columns of the grid
            for (int i = 0; i < 9; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50) });
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {

                    Border border = new Border();
                    border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#212F3C"));
                    OrderBorderThickness(border, i, j);
                    viewModel.GenerateTextBoxCell(new int[] { i, j });
                    border.Child = viewModel.TextBoxCells.Last();

                    // Add the text box to the grid
                    Grid.SetRow(border, i);
                    Grid.SetColumn(border, j);
                    grid.Children.Add(border);

                }
            }

            //  grid.Margin = new Thickness(0, 0, 0, 10);
            spTable.Children.Insert(0, grid);

        }

        void OrderBorderThickness(Border borCurrent, int i, int j)
        {
            //  Border Thickness of borad
            if (i % 3 == 0)
            {
                borCurrent.BorderThickness = new Thickness(0, 1.5, 0, 0);
            }
            else if (i == 8)
            {
                borCurrent.BorderThickness = new Thickness(0, 0, 0, 1.5);
            }
            if (j % 3 == 0)
            {
                borCurrent.BorderThickness = new Thickness(1.5, borCurrent.BorderThickness.Top, 0, borCurrent.BorderThickness.Bottom);
            }
            else if (j == 8)
            {
                borCurrent.BorderThickness = new Thickness(0, borCurrent.BorderThickness.Top, 1.5, borCurrent.BorderThickness.Bottom);
            }
        }


        public void HandlerButtonOutsideGame(GameAction action, string pram = null)
        {
            try
            {

                switch (action)
                {
                    case GameAction.New_Game:
                        {
                            viewModel.Board = pram;
                            break;
                        }

                    case GameAction.Reset_Game:
                        {
                            viewModel.Board = "clone";
                            break;
                        }

                    case GameAction.Number_Optional_Or_Erase:
                        {
                            if (viewModel.focusedTextBox != null && !viewModel.focusedTextBox.IsReadOnly)
                            {
                                viewModel.focusedTextBox.Text = pram;
                            }
                            break;
                        }

                    default:
                        break;
                }

            }
            catch
            {

            }

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


        public void LostFocusCell()
        {
            if (viewModel.focusedTextBox != null)
            {
                viewModel.focusedTextBox.GotFocus -= viewModel.TextBox_GotFocus;
            }

        }
        public void RefocusCell()
        {

            if (viewModel.focusedTextBox != null)
            {

                viewModel.focusedTextBox.GotFocus += viewModel.TextBox_GotFocus;
            }
        }



        //private void ArrowClick(object sender, RoutedEventArgs e)
        //{
        //    var buttons = stPanelColor.Children.OfType<Button>().
        //                        Where(
        //                        Bu => Bu.Content == null
        //                        ).ToList();


        //    Action<List<Button>> AddNewChildernColor = (li) =>
        //    {
        //        stPanelColor.Children.OfType<Button>()
        //                            .Where(b => b.Content == null)
        //                            .ToList()
        //                            .ForEach(b => stPanelColor.Children.Remove(b));
        //        for (int i = 0; i < li.Count; i++)
        //        {
        //            stPanelColor.Children.Insert(i + 2, li[i]);
        //        }
        //    };

        //    Button temp = null;
        //    int newIndex = 0;
        //    if ((sender as Button).Name == "buRight")
        //    {

        //        // Move to the right
        //        temp = buttons[0];
        //        buttons.RemoveAt(0);

        //        // Calculate the new index for the color (wrapping around if needed)
        //        newIndex = Math.Abs((++indexColor + buttons.Count) % 8);
        //        temp.Background = Resources[$"CustomColor{newIndex}"] as SolidColorBrush;
        //        buttons.Add(temp);
        //    }
        //    else
        //    {

        //        // Move to the left
        //        int indexLast = buttons.Count - 1;
        //        temp = buttons[indexLast];
        //        buttons.RemoveAt(indexLast);

        //        // Calculate the new index for the color (wrapping around if needed)
        //        newIndex = Math.Abs((indexColor-- + 7) % 8);
        //        temp.Background = Resources[$"CustomColor{newIndex}"] as SolidColorBrush;
        //        buttons.Insert(0, temp);
        //    }


        //    AddNewChildernColor(buttons);
        //    indexColor %= 8;


        //    //if ((sender as Button).Name == "buRight")
        //    //{
        //    //    temp = buttons[0];
        //    //    buttons.RemoveAt(0);
        //    //    if (index2 >= 7)
        //    //    {
        //    //        index2 = 0;
        //    //    }

        //    //    buttons.Add(temp);
        //    //    temp.Background = Resources[$"CustomColor{buttons.Count + (index2++)}"] as SolidColorBrush;
        //    //    // indexColorLeft++;

        //    //}
        //    //else
        //    //{
        //    //    int indexLast = buttons.Count - 1;
        //    //    temp = buttons[indexLast];
        //    //    buttons.RemoveAt(indexLast);
        //    //    buttons.Insert(0, temp);
        //    //    if (index2 == 0)
        //    //    {
        //    //        index2 = 8;
        //    //    }

        //    //    // int current = 8 + (--index2) - buttons.Count;
        //    //    temp.Background = Resources[$"CustomColor{(--index2)}"] as SolidColorBrush;
        //    //    //temp.Background = Resources[$"CustomColor{indexColorLeft--}"] as SolidColorBrush;
        //    //    // indexColorRight--;
        //    //}
        //    //indexColorLeft %= 8;
        //    //indexColorRight %= 8;


        //}



    }
}
