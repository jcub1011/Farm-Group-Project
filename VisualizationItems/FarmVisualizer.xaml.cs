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
            DroneOverlay.Children.Clear();
            Drone = null;
            ContentHolder.Children.Clear();
            if (Source == null)
            {
                return;
            }

            // Update drone situation.
            foreach(IInventoryItem child in Source)
            {
                if (child.ItemTag == Tags.Drone)
                {
                    Drone = (VirtualDrone)child;
                    var modelDrone = new VirtualDrone(child);

                    // Connect locations of drones (this is only one way). Don't mind the hacky solution.
                    Drone.PropertyChanged += (sender, e) =>
                    {
                        if (e.PropertyName == nameof(Drone.X))
                        {
                            modelDrone.X = Drone.X;
                        }
                        else if (e.PropertyName == nameof(Drone.Y))
                        {
                            modelDrone.Y = Drone.Y;
                        }
                    };
                    break;
                }
            }

            // Update items in farm visualizer.
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
