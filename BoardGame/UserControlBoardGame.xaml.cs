using BoardGame.Converters;
using BoardGame.Interfaces;
using BoardGame.Models;
using BoardGame.Properties;
using BoardGame.Utilities;
using BoardGame.VIewModels;
using Microsoft.Xaml.Behaviors;
using Newtonsoft.Json.Linq;
using PropertyChanged;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;


namespace SudokuGame.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlBoardGame.xaml
    /// </summary>


    //[AddINotifyPropertyChangedInterface]
    public partial class UserControlBoardGame : UserControl
    {
        public bool IsPanelColorVisable { get; set; } = true;

        public UserControlBoardGame(BoardViewModel boradVM)
        {
            InitializeComponent();
            DataContext = boradVM;
        }

    }


}

