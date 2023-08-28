using BoardGame.Converters;
using BoardGame.Interfaces;
using BoardGame.Models;
using BoardGame.Utilities;
using PropertyChanged;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;


namespace BoardGame.VIewModels
{

    [AddINotifyPropertyChangedInterface]
    public class BoardViewModel
    {

        //  private object board;
        //public object Board
        //{
        //    get => board;
        //    set
        //    {
        //        //  if (value is int[,] arr2D)
        //        if (value is string boardLevel)
        //        {
        //            if (boardLevel == "clone")
        //            {
        //                board = this.gameBoard.CloneBorad();
        //                //board = ((int[,])boardClone).Clone();
        //            }
        //            else
        //            {
        //                board = this.gameBoard.GetBoard(boardLevel);
        //            }
        //            UpdateCells();

        //        }

        //        else if (value is int[] parms)
        //        {
        //            UpdateCell(parms);
        //        }
        //    }
        //}

        #region Fields

        internal TextBox focusedTextBox;

        private Dictionary<string, SoundPlayer> soundPlayers;

        private Action actionAfterEndGame;

        private ICombinedGameBoardProvider gameBoard;

        private static int[,] ArrBorad;

        // public static bool IsInitialized { get; internal set; } = true;

        #endregion

        #region Propites
        public ICommand ChangeBackgroundColorCommand { get; }

        public ICommand FocusTextBoxCommand { get; }

        public ICommand AfterInsertValToCell { get; }


        //   public ObservableCollection<TextBoxCell> TextBoxCells { get; set; }

        public ObservableCollection<CellContect> FullBorad { get; set; }


        #endregion

        #region Constructor
        public BoardViewModel(ICombinedGameBoardProvider gameBoard, string boardLevel, Action actionAfterEndGame)
        {
            this.gameBoard = gameBoard;
            Enum.TryParse(boardLevel, out DifficultyLevel level);
            this.gameBoard.GenerateNewBoard(out ArrBorad, level);
            FillCollectionTextBoxCells();
            ChangeBackgroundColorCommand = new RelayCommand(ChangeBackgroundColor);
            FocusTextBoxCommand = new RelayCommand(FocusTextBox);
            AfterInsertValToCell = new RelayCommand(ExecuteAfterInsertVal);
            this.actionAfterEndGame = actionAfterEndGame;
            FillSounds();
        }


        #endregion

        #region Methods

        private void FillSounds()
        {
            soundPlayers = new Dictionary<string, SoundPlayer>();
            // string soundFilePath = "pack://application:,,,/Assets/Sounds";
            string soundFilePath = System.IO.Path.Combine("..", "..", "..", "BoardGame/Assets/Sounds");
            string[] soundFiles = Directory.GetFiles(soundFilePath);

            for (int i = 0; i < soundFiles.Length; i++)
            {
                string soundFileName = System.IO.Path.GetFileNameWithoutExtension(soundFiles[i]);
                soundPlayers[soundFileName] = new SoundPlayer(soundFiles[i]);
            }
        }

<<<<<<< HEAD
=======


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

>>>>>>> 56301c45d9e64c002b8f6b05440fb3f9ad799768
        private void ChangeBackgroundColor(object parameter)
        {
            ColorDictionaryResource.ColorHexKey = parameter.ToString();
            if (focusedTextBox != null)
            {
                focusedTextBox.Focus();
            }
            else
            {
                soundPlayers["focus-sound"].Play();
            }
        }

        private void FillCollectionTextBoxCells()
        {
            FullBorad = new ObservableCollection<CellContect>();

            for (int i = 0; i < 81; i++)
            {
                var locations = BoardUtility.GenerateIndexRowColSubMatrix(i);
                int num = ArrBorad[locations.Item1, locations.Item2];
                // bool isReadOnly = num != 0;
                var cell = new CellContect()
                {
                    CellTag = locations
                };
                FillCell(cell, num);
                FullBorad.Add(cell);
            }
        }

        public void RestOrNewGame(bool isRest, string _level = null)
        {
            try
            {
                if (isRest)
                {
                    this.gameBoard.InitializeBoard(out ArrBorad);
                }
                else
                {
                    Enum.TryParse(_level.Replace(" ", ""), out DifficultyLevel level);
                    this.gameBoard.GenerateNewBoard(out ArrBorad, level);
                }

                for (int i = 0; i < FullBorad.Count; i++)
                {
                    var cell = FullBorad[i];
                    int num = ArrBorad[cell.CellTag.Item1, cell.CellTag.Item2];
                    FillCell(cell, num);
                }
            }
            catch { }

        }

        private void FillCell(CellContect cell, int num)
        {
            bool isReadOnly = num != 0;
            cell.Content = num + "";
            cell.CellBackgroundColor = ModeCellBackgroundColor.Without;
            cell.CellForegroundColor = isReadOnly ? ModeCellForegroundColor.ReadOnly : ModeCellForegroundColor.Normal;
            cell.IsReadOnly = isReadOnly;
        }


        internal void ExecuteAfterInsertVal(object obj)
        {
            try
            {
                var tag = focusedTextBox.Tag as Tuple<int, int, int>;
                if (focusedTextBox.Text != ArrBorad[tag.Item1, tag.Item2].ToString() && !focusedTextBox.IsReadOnly)
                {
                    int val = focusedTextBox.Text != "" ? int.Parse(focusedTextBox.Text) : 0;

                    UpdateArrBorad(val, tag.Item1, tag.Item2);
                }

            }
            catch { }

        }


        /// <summary>
        /// Updates the foreground color of cells previously marked as error to their normal mode.
        /// The function then passes as an argument the tag of the position of the argument 
        /// in the board to update all the background colors of the entire board
        /// </summary>
        /// <param name="liIndexes">A list of integer arrays containing row and column indexes of cells to update.</param>
        /// <param name="cellCurrent">The contents of the current cell</param>
        private void UpdateErrorCellsForeground(List<int[]> liIndexes, CellContect cellCurrent)
        {
            liIndexes.ForEach(indexes =>
            {
                int index = indexes[0] * 9 + indexes[1] % 9;

                if (!FullBorad[index].IsReadOnly)
                {
                    FullBorad[index].CellForegroundColor = ModeCellForegroundColor.Normal;
                }
            });
            UpdateBoradBackgroundColor(cellCurrent.CellTag);
        }


        private void UpdateArrBorad(int num, int r, int c)
        {

            var cell = FullBorad[r * 9 + c % 9];
            int preValueChange = ArrBorad[r, c];
            if (num != 0)
            {

                if (gameBoard.IsBoardValid(num, r, c))
                {
                    cell.CellForegroundColor = ModeCellForegroundColor.Normal;
                    soundPlayers["success-sound"].Play();
                }
                else
                {
                    cell.CellForegroundColor = ModeCellForegroundColor.Error;
                    soundPlayers["error-sound"].Play();
                }

                ArrBorad[r, c] = num;
                UpdateErrorCellsForeground(gameBoard.GetIndexesLocationError(preValueChange, r, c), cell);

            }
            else if (ArrBorad[r, c] != 0)
            {

                cell.CellForegroundColor = ModeCellForegroundColor.Normal;
                ArrBorad[r, c] = num;
                UpdateErrorCellsForeground(gameBoard.GetIndexesLocationError(preValueChange, r, c), cell);
            }

            if (IsBoardSolved())
            {
                actionAfterEndGame();
            }

        }


        private void FocusTextBox(object obj)
        {
            var eventArgs = (obj as RoutedEventArgs);
            focusedTextBox = (eventArgs).OriginalSource as TextBox;
            soundPlayers["focus-sound"].Play();
            UpdateBoradBackgroundColor(focusedTextBox.Tag as Tuple<int, int, int>);
        }

        private void UpdateBoradBackgroundColor(Tuple<int, int, int> tag)
        {
            FullBorad.ToList().ForEach(cell =>
            {
                cell.CellBackgroundColor = ModeCellBackgroundColor.Without;
                if (cell.CellTag.Item1 == tag.Item1 &&
                    cell.CellTag.Item2 == tag.Item2)
                {
                    cell.CellBackgroundColor = ModeCellBackgroundColor.Focus;
                }
                else if (cell.CellForegroundColor == ModeCellForegroundColor.Error)
                {
                    cell.CellBackgroundColor = ModeCellBackgroundColor.SameValueError;
                }

                else if (cell.Content != "" && cell.Content == focusedTextBox.Text)
                {
                    cell.CellBackgroundColor = ModeCellBackgroundColor.SameValue;
                }

                else if (cell.CellTag.Item1 == tag.Item1 ||
                         cell.CellTag.Item2 == tag.Item2 ||
                         cell.CellTag.Item3 == tag.Item3)
                {
                    cell.CellBackgroundColor = ModeCellBackgroundColor.Related;
                }

            });

        }

        private bool IsBoardSolved()
        {
            return FullBorad.All(
                cell =>
                cell.Content != "" && (
                cell.CellForegroundColor == ModeCellForegroundColor.Normal ||
                cell.CellForegroundColor == ModeCellForegroundColor.ReadOnly)
             );

            //return TextBoxCells.OfType<TextBox>().All(
            //   textbox =>
            //   !(textbox.Text == "" && textbox != focusedTextBox || textbox.Foreground == Brushes.Red));

        }



        //private void UpdateCells(bool isNewGame = true, dynamic li_indexes = null)
        //{
        //    if (isNewGame)
        //    {
        //        // CellCurrentTag = null;
        //        if (focusedTextBox != null)
        //        {
        //            //focusedTextBox.Background = Brushes.Goldenrod;
        //            focusedTextBox = null;
        //        }

        //        //  Cell = new CellContect();
        //        //TextBoxCells.OfType<TextBox>().ToList().ForEach(tb => tb.Foreground = Brushes.Black);
        //        return;

        //    }
        //    else if (/*li_indexes != null &&*/ li_indexes.Count > 0)
        //    {
        //        foreach (var indexes in li_indexes)
        //        {
        //            int index = indexes[0] * 9 + indexes[1] % 9;
        //            if (!TextBoxCells[index].IsReadOnly)
        //            {
        //                TextBoxCells[index].Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x72, 0xE3));
        //            }
        //        }
        //    }
        //    if (IsBoardSolved())
        //    {
        //        actionAfterEndGame();
        //    }

        //}



        //public int GetMoveValidInBorad(int row_or_col)
        //{

        //    //A function that returns the legal position of a slot
        //    //This function is actually used to test the arrow keys pressed by the user

        //    //int rows = (board as int[,]).GetLength(0);
        //    int rows = 9;
        //    if (row_or_col % rows == 0)
        //    {
        //        return 0;
        //    }
        //    if (row_or_col < 0)
        //    {
        //        return rows - 1;
        //    }
        //    return row_or_col % rows;
        //}


        //private void UpdateCell(int[] parms)
        //{
        //    var bo = (board as int[,]);
        //    int row = parms[1], col = parms[2];
        //    int result = bo[row, col] == parms[0] ? 0 : parms[0];
        //    int index = row * 9 + col % 9;
        //    if (result != 0)
        //    {

        //        if (gameBoard.IsBoardValid(parms))
        //        {
        //            TextBoxCells[index].Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x72, 0xE3));
        //            soundPlayers["success-sound"].Play();
        //        }
        //        else
        //        {
        //            TextBoxCells[index].Foreground = Brushes.Red;
        //            soundPlayers["error-sound"].Play();
        //        }



        //        //  TextBoxCells[index].Foreground = gameBoard.IsBoardValid(parms) ? new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x72, 0xE3)) : Brushes.Red;
        //        // Update the foreground for the appearances of this replaced value 
        //        int preValueChange = bo[row, col];
        //        bo[row, col] = result;
        //        parms[0] = preValueChange;
        //        UpdateCells(false, gameBoard.GetIndexesLocationError(parms));
        //    }
        //    else if (bo[row, col] != 0)
        //    {
        //        bo[row, col] = result;
        //        UpdateCells(false, gameBoard.GetIndexesLocationError(parms));
        //    }
        //    //Cell.Content = result.ToString();

        //}

        //public void GenerateTextBoxCell(int[] indexesCell)
        //{
        //    var textBox = new TextBoxCell();
        //    textBox.SelectionChanged += TextBox_SelectionChanged;
        //    textBox.GotFocus += TextBox_GotFocus;
        //    textBox.PreviewKeyDown += TextBox_PreviewKeyDown;
        //    //  textBox.PreviewMouseRightButtonDown += TextBox_PreviewMouseRightButtonDown;
        //    Func<IValueConverter, Binding> createBinding = (convter) =>
        //     {
        //         Binding bind = new Binding("Board")
        //         {
        //             Source = this,
        //             Converter = convter,
        //             //  ConverterParameter = textBox.IsReadOnly,
        //             Mode = BindingMode.TwoWay,
        //             UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,

        //         };
        //         return bind;
        //     };

        //    textBox.SetBinding(TextBox.IsReadOnlyProperty, createBinding(new CellReadOnlyConverter(indexesCell)));
        //    textBox.SetBinding(TextBox.TagProperty, createBinding(new CellTagConverter(indexesCell)));
        //    textBox.SetBinding(TextBox.TextProperty, createBinding(new CellTextConverter(indexesCell)));


        //    var multiBinding = new MultiBinding
        //    {
        //        Converter = new MultiBackgroundConverter(),
        //        ConverterParameter = textBox // new object[] { textBox.Tag, textBox }
        //    };

        //    multiBinding.Bindings.Add(new Binding("Cell.CellCurrentTag")
        //    {
        //        Mode = BindingMode.OneWay,
        //        Source = this,
        //        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
        //    });

        //    multiBinding.Bindings.Add(new Binding("Cell.Content")
        //    {
        //        Mode = BindingMode.OneWay,
        //        Source = this,
        //        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
        //    });


        //    textBox.SetBinding(TextBox.BackgroundProperty, multiBinding);


        //    //textBox.SetBinding(TextBox.BackgroundProperty, new Binding(
        //    //      "Cell")
        //    //{
        //    //    Mode = BindingMode.OneWay,
        //    //    Source = this,
        //    //    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
        //    //    Converter = new CellBackgroundConverter(),
        //    //    ConverterParameter = new object[] { textBox.Tag, textBox.Text },
        //    //});


        //    TextBoxCells.Add(textBox);

        //}



        //private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        //{
        //    //Prevent caret of textbox 
        //    if (sender != null)
        //    {
        //        TextBox tb = (sender as TextBox);
        //        e.Handled = true;
        //        if (tb.SelectionLength != 0)
        //            tb.SelectionLength = 0;
        //    }
        //}

        //internal void TextBox_GotFocus(object sender, RoutedEventArgs e)
        //{

        //    focusedTextBox = (TextBox)sender;
        //    //  CellCurrentTag = (dynamic)(focusedTextBox).Tag;

        //    // Cell.CellCurrentTag = (dynamic)(focusedTextBox).Tag;
        //    //  Cell.Content = focusedTextBox.Text;
        //    soundPlayers["focus-sound"].Play();
        //}

        #endregion
    }
}
