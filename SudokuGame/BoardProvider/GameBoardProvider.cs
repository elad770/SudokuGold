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

    public class GameBoardProvider : ICombinedGameBoardProvider
    {

        private int[,] _array;
        private int[,] _arrClone;
        private string filePath = "borads_game\\boards.json";
        private Dictionary<string, int[][,]> difficultyBorads;



        public DifficultyLevel Level { get; private set; }



        public GameBoardProvider()
        {
            ReadJsonOfBorad();
            int[,] arr = new int[10, 10];
        }

        private void ReadJsonOfBorad()
        {

            // Read the JSON file content
            string jsonContent = File.ReadAllText(filePath);

            // Deserialize the JSON into a dictionary
            difficultyBorads = JsonConvert.DeserializeObject<Dictionary<string, int[][,]>>(jsonContent);
        }

        public void GenerateNewBoard(out int[,] board, DifficultyLevel level)
        {
            //Level = level;
            var borads = difficultyBorads[level.ToString()];
            int boradRandom = new Random().Next(0, borads.Length);
            _arrClone = (int[,])borads[boradRandom].Clone();
            InitializeBoard(out board);
        }

        public void InitializeBoard(out int[,] board)
        {
            board = _array = (int[,])_arrClone.Clone();
        }

        //public bool IsBoardValid(int[] parms)
        public bool IsBoardValid(int value, int indexRow, int indexCol)
=======
        public bool IsBoardValid(int[] parms)
>>>>>>> 56301c45d9e64c002b8f6b05440fb3f9ad799768
        {
            //if (parms.Length != 3)
            //{
            //    throw new ArgumentException("Invalid number of parameters. Please provide the value, row index, and column index.");
            //}
            //  int value = parms[0], indexRow = parms[1], indexCol = parms[2];

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

<<<<<<< HEAD
        //public List<int[]> GetIndexesToChnageColor(params int[] para)
        //{
        //    return GetIndexesToChnageColor(new int[] { para[0], para[1], para[2] }, para[3]);
        //}

        public List<int[]> GetIndexesLocationError(params int[] prmas)
=======
        public List<int[]> GetIndexesLocationError(int[] prmas)
>>>>>>> 56301c45d9e64c002b8f6b05440fb3f9ad799768
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
