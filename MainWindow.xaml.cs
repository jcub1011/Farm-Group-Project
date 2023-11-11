using Farm_Group_Project.InventorySystem;
using Farm_Group_Project.VisualizationItems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Farm_Group_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var building = new InventoryItem("Building 1", Tags.Building, new double[] { 300, 100 }, new double[] { 100, 100 }, 10000);
            var building2 = new InventoryItem("Building 2", Tags.Building, new double[] { 10, 10 }, new double[] { 90, 90 }, 10000);
            var item1 = new InventoryItem("Item 1", Tags.Equipment, new double[] { 10, 10 }, new double[] { 50, 50 }, 10);
            var item2 = new InventoryItem("Item 2", Tags.Equipment, new double[] { 10, 10 }, new double[] { 50, 50 }, 10);
            var item3 = new InventoryItem("Item 3", Tags.Equipment, new double[] { 10, 10 }, new double[] { 50, 50 }, 10);

            var building3 = new InventoryItem("Building 3", Tags.Building, new double[] { 450, 100 }, new double[] { 100, 100 }, 10000);

            building.Children.Add(building2);
            building.Children.Add(item2);
            building2.Children.Add(item1);
            building3.Children.Add(item3);

            ObservableCollection<IInventoryItem> sourceItems = new();
            sourceItems.Add(building);
            sourceItems.Add(building3);

            Inventory.Source = sourceItems;
            
        }
    }
}
