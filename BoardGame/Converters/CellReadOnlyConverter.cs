using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BoardGame.Converters
{

    internal class CellReadOnlyConverter : IValueConverter
    {
        int row, col;

        public CellReadOnlyConverter(int[] indexes)
        {
            this.row = indexes[0];
            this.col = indexes[1];
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int[,])value)[row, col] != 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return null;
        }
    }
}
