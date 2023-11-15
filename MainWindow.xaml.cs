﻿using Farm_Group_Project.InventorySystem;
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

            // Code for testing.
            var commandCenter = new InventoryItem("Command Center", Tags.Building, 0, 0, 80, 80, 1000, new());

            var barn = new InventoryItem("Barn", Tags.Building, 0, 100, 500, 100, 10000, new());
            var livestockArea = new InventoryItem("Livestock Area", Tags.Building, 100, 0, 100, 100, 1000, new());
            var milkStorage = new InventoryItem("Milk Storage", Tags.Building, 0, 0, 100, 100, 9800, new());

            barn.Children?.Add(livestockArea);
            barn.Children?.Add(milkStorage);

            var cow = new InventoryItem("Cow", Tags.Livestock, 0, 0, 20, 20, 500);
            livestockArea.Children?.Add(cow);

            var storageBuilding = new InventoryItem("Storage Building", Tags.Building, 0, 250, 100, 100, 9800, new());
            var tractor = new InventoryItem("Tractor", Tags.Equipment, 0, 0, 50, 50, 27000);
            var tiller = new InventoryItem("Tiller", Tags.Equipment, 80, 0, 50, 50, 21000);
            storageBuilding.Children?.Add(tractor);
            storageBuilding.Children?.Add(tiller);

            var soy = new InventoryItem("Soy", Tags.Crop, 250, 300, 100, 50, 2000);

            ObservableCollection<IInventoryItem> sourceItems = new()
            {
                new VirtualDrone("Drone", Tags.Drone, 100, 100, 1000)
            };

            Inventory.Source = sourceItems;
            Visualizer.Source = sourceItems;

            sourceItems.Add(commandCenter);
            sourceItems.Add(barn);
            sourceItems.Add(storageBuilding);
            sourceItems.Add(soy);

            Visualizer.UpdateView();

            ContentRendered += (_, _) =>
            {
                var dest = Visualizer.FindPointForItem(commandCenter);
                Visualizer.Drone?.MoveTo(dest.X, dest.Y + 20, 300);
            };
            
            // End code for testing.
        }
    }
}
