using Newtonsoft.Json;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace BoardGame.Models
{
    //[AddINotifyPropertyChangedInterface]
    public class TextBoxCell : TextBox
    {
        public bool IsCellNote { get; set; }

    }

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


    //static ColorDictionaryWrapper()
    //    {
    //        DictFocusColors = new ColorDictionary();
    //        DictFocusColors.Add("#ff71c7ec", "#b4d7f5");
    //        DictFocusColors.Add("#ffbfd7f5", "#b4e2f5");
    //        DictFocusColors.Add("#ffc0d8c0", "#dcede6");
    //        DictFocusColors.Add("#ffffffcc", "#fff2cc");
    //        DictFocusColors.Add("#fff69c10", "#fce2ba");
    //        DictFocusColors.Add("#ffb99a87", "#c9beb1");
    //        DictFocusColors.Add("#fff79489", "#f4b9b8");
    //        DictFocusColors.Add("#ff9c90c1", "#b9b1d3");

    //        ColorHexKey = DictFocusColors.Keys.FirstOrDefault();
    //    }

    //}
    public class ColorDictionary : Dictionary<string, string> { }

    [AddINotifyPropertyChangedInterface]
    public class CellContect
    {
        public Tuple<int, int, int> CellCurrentTag { get; set; }
        public string Content { get; set; } = "";
    }
}
