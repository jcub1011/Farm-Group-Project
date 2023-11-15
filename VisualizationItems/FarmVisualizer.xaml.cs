using Farm_Group_Project.InventorySystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
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
        public VirtualDrone? Drone { get; set; }

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
            Drone = null;
            ContentHolder.Children.Clear();
            if (Source == null)
            {
                return;
            }

            // Update drone situation.
            IInventoryItem? droneSource = null;
            foreach(IInventoryItem child in Source)
            {
                if (child.ItemTag == Tags.Drone)
                {
                    Drone = (VirtualDrone)child;
                    droneSource = child;
                    break;
                }
            }

            // Update items in farm visualizer.
            foreach (var item in Source)
            {
                if (item.ItemTag == Tags.Drone)
                {
                    continue;
                }
                ContentHolder.Children.Add(new VisualObject(item));
            }

            // Add drone last so it appears on top.
            if (droneSource != null) ContentHolder.Children.Add(new VisualObject(droneSource));
        }

        public Point FindPointForItem(IInventoryItem item)
        {
            var result = FindPointForItem(item, ContentHolder.Children);
            if (result == null) throw new Exception($"Unable to find {item.ItemName} in {nameof(FarmVisualizer)}.");

            var origin = ContentHolder.PointToScreen(new Point(0, 0));
            return (Point)((Point)result - origin);
        }

        private Point? FindPointForItem(IInventoryItem item, UIElementCollection source)
        {
            foreach(VisualObject child in source)
            {
                if (ReferenceEquals(child.SourceItem, item))
                {
                    return child.PointToScreen(new Point(0, 0));
                }

                var result = FindPointForItem(item, child.ObjectChildren);
                if (result == null) continue;
                else return result;
            }

            return null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateView();
        }
    }
}
