using BoardGame.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BoardGame.Resources
{



    public static class ResourceHelper
    {



        //public static ResourceDictionary ResourceDictionary_ { get; } = new ResourceDictionary
        //{
        //    // Source = new Uri("Styles.xaml", UriKind.RelativeOrAbsolute)
        //    Source = new Uri("C:\\Users\\Elad\\source\\repos\\games2\\SudokuGame\\BoardGame\\Resources\\Styles.xaml", UriKind.RelativeOrAbsolute)
        //};
    }


    //public static class ResourceHelper
    //{
    //    public static ResourceDictionary ResourceDictionary { get; } = new ResourceDictionary();

    //    static ResourceHelper()
    //    {
    //        // Merge the Material Design dictionaries
    //        ResourceDictionary.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml") });
    //        ResourceDictionary.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Badged.xaml") });
    //        ResourceDictionary.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Button.xaml") });

    //        // Add other merged dictionaries as needed

    //        // Add the custom resources
    //        ResourceDictionary.Add("TextBoxBackgroundColor", new SolidColorBrush(Color.FromArgb(0xFF, 0x71, 0xC7, 0xEC)));
    //        ResourceDictionary.Add("CustomColor1", new SolidColorBrush(Color.FromArgb(0xFF, 0xBF, 0xD7, 0xF5)));
    //        ResourceDictionary.Add("CustomColor2", new SolidColorBrush(Color.FromArgb(0xFF, 0xC0, 0xD8, 0xC0)));
    //        ResourceDictionary.Add("CustomColor3", new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xCC, 0x00)));
    //        ResourceDictionary.Add("CustomColor4", new SolidColorBrush(Color.FromArgb(0xFF, 0xF6, 0x9C, 0x10)));

    //        // Add the ColorDictionary
    //        var colorDictionary = new ColorDictionary();
    //        colorDictionary.Add("#ff71c7ec", "#b4d7f5");
    //        colorDictionary.Add("#ffbfd7f5", "#b4e2f5");
    //        colorDictionary.Add("#ffc0d8c0", "#dcede6");
    //        colorDictionary.Add("#ffffffcc", "#fff2cc");
    //        colorDictionary.Add("#fff69c10", "#fce2ba");
    //        ResourceDictionary.Add("colorDictionary", colorDictionary);

    //        // Add the CustomTextBoxStyle
    //        var customTextBoxStyle = new Style(typeof(TextBoxCell));
    //        customTextBoxStyle.Setters.Add(new Setter(TextBoxCell.BackgroundProperty, Brushes.White));
    //        customTextBoxStyle.Setters.Add(new Setter(TextBoxCell.MaxLengthProperty, 1));
    //        customTextBoxStyle.Setters.Add(new Setter(TextBoxCell.ContextMenuProperty, new ContextMenu()));
    //        var trigger = new Trigger { Property = TextBoxCell.IsFocusedProperty, Value = true };
    //        trigger.Setters.Add(new Setter(TextBoxCell.BackgroundProperty, new DynamicResourceExtension("TextBoxBackgroundColor")));
    //        customTextBoxStyle.Triggers.Add(trigger);
    //        ResourceDictionary.Add("CustomTextBoxStyle", customTextBoxStyle);
    //    }
    //}



    //public static class ResourceHelper
    //{
    //    private static ResourceDictionary _styleResources;
    //    private static string key = "colorDictionary";
    //    public static void SetStyleResources(ResourceDictionary styleResources)
    //    {
    //        _styleResources = styleResources ?? new ResourceDictionary();
    //    }

    //    public static void SetStyleByIndex(int index)
    //    {
    //        try
    //        {
    //            if (_styleResources.Contains(key))
    //            {
    //                var styles = (Dictionary<string, string>)_styleResources[key];
    //                var changeColor = styles.ElementAt(index);
    //                styles[styles.ElementAt(index).ToString()] = styles[styles.ElementAt(0).ToString()];
    //                styles[styles.ElementAt(0).ToString()] = changeColor.ToString();
    //            }
    //        }

    //        catch
    //        {

    //        }

    //    }

    //    public static dynamic GetStyle()
    //    {

    //        if (_styleResources != null && _styleResources.Contains(key))
    //        {
    //            return ((Dictionary<string, string>)_styleResources[key]).ElementAt(0);
    //        }

    //        var dictColors = new Dictionary<string, string>();
    //        dictColors.Add("#ff71c7ec", "#b4d7f5");
    //        return dictColors.ElementAt(0);

    //    }


    //}

    //public static class ResourceHelper
    //{
    //    public static readonly DependencyProperty ExternalResourcesProperty =
    //        DependencyProperty.RegisterAttached(
    //            "ExternalResources",
    //            typeof(ResourceDictionary),
    //            typeof(ResourceHelper),
    //            new PropertyMetadata(null, OnExternalResourcesChanged));

    //    public static void SetExternalResources(DependencyObject element, ResourceDictionary value)
    //    {
    //        element.SetValue(ExternalResourcesProperty, value);
    //    }

    //    public static ResourceDictionary GetExternalResources(DependencyObject element)
    //    {
    //        return (ResourceDictionary)element.GetValue(ExternalResourcesProperty);
    //    }

    //    private static void OnExternalResourcesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //    {
    //        if (d is FrameworkElement frameworkElement)
    //        {
    //            if (e.NewValue is ResourceDictionary resourceDictionary)
    //            {
    //                foreach (var key in resourceDictionary.Keys)
    //                {
    //                    if (!frameworkElement.Resources.Contains(key))
    //                        frameworkElement.Resources.Add(key, resourceDictionary[key]);
    //                }
    //            }
    //        }

    //    }
    //}
    //public static class ResourceHelper
    //{
    //    private static ResourceDictionary _styleResources;

    //    public static void SetStyleResources(ResourceDictionary styleResources)
    //    {
    //        _styleResources = styleResources ?? throw new ArgumentNullException(nameof(styleResources));
    //    }

    //    public static IEnumerable<Brush> GetStyles()
    //    {
    //        if (_styleResources == null)
    //            return new List<Brush>() { new SolidColorBrush((Color)ColorConverter.ConvertFromString("#71c7ec")) };

    //        var styles = new List<Brush>();
    //        FindStylesRecursive(_styleResources, styles);
    //        return styles;
    //    }

    //    private static void FindStylesRecursive(ResourceDictionary dictionary, List<Brush> styles)
    //    {
    //        foreach (var key in dictionary.Keys)
    //        {
    //            styles.Add((Brush)dictionary[key]);
    //        }
    //    }
    //}
}

