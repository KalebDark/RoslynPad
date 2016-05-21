using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace RoslynPad.Controls
{
    internal class TreeListView : TreeView
    {
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TreeListViewItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is TreeListViewItem;
        }

        public GridViewColumnCollection Columns
        {
            get
            {
                if (_columns == null)
                {
                    _columns = new GridViewColumnCollection();
                }

                return _columns;
            }
        }

        private GridViewColumnCollection _columns;
    }

    internal class TreeListViewItem : TreeViewItem
    {
        private static readonly DependencyPropertyKey LevelPropertyKey = DependencyProperty.RegisterReadOnly(
            "Level", typeof(int), typeof(TreeListViewItem), new FrameworkPropertyMetadata());

        public static readonly DependencyProperty LevelProperty = LevelPropertyKey.DependencyProperty;

        public int Level => (int)GetValue(LevelProperty);

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TreeListViewItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is TreeListViewItem;
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            element.SetValue(LevelPropertyKey, Level + 1);
        }
    }

    internal sealed class LevelToIndentConverter : IValueConverter
    {
        private const double IndentSize = 19.0;

        public object Convert(object o, Type type, object parameter, CultureInfo culture)
        {
            return new Thickness((int)o * IndentSize, 0, 0, 0);
        }

        public object ConvertBack(object o, Type type, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}