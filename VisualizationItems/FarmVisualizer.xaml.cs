using Farm_Group_Project.InventorySystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for FarmVisualizer.xaml
    /// </summary>
    public partial class FarmVisualizer : UserControl
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ObservableCollection<IInventoryItem>), typeof(FarmVisualizer));

        public ObservableCollection<IInventoryItem> Source
        {
            get => (ObservableCollection<IInventoryItem>)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public FarmVisualizer()
        {
            InitializeComponent();
            UpdateView();
        }

        /// <summary>
        /// Call this to refresh the view.
        /// </summary>
        public void UpdateView()
        {
            ContentHolder.Children.Clear();

            if (Source == null) return;
            foreach (var item in Source)
            {
                ContentHolder.Children.Add(new VisualObject(item));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateView();
        }
    }
}
