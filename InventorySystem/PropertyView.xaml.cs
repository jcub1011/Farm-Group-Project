using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
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
        InventoryItem? _itemToModify;
        public InventoryItem? ItemToModify
        {
            get => _itemToModify;
            set
            {
                _itemToModify = value;
                Debug.WriteLine($"New item has been set. {PropertyChanged}");
                
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemToModify)));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public PropertyView()
        {
            InitializeComponent();
        }
    }
}
