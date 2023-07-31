using BoardGame.Models;
using Microsoft.Xaml.Behaviors.Layout;
using SudokuGame.UserControls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace BoardGame.Converters
{



    public class MultiBackgroundConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            string keyColor = ColorDictionaryResource.ColorHexKey.ToLower();
            var valueColor = ColorDictionaryResource.DictFocusColors[keyColor];

            var tb = parameter as TextBox;
            if (tb.Tag.Equals(values[0]))
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString(keyColor));
            }

            string txt = values[1].ToString();
            if (txt != "" && txt == tb.Text)
            {
                string hexColorGreen = "#50C878";
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexColorGreen));
            }


            var tags1 = (Tuple<int, int, int>)values[0];
            var tags2 = (Tuple<int, int, int>)tb.Tag;

            if (tags1 != null && (tags1.Item1 == tags2.Item1 || tags1.Item2 == tags2.Item2 ||
                tags1.Item3 == tags2.Item3))
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString(valueColor));
            }

            return Brushes.White;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }



    //internal class CellBackgroundConverter : IValueConverter
    //{

    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        ////  if (parameter is string praString)
    //        ////  {
    //        //return Brushes.White;
    //        ////   }

    //        //   var parameterObj = parameter as object[];
    //        string keyColor = ColorDictionaryWrapper.ColorHexKey.ToLower();
    //        var valueColor = ColorDictionaryWrapper.DictFocusColors[keyColor];
    //        if (parameter.Equals(value))
    //        {
    //            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(keyColor));
    //        }
    //        var tags1 = (Tuple<int, int, int>)value;
    //        // var tags2 = (Tuple<int, int, int>)parameterObj[0];
    //        var tags2 = (Tuple<int, int, int>)parameter;

    //        if (tags1 != null && (tags1.Item1 == tags2.Item1 || tags1.Item2 == tags2.Item2 ||
    //            tags1.Item3 == tags2.Item3))
    //        {
    //            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(valueColor));
    //        }

    //        return Brushes.White;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {

    //        return null;
    //    }

    //}
}
