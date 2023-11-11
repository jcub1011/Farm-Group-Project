using Farm_Group_Project.InventorySystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Farm_Group_Project.VisualizationItems
{
    /// <summary>
    /// Interaction logic for VisualObject.xaml
    /// </summary>
    public partial class VisualObject : UserControl
    {
        public static readonly DependencyProperty ChildObjectsProperty = DependencyProperty.Register(nameof(ChildObjects), typeof(ObservableCollection<IInventoryItem>), typeof(VisualObject));

        public ObservableCollection<IInventoryItem> ChildObjects
        {
            get => (ObservableCollection<IInventoryItem>)GetValue(ChildObjectsProperty);
            set => SetValue(ChildObjectsProperty, value);
        }

        public static readonly DependencyProperty DisplayNameProperty = DependencyProperty.Register(nameof(DisplayName), typeof(string), typeof(VisualObject));

        public string DisplayName
        {
            get => (string)GetValue(DisplayNameProperty);
            set => SetValue(DisplayNameProperty, value);
        }

        public VisualObject()
        {
            InitializeComponent();
        }
    }
}
