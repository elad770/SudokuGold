using BoardGame.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace BoardGame.Converters
{
    public class CellForegroundColorConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var mode = (ModeCellForegroundColor)value;

            if (mode == ModeCellForegroundColor.ReadOnly)
            {
                return Brushes.Black;
            }
            if (mode == ModeCellForegroundColor.Error)
            {
                return Brushes.Red;
            }
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0072e3"));
            //return mode == ModeCellColor.Error ? Brushes.Red :
            //    mode == ModeCellColor.ReadOnly ? Brushes.Black :
            //    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0072e3"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return null;
        }


    }
}
