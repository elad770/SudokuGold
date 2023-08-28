using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using BoardGame.VIewModels;

namespace BoardGame.Utilities
{
    class TextBoxBehavior : Behavior<TextBox>
    {

        protected override void OnAttached()
        {
            this.AssociatedObject.SelectionChanged += TextBox_SelectionChanged;
            this.AssociatedObject.PreviewKeyDown += (sen, e) =>
            {

                var focusedTextBox = (sen as TextBox);
                var itemsControl = FindParent<ItemsControl>(focusedTextBox);
                if (e.Key >= Key.Left && e.Key <= Key.Down)
                {
                    var tag = (sen as TextBox).Tag as Tuple<int, int, int>;
                    int rowIndex = tag.Item1, columnIndex = tag.Item2, subMatrix = tag.Item3;
                    switch (e.Key)
                    {
                        case Key.Left:
                            columnIndex = BoardUtility.GetMoveValidInBorad(--columnIndex);
                            break;
                        case Key.Right:
                            columnIndex = BoardUtility.GetMoveValidInBorad(++columnIndex);
                            break;
                        case Key.Up:
                            rowIndex = BoardUtility.GetMoveValidInBorad(--rowIndex);
                            break;
                        case Key.Down:
                            rowIndex = BoardUtility.GetMoveValidInBorad(++rowIndex);
                            break;
                    }
                    ContentPresenter c = (ContentPresenter)itemsControl.ItemContainerGenerator.ContainerFromItem(itemsControl.Items[rowIndex * 9 + columnIndex % 9]);
                    (c.ContentTemplate.FindName("tb", c) as TextBox).Focus();
                }
                else if (!focusedTextBox.IsReadOnly)
                {
                    if (e.Key == Key.Back)
                    {
                        focusedTextBox.Text = "0";
                    }
                    else if (e.Key >= Key.D0 && e.Key <= Key.D9)
                    {
                        string digit = e.Key.ToString().Substring(1);
                        if (digit == focusedTextBox.Text || digit == "0")
                        {
                            focusedTextBox.Text = "0";
                        }
                        else
                        {
                            focusedTextBox.Text = digit;
                        }

                    }

                    if (itemsControl.DataContext is BoardViewModel viewModel)
                    {
                        viewModel.AfterInsertValToCell.Execute(null);
                    }

                    e.Handled = true;
                }


            };
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //Prevent caret of textbox 
            if (sender != null)
            {
                TextBox tb = (sender as TextBox);
                e.Handled = true;
                if (tb.SelectionLength != 0)
                    tb.SelectionLength = 0;
            }
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);

            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as T;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.SelectionChanged -= TextBox_SelectionChanged;
        }
    }
}
