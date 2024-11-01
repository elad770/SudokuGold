using BoardGame.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SudokuGame.UserControls.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace SudokuGame.BoardProvider
{

    public enum DifficultyLevel
    {
        Impossible,
        Extreme,
        Hard,
        Medium,
        Easy,
        VeryEasy
    }


    public class GameBoardProvider : ICombinedGameBoardProvider
    {

        private int[,] _array;
        private int[,] _arrClone;
        private string filePath = "borads_game\\boards.json";
        private Dictionary<string, int[][,]> difficultyBorads;



        public DifficultyLevel Level { get; set; }



        public GameBoardProvider()
        {
            ReadJsonOfBorad();
        }

        private void ReadJsonOfBorad()
        {

            // Read the JSON file content
            string jsonContent = File.ReadAllText(filePath);

            // Deserialize the JSON into a dictionary
            difficultyBorads = JsonConvert.DeserializeObject<Dictionary<string, int[][,]>>(jsonContent);
        }

        private void InternalGenerateNewBoard()
        {
            var borads = difficultyBorads[Level.ToString()];
            int boradRandom = new Random().Next(0, borads.Length);
            _arrClone = (int[,])borads[boradRandom].Clone();
            _array = (int[,])_arrClone.Clone();
        }

        public void GenerateNewBoard(out int[,] borad)
        {
            InternalGenerateNewBoard();
            borad = _array;
        }

        public void InitializeBoard(out int[,] borad)
        {
            if(_arrClone == null)
            {
                InternalGenerateNewBoard();
            }
            borad = _array;
        }


        public bool IsBoardValid(int value, int indexRow, int indexCol)
        {
           
            var lambdaIsRowColSubMatrix = new Func<int, int, bool>((row, col) =>
            {
                if (indexRow == row && indexCol == col || value != _array[row, col])
                {
                    return true;
                }
                return false;
            });

            return IsRowOrColunmValid(0, indexCol, lambdaIsRowColSubMatrix, false) &&
                 IsRowOrColunmValid(indexRow, 0, lambdaIsRowColSubMatrix) &&
                 IsSubMatrixValid(indexRow, indexCol, lambdaIsRowColSubMatrix);
        }


        void Swap(ref int r, ref int c)
        {
            int temp = r;
            r = c;
            c = temp;
        }

        private bool IsRowOrColunmValid(int row, int col, Func<int, int, bool> lambda, bool isRow = true)
        {
            int r = 1, c = 0;
            if (isRow)
            {
                Swap(ref r, ref c);
            }
            int rows = _array.GetLength(0);
            for (int i = 0; i < rows; i++)
            {

                if (!lambda(row, col))
                {
                    return false;
                }
                row += r;
                col += c;
            }
            return true;
        }

        private bool IsSubMatrixValid(int row, int col, Func<int, int, bool> lambda)
        {
            int r = row % 3, c = col % 3;
            for (int i = 0; i < 3; i++)
            {
                int x = row - r, y = col - c;

                for (int j = 0; j < 3; j++)
                {
                    if (!lambda(x + i, y + j))
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        public List<int[]> GetIndexesLocationError(params int[] prmas)
        {

            List<int[]> liIndexesChnage = new List<int[]>();
            int value = prmas[0], row_current = prmas[1], col_current = prmas[2];

            int potentialIndexRow = -1, potentialIndexCol = -1;

            var lambdaPotentialIndex = new Func<int, int, bool>((r, c) =>
            {
                if (_array[r, c] == value)
                {

                    if (potentialIndexRow == -1)
                    {
                        potentialIndexRow = r;
                        potentialIndexCol = c;
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            });

            void AddIndexToList()
            {
                // check if index potential valid 
                if (potentialIndexRow != -1 && IsBoardValid(value, potentialIndexRow, potentialIndexCol))
                {
                    liIndexesChnage.Add(new int[] { potentialIndexRow, potentialIndexCol });
                }
                potentialIndexRow = potentialIndexCol = -1;
            }

            //Check is find index potential to chnage in col
            IsRowOrColunmValid(0, col_current, lambdaPotentialIndex, false);
            AddIndexToList();

            //Check is find index potential to chnage in row
            IsRowOrColunmValid(row_current, 0, lambdaPotentialIndex);
            AddIndexToList();

            //Check is find index potential to chnage in sub matrix
            IsSubMatrixValid(row_current, col_current, lambdaPotentialIndex);
            AddIndexToList();

            return liIndexesChnage;
        }

    }
}
