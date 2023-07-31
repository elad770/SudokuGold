using BoardGame.Converters;
using BoardGame.Interfaces;
using BoardGame.Models;
using BoardGame.Resources;
using BoardGame.Utilities;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;


namespace BoardGame.VIewModels
{


    [AddINotifyPropertyChangedInterface]
    public class BoardViewModel
    {

        private object board;

        internal TextBox focusedTextBox;

        private Dictionary<string, SoundPlayer> soundPlayers;

        private Action actionAfterEndGame;

        private ICombinedGameBoardProvider gameBoard;

        public object Board
        {
            get => board;
            set
            {
                //  if (value is int[,] arr2D)
                if (value is string boardLevel)
                {
                    if (boardLevel == "clone")
                    {
                        board = this.gameBoard.CloneBorad();
                        //board = ((int[,])boardClone).Clone();
                    }
                    else
                    {
                        board = this.gameBoard.GetBoard(boardLevel);
                    }
                    UpdateCells();

                }

                else if (value is int[] parms)
                {
                    UpdateCell(parms);
                }
            }
        }
        // public Tuple<int, int, int> CellCurrentTag { get; set; }

        public CellContect Cell { get; set; }


        public ICommand ChangeBackgroundColorCommand { get; }

        public ObservableCollection<TextBoxCell> TextBoxCells { get; set; }




        public BoardViewModel(ICombinedGameBoardProvider gameBoard, string boardLevel, Action actionAfterEndGame)
        {
            this.gameBoard = gameBoard;
            TextBoxCells = new ObservableCollection<TextBoxCell>();
            Board = boardLevel;
            ChangeBackgroundColorCommand = new RelayCommand(ChangeBackgroundColor);
            this.actionAfterEndGame = actionAfterEndGame;
            FillSounds();
        }

        private void FillSounds()
        {
            soundPlayers = new Dictionary<string, SoundPlayer>();
            string soundFilePath = System.IO.Path.Combine("..", "..", "..", "BoardGame/Assets/Sounds");
            string[] soundFiles = Directory.GetFiles(soundFilePath);

            for (int i = 0; i < soundFiles.Length; i++)
            {
                string soundFileName = System.IO.Path.GetFileNameWithoutExtension(soundFiles[i]);
                soundPlayers[soundFileName] = new SoundPlayer(soundFiles[i]);
            }
        }



        private void UpdateCells(bool isNewGame = true, dynamic li_indexes = null)
        {
            if (isNewGame)
            {
                // CellCurrentTag = null;
                focusedTextBox = null;
                Cell = new CellContect();
                TextBoxCells.OfType<TextBox>().ToList().ForEach(tb => tb.Foreground = Brushes.Black);
                return;

            }
            else if (/*li_indexes != null &&*/ li_indexes.Count > 0)
            {
                foreach (var indexes in li_indexes)
                {
                    int index = indexes[0] * 9 + indexes[1] % 9;
                    if (!TextBoxCells[index].IsReadOnly)
                    {
                        TextBoxCells[index].Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x72, 0xE3));
                    }
                }
            }
            if (IsBoardSolved())
            {
                actionAfterEndGame();
            }

        }


        private bool IsBoardSolved()
        {

            return TextBoxCells.OfType<TextBox>().All(
               textbox =>
               !(textbox.Text == "" && textbox != focusedTextBox || textbox.Foreground == Brushes.Red));

            //foreach (var tb in TextBoxCells)
            //{
            //    if (tb.Text == "" && tb != focusedTextBox || tb.Foreground == Brushes.Red)
            //    {
            //        return false;
            //    }

            //}
            //return true;
            //return TextBoxCells.OfType<TextBox>().All(
            //    textbox =>
            //    textbox.Text != ""  && textbox.Foreground != Brushes.Red);
        }

        private void UpdateCell(int[] parms)
        {
            var bo = (board as int[,]);
            int row = parms[1], col = parms[2];
            int result = bo[row, col] == parms[0] ? 0 : parms[0];
            int index = row * 9 + col % 9;
            if (result != 0)
            {

                if (gameBoard.IsBoardValid(parms))
                {
                    TextBoxCells[index].Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x72, 0xE3));
                    soundPlayers["success-sound"].Play();
                }
                else
                {
                    TextBoxCells[index].Foreground = Brushes.Red;
                    soundPlayers["error-sound"].Play();
                }



                //  TextBoxCells[index].Foreground = gameBoard.IsBoardValid(parms) ? new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x72, 0xE3)) : Brushes.Red;
                // Update the foreground for the appearances of this replaced value 
                int preValueChange = bo[row, col];
                bo[row, col] = result;
                parms[0] = preValueChange;
                UpdateCells(false, gameBoard.GetIndexesLocationError(parms));
            }
            else if (bo[row, col] != 0)
            {
                bo[row, col] = result;
                UpdateCells(false, gameBoard.GetIndexesLocationError(parms));
            }
            Cell.Content = result.ToString();

        }

        public void GenerateTextBoxCell(int[] indexesCell)
        {
            var textBox = new TextBoxCell();
            textBox.SelectionChanged += TextBox_SelectionChanged;
            textBox.GotFocus += TextBox_GotFocus;
            textBox.PreviewKeyDown += TextBox_PreviewKeyDown;
            //  textBox.PreviewMouseRightButtonDown += TextBox_PreviewMouseRightButtonDown;
            Func<IValueConverter, Binding> createBinding = (convter) =>
             {
                 Binding bind = new Binding("Board")
                 {
                     Source = this,
                     Converter = convter,
                     //  ConverterParameter = textBox.IsReadOnly,
                     Mode = BindingMode.TwoWay,
                     UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,

                 };
                 return bind;
             };

            textBox.SetBinding(TextBox.IsReadOnlyProperty, createBinding(new CellReadOnlyConverter(indexesCell)));
            textBox.SetBinding(TextBox.TagProperty, createBinding(new CellTagConverter(indexesCell)));
            textBox.SetBinding(TextBox.TextProperty, createBinding(new CellTextConverter(indexesCell)));


            var multiBinding = new MultiBinding
            {
                Converter = new MultiBackgroundConverter(),
                ConverterParameter = textBox // new object[] { textBox.Tag, textBox }
            };

            multiBinding.Bindings.Add(new Binding("Cell.CellCurrentTag")
            {
                Mode = BindingMode.OneWay,
                Source = this,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

            multiBinding.Bindings.Add(new Binding("Cell.Content")
            {
                Mode = BindingMode.OneWay,
                Source = this,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });


            textBox.SetBinding(TextBox.BackgroundProperty, multiBinding);


            //textBox.SetBinding(TextBox.BackgroundProperty, new Binding(
            //      "Cell")
            //{
            //    Mode = BindingMode.OneWay,
            //    Source = this,
            //    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            //    Converter = new CellBackgroundConverter(),
            //    ConverterParameter = new object[] { textBox.Tag, textBox.Text },
            //});


            TextBoxCells.Add(textBox);

        }



        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //Prevent caret of textbox 
            if (sender != null)
            {
                TextBox tb = (sender as TextBox);
                e.Handled = true;
                if (tb.SelectionLength != 0)
                    tb.SelectionLength = 0;
            }
        }

        internal void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {

            focusedTextBox = (TextBox)sender;
            //  CellCurrentTag = (dynamic)(focusedTextBox).Tag;

            Cell.CellCurrentTag = (dynamic)(focusedTextBox).Tag;
            Cell.Content = focusedTextBox.Text;
            soundPlayers["focus-sound"].Play();
        }

        private void ChangeBackgroundColor(object parameter)
        {
            ColorDictionaryResource.ColorHexKey = parameter.ToString();
            if (focusedTextBox != null)
            {
                Cell.CellCurrentTag = null;
                //CellCurrentTag = null;
                focusedTextBox.Focus();
            }
            else
            {
                soundPlayers["focus-sound"].Play();
            }
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox changedTextBox = (TextBox)sender;

            if (e.Key >= Key.Left && e.Key <= Key.Down)
            {
                dynamic tag = changedTextBox.Tag as Tuple<int, int, int>;
                int rowIndex = tag.Item1, columnIndex = tag.Item2;

                switch (e.Key)
                {
                    case Key.Left:
                        columnIndex = GetMoveValidInBorad(--columnIndex);
                        break;
                    case Key.Right:
                        columnIndex = GetMoveValidInBorad(++columnIndex);
                        break;
                    case Key.Up:
                        rowIndex = GetMoveValidInBorad(--rowIndex);
                        break;
                    case Key.Down:
                        rowIndex = GetMoveValidInBorad(++rowIndex);
                        break;
                }
                TextBoxCells[rowIndex * 9 + columnIndex % 9].Focus();

            }
            else if (!changedTextBox.IsReadOnly)
            {
                if (e.Key == Key.Back)
                {
                    changedTextBox.Text = "";
                }
                else if (changedTextBox.Text.Length > 0)
                {
                    changedTextBox.Text = e.Key.ToString().Substring(1);
                    e.Handled = true;
                }

            }

        }

        public int GetMoveValidInBorad(int row_or_col)
        {

            //A function that returns the legal position of a slot
            //This function is actually used to test the arrow keys pressed by the user

            int rows = (board as int[,]).GetLength(0);
            if (row_or_col % rows == 0)
            {
                return 0;
            }
            if (row_or_col < 0)
            {
                return rows - 1;
            }
            return row_or_col % rows;
        }

    }
}
