using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace BoardGame.Converters
{


    public class CellTextConverter : IValueConverter
    {
        int row, col;

        public CellTextConverter(int[] indexes)
        {
            this.row = indexes[0];
            this.col = indexes[1];
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            string cellValue = ((int[,])value)[row, col].ToString();
            return cellValue != "0" ? cellValue : "";

            //string cellValue = value.ToString();
            //return cellValue != "0" ? cellValue : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            string txt = value.ToString();
            // txt = txt.Length > 1 ? txt[1].ToString() : txt;
            if (Regex.IsMatch(txt, "^[0-9]$") || txt == "")
            {
                int val = txt != "" ? int.Parse(txt) : 0;
                return new int[] { val, row, col };
            }


            return null;
        }
    }

}
