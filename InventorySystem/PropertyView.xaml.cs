using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Farm_Group_Project.InventorySystem
{
    public class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((double)value).ToString();
        }

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

        private void CheckValidNumber(object sender, TextCompositionEventArgs e)
        {
            //e.Handled = double.TryParse(e.Text, out _);
        }

        private void CheckValidPastedNumber(object sender, DataObjectPastingEventArgs e)
        {
            String text = (String)e.DataObject.GetData(typeof(String));

            if (!e.DataObject.GetDataPresent(typeof(String)))
            {
                e.CancelCommand();
            }
            else if (!double.TryParse(text, out _))
            {
                e.CancelCommand();
            }
        }
    }
}
