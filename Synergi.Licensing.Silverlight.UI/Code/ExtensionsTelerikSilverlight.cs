using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls.Charting;
using Telerik.Windows.Data;
using System.Collections.Generic;

namespace Synergi.Licensing.Silverlight.UI
{
    internal static class ExtensionsTelerikSilverlight
    {
        internal static string SelectedText(this RadComboBox combobox)
        {
            string result = string.Empty;

            if (combobox.SelectedItem != null)
            {
                if (combobox.SelectedItem is KeyValuePair<int, string>)
                {
                    result = ((KeyValuePair<int, string>)combobox.SelectedItem).Value;
                }
                else if (combobox.SelectedItem is RadComboBoxItem)
                {
                    result = ((RadComboBoxItem)combobox.SelectedItem).Content.ToString();
                }
                else if (combobox.SelectionBoxItem != null)
                {
                    result = combobox.SelectionBoxItem.ToString();
                }
                else
                {
                    result = String.Empty;
                }
            }

            return result;
        }

        private const string _show = "Show";
        private const string _hide = "Hide";
        internal static bool IsHideItem(this RadMenuItem menuItem)
        {
            if (menuItem.Header == null)
            {
                return false;
            }
            else
            {
                return (menuItem.Header.ToString().IndexOf(_hide) != -1); 
            }
        }

        internal static void ToggleShowToHide(this RadMenuItem menuItem)
        {
            ToggleShowHide(menuItem, _show, _hide);
        }

        internal static void ToggleHideToShow(this RadMenuItem menuItem)
        {
            ToggleShowHide(menuItem, _hide, _show);
        }

        private static void ToggleShowHide(RadMenuItem menuItem, string from, string to)
        {
            if (menuItem.Header != null)
            {
                menuItem.Header = menuItem.Header.ToString().Replace(from, to);
            }
        }

        internal static void ToggleShowHideItems(this RadMenuItem menuItem)
        {
            RadMenuItem parent = menuItem.Parent as RadMenuItem;
            foreach (RadMenuItem childItems in parent.Items)
            {
                childItems.ToggleHideToShow();
            }
            menuItem.ToggleShowToHide();
        }

        internal static void ForceUpdateLayout(this RadGridView grid)
        {
            var itemsSource = grid.ItemsSource;
            grid.ItemsSource = null;
            grid.ItemsSource = itemsSource;
        }

		public static string GetMemberName(this IGroupDescriptor descriptor)
		{
			var groupDescriptor = descriptor as GroupDescriptor;
			if (groupDescriptor != null)
			{
				return groupDescriptor.Member;
			}

			var columnGroupDescriptor = descriptor as ColumnGroupDescriptor;
			if (columnGroupDescriptor != null)
			{
				var boundColumn = columnGroupDescriptor.Column as GridViewBoundColumnBase;
				if (boundColumn != null)
				{
					return boundColumn.GetDataMemberName();
				}
			}

			return null;
		}
	}
}
