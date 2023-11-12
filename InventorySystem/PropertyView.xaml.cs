using Farm_Group_Project.VisualizationItems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Farm_Group_Project.InventorySystem
{
    /// <summary>
    /// Class for conversions in XAML.
    /// </summary>
    public class DoubleToStringConverter : IValueConverter
    {
        /// <summary>
        /// Converts a double to a string.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">Parameters.</param>
        /// <param name="culture">?</param>
        /// <returns>string</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((double)value).ToString();
        }

        /// <summary>
        /// Converts a string to a double.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">Parameters.</param>
        /// <param name="culture">?</param>
        /// <returns>double</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse((string)value, out double output)) return output;
            return double.NaN;
        }
    }

    /// <summary>
    /// Interaction logic for PropertyView.xaml
    /// </summary>
    public partial class PropertyView : UserControl, INotifyPropertyChanged
    {
        bool _disablePropertyModification;
        public bool DisablePropertyModification
        {
            get => _disablePropertyModification;
            set
            {
                _disablePropertyModification = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisablePropertyModification)));
            }
        }

        InventoryItem? _itemToModify;
        public InventoryItem? ItemToModify
        {
            get => _itemToModify;
            set
            {
                // Prevent changing to drone when the item wasn't already a drone.
                if (TagList != null)
                {
                    if (value != null)
                    {
                        if (value.ItemTag != Tags.Drone && TagList.Contains(Tags.Drone)) TagList.Remove(Tags.Drone);
                        else if (value.ItemTag == Tags.Drone) TagList.Add(Tags.Drone);
                    }
                    else
                    {
                        if (TagList.Contains(Tags.Drone)) TagList.Remove(Tags.Drone);
                    }
                }
                _itemToModify = value;
                
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemToModify)));

                if (_itemToModify == null) return;
                // Disable tag editing if its a drone.
                IsTagChangeable = !(Tags.Drone == _itemToModify.ItemTag);
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        ObservableCollection<string>? _tagList;
        public ObservableCollection<string>? TagList
        {
            get => _tagList;
            set
            {
                _tagList = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TagList)));
            }
        }

        private bool IsTagChangeable
        {
            get => TagComboBox.IsEnabled;
            set => TagComboBox.IsEnabled = value;
        }

        public PropertyView()
        {
            TagList = TagEvaluator.GetTagList();
            InitializeComponent();
            IsTagChangeable = false;
        }

        private void OnTagSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("tag changed");
            // if (preventChangingToDrone && e.AddedItems.Contains(Tags.Drone)) TagComboBox.Text = e.RemovedItems.Cast<string>().FirstOrDefault("default");
        }
    }
}
