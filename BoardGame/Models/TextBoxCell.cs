using BoardGame.Utilities;
using BoardGame.VIewModels;
using PropertyChanged;
using System;
using System.Windows.Controls;


namespace BoardGame.Models
{
    [AddINotifyPropertyChangedInterface]
    public class TextBoxCell : TextBox
    {
        //   public bool IsCellNote { get; set; }

    }


    [AddINotifyPropertyChangedInterface]
    public class CellContect //: CellContectBase
    {
        public Tuple<int, int, int> CellTag { get; set; }


        public bool IsReadOnly { get; set; }

        public ModeCellForegroundColor CellForegroundColor { get; set; }

        public ModeCellBackgroundColor CellBackgroundColor { get; set; }


        private string _content;

        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value != "0" ? value : "";
            }
        }


    }
}
