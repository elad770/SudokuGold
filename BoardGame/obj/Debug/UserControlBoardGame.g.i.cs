﻿#pragma checksum "..\..\UserControlBoardGame.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "560B58D9F1508DA96DB327975CA152671A2791DF91F03735901CC9B288BAA2FA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using BoardGame.Converters;
using BoardGame.Models;
using BoardGame.Resources;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using SudokuGame.UserControls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace SudokuGame.UserControls {
    
    
    /// <summary>
    /// UserControlBoardGame
    /// </summary>
    public partial class UserControlBoardGame : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 52 "..\..\UserControlBoardGame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel spTable;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\UserControlBoardGame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel stPanelColor;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\UserControlBoardGame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buLeft;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\UserControlBoardGame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buRight;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/BoardGame;component/usercontrolboardgame.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\UserControlBoardGame.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.spTable = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 2:
            this.stPanelColor = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.buLeft = ((System.Windows.Controls.Button)(target));
            
            #line 90 "..\..\UserControlBoardGame.xaml"
            this.buLeft.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.ArrowClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.buRight = ((System.Windows.Controls.Button)(target));
            
            #line 96 "..\..\UserControlBoardGame.xaml"
            this.buRight.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.ArrowClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

