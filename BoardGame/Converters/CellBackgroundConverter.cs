using BoardGame.Models;
using BoardGame.Utilities;
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



    public class CellBackgroundConverter : IValueConverter
    {
        Dictionary<ModeCellBackgroundColor, string> dictModeCellBackground;

        public CellBackgroundConverter()
        {
            dictModeCellBackground = new Dictionary<ModeCellBackgroundColor, string>();
            dictModeCellBackground.Add(ModeCellBackgroundColor.Without, "#FFFFFF");
            dictModeCellBackground.Add(ModeCellBackgroundColor.SameValue, "#90EE90");
            dictModeCellBackground.Add(ModeCellBackgroundColor.SameValueError, "#ffc3d6");
            dictModeCellBackground.Add(ModeCellBackgroundColor.Focus, "#E5E5E5");
            dictModeCellBackground.Add(ModeCellBackgroundColor.Related, "#E5E5E5");
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            string keyColor = ColorDictionaryResource.ColorHexKey.ToLower();
            var valueColor = ColorDictionaryResource.DictFocusColors[keyColor];
            dictModeCellBackground[ModeCellBackgroundColor.Focus] = keyColor;
            dictModeCellBackground[ModeCellBackgroundColor.Related] = valueColor;
            return dictModeCellBackground[(ModeCellBackgroundColor)value];
            // color green to SameValue 
            // string hexColorSameValue = "#90EE90";
            //string hexColorSameValue = "#8adaa5";

            //switch ((ModeCellBackgroundColor)value)
            //{
            //    case ModeCellBackgroundColor.Without:
            //        return Brushes.White;
            //    // return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#212F3C"));
            //    case ModeCellBackgroundColor.Focus:
            //        return new SolidColorBrush((Color)ColorConverter.ConvertFromString(keyColor));
            //    case ModeCellBackgroundColor.Related:
            //        return new SolidColorBrush((Color)ColorConverter.ConvertFromString(valueColor));
            //    case ModeCellBackgroundColor.SameValue:
            //        // color green
            //        return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#90EE90"));
            //    case ModeCellBackgroundColor.SameValueError:
            //        // color red
            //        return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff4d4d"));
            //    default:
            //        return Brushes.White;

            //}

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }
<<<<<<< HEAD

=======
>>>>>>> 56301c45d9e64c002b8f6b05440fb3f9ad799768
}
