using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BoardGame.Converters
{
    public class CellTagConverter : IValueConverter
    {

        int row, col;

        public CellTagConverter(int[] indexes)
        {
            row = indexes[0];
            col = indexes[1];
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is int[,] arr)
            {
                //int row = arr[0];
                //int column = arr[1];
                int submatrixRow = row / 3;
                int submatrixColumn = col / 3;
                int submatrixNumber = submatrixRow * 3 + submatrixColumn;
                return new Tuple<int, int, int>(row, col, submatrixNumber);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
