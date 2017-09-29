using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace SveaFinansTest.Behaviors
{
    public class DataGridScrollToRowBehavior : Behavior<DataGrid>
    {
        public static readonly DependencyProperty FocusWhileSelectionProperty =
            DependencyProperty.RegisterAttached(
                "FocusWhileSelection",
                typeof(bool),
                typeof(DataGridScrollToRowBehavior),
                new UIPropertyMetadata(false, OnFocusWhileSelectionChanged));

        public static bool GetFocusWhileSelection(DataGridRow dataGridRow)
        {
            return (bool)dataGridRow.GetValue(FocusWhileSelectionProperty);
        }
        public static void SetFocusWhileSelection(DataGridRow dataGridRow, bool value)
        {
            dataGridRow.SetValue(FocusWhileSelectionProperty, value);
        }
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
        }
        private static void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var datagrid = sender as DataGrid;
            if (datagrid?.SelectedItem != null)
            {
                datagrid.Dispatcher.BeginInvoke((Action)(() =>
                {
                    datagrid.UpdateLayout();
                    if (datagrid.SelectedItem != null)
                    {
                        datagrid.ScrollIntoView(datagrid.SelectedItem);
                    }
                }));
            }
        }
        private static void OnFocusWhileSelectionChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            var item = depObj as DataGridRow;
            if (item == null)
                return;

            if (e.NewValue is bool == false)
                return;

            if ((bool)e.NewValue)
                item.Selected += OndataGridRowSelected;
            else
                item.Selected -= OndataGridRowSelected;
        }
        private static void OndataGridRowSelected(object sender, RoutedEventArgs e)
        {
            var row = e.OriginalSource as DataGridRow;
            if (Keyboard.FocusedElement is DataGridCell || row == null) return;
            row.Focusable = true;
            Keyboard.Focus(row);
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
        }
    }
}

