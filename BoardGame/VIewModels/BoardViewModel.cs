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

        #region Fields

        internal TextBox focusedTextBox;
        
        private Dictionary<string, SoundPlayer> soundPlayers;
        
        private Action actionAfterEndGame;
        
        private ICombinedGameBoardProvider gameBoard;

       


        private static int[,] ArrBorad;

        #endregion

        #region Propites


        public ICommand RestOrNewGameCommand { get; }

        public ICommand ChangeBackgroundColorCommand { get; }

        public ICommand FocusTextBoxCommand { get; }

        public ICommand AfterInsertValToCell { get; }

        public ObservableCollection<CellContect> FullBorad { get; set; }


        #endregion

        #region Constructor
        public BoardViewModel(ICombinedGameBoardProvider iGameBoard, Action actionAfterEndGame)
        {
            gameBoard = iGameBoard;
            gameBoard.GenerateNewBoard(out ArrBorad);
            FillCollectionTextBoxCells();

            RestOrNewGameCommand = new RelayCommand(RestOrNewGame);
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
                var cell = new CellContect()
                {
                    CellTag = locations
                };
                FillCell(cell, num);
                FullBorad.Add(cell);
            }
        }


        public void OptionalEraseReplaceNum(string num)
        {
            if (focusedTextBox != null && !focusedTextBox.IsReadOnly)
            {
                focusedTextBox.Text = focusedTextBox.Text != num ? num : "0";
                ExecuteAfterInsertVal(null);
            }
        }

        public void RestOrNewGame(object obj)
        {
            bool isRest = bool.Parse(obj.ToString());
            try
            {
                if (isRest)
                {
                    gameBoard.InitializeBoard(out ArrBorad);
                }
                else
                {
                    gameBoard.GenerateNewBoard(out ArrBorad);
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
        }

        #endregion
    }
}
