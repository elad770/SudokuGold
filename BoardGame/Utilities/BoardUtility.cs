using BoardGame.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BoardGame.Utilities
{


    internal static class BoardUtility
    {

        public static Thickness OrderBorderThickness(int i, int j)
        {
            Thickness thickness = new Thickness();
            //  Border Thickness of borad
            if (i % 3 == 0)
            {
                thickness = new Thickness(0, 1.5, 0, 0);
            }
            else if (i == 8)
            {
                thickness = new Thickness(0, 0, 0, 1.5);
            }
            if (j % 3 == 0)
            {
                thickness = new Thickness(1.5, thickness.Top, 0, thickness.Bottom);
            }
            else if (j == 8)
            {
                thickness = new Thickness(0, thickness.Top, 1.5, thickness.Bottom);
            }

            return thickness;
        }

        public static int GetMoveValidInBorad(int row_or_col)
        {

            //A function that returns the legal position of a slot
            //This function is actually used to test the arrow keys pressed by the user

            //int rows = (board as int[,]).GetLength(0);
            int rows = 9;
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

        public static Tuple<int, int, int> GenerateIndexRowColSubMatrix(int index)
        {
            int row = index / 9, col = index % 9;
            int submatrixRow = row / 3;
            int submatrixColumn = col / 3;
            int submatrixNumber = submatrixRow * 3 + submatrixColumn;
            return new Tuple<int, int, int>(row, col, submatrixNumber);
        }
    }


    public class ColorDictionary : Dictionary<string, string> { }

    public static class ColorDictionaryResource
    {
        public static ColorDictionary DictFocusColors { get; private set; } = new ColorDictionary();

        public static string ColorHexKey { get; set; }

        static ColorDictionaryResource()
        {

            try
            {
                string soundFilePath = System.IO.Path.Combine("..", "..", "..", "BoardGame/Resources");
                string jsonString = File.ReadAllText(soundFilePath + "/ColorsProvider.json");
                var dictFocusColors = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
                foreach (var kvp in dictFocusColors)
                {
                    DictFocusColors.Add(kvp.Key, kvp.Value);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, e.g., log or throw
            }
            ColorHexKey = DictFocusColors.Keys.FirstOrDefault();
        }
    }
}
