using Farm_Group_Project.VisualizationItems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
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
    /// <summary>
    /// Interaction logic for InventoryView.xaml
    /// </summary>
    public partial class InventoryView : UserControl, IDisposable
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof(Source), typeof(ObservableCollection<IInventoryItem>), typeof(VisualObject));
        private bool disposedValue;
        public IInventoryItem? SelectedItem
        {
            get
            {
                return (IInventoryItem)ContentContainer.SelectedItem;
            }
        }

        private bool RemoveIsEnabled
        {
            get => RemoveButton.IsEnabled;
            set => RemoveButton.IsEnabled = value;
        }

        public ObservableCollection<IInventoryItem> Source
        {
            get => (ObservableCollection<IInventoryItem>)GetValue(SourceProperty);
            set
            {
                if (Source != null) Source.CollectionChanged -= OnCollectionChange;
                value.CollectionChanged += OnCollectionChange;
                foreach (var item in value)
                {
                    OnCollectionChange(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
                }
                SetValue(SourceProperty, value);
            }

        }

        public InventoryView()
        {
            InitializeComponent();

            if (ContentContainer.SelectedItem != null && ContentContainer.SelectedItem.GetType() == typeof(VirtualDrone))
            {
                PropertyShower.DisablePropertyModification = true;
            }

            PropertyShower.ItemToModify = (InventoryItem)ContentContainer.SelectedItem;
            ContentContainer.SelectedItemChanged += (_, _) =>
            {
                if (ContentContainer.SelectedItem.GetType() == typeof(VirtualDrone))
                {
                    PropertyShower.DisablePropertyModification = true;
                    var drone = (VirtualDrone)ContentContainer.SelectedItem;
                    var displayValues = new InventoryItem(drone.ItemName, drone.ItemTag, drone.X, drone.Y, drone.Width, drone.Height, drone.Price, drone.Price);
                    PropertyShower.ItemToModify = displayValues;
                    RemoveIsEnabled = false;
                    ItemMaker.CanAddChild = false;
                }
                else
                {
                    RemoveIsEnabled = true;
                    ItemMaker.CanAddChild = TagEvaluator.IsChildCarryingTag(((IInventoryItem)ContentContainer.SelectedItem).ItemTag);
                    PropertyShower.DisablePropertyModification = false;
                    PropertyShower.ItemToModify = (InventoryItem)ContentContainer.SelectedItem;
                }
                Debug.WriteLine("New item selected.");
            };

            Source ??= new();

            // Subscribe to ItemGenerator.
            ItemMaker.OnItemCreatedChild += (e) =>
            {
                if (ContentContainer.SelectedItem != null)
                {
                    var item = (IInventoryItem)ContentContainer.SelectedItem;
                    Debug.WriteLine(item.Children.Count);
                    item.Children.Add(e);
                    Debug.WriteLine("Child added.");
                    Debug.WriteLine(item.Children.Count);
                }
            };
            ItemMaker.OnItemCreatedRoot += (e) =>
            {
                Source.Add(e);
            };
        }

        ~InventoryView()
        {
            Dispose();
        }

        // Handles the removal or addition of items in the tree.
        private void OnCollectionChange(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach(var item in e.NewItems)
                    {
                        ContentContainer.Items.Add(item);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems)
                    {
                        ContentContainer.Items.Remove(item);
                    }
                    break;
                default:
                    break;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ContentContainer.Items.Clear();
                }

                if (Source != null) Source.CollectionChanged -= OnCollectionChange;
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private void OnRemove(object sender, RoutedEventArgs e)
        {
            if (ContentContainer.SelectedItem == null) return;
            if (((IInventoryItem)ContentContainer.SelectedItem).ItemTag == Tags.Drone) return;

            var result = FindParentContainer((IInventoryItem)ContentContainer.SelectedItem, Source);
            if (result == null) return;
            result.Remove((IInventoryItem)ContentContainer.SelectedItem);
            Debug.WriteLine($"Removed {((IInventoryItem)ContentContainer.SelectedItem).ItemName}.");
        }

        /// <summary>
        /// Find target using dfs.
        /// </summary>
        /// <param name="target">Item to find.</param>
        /// <param name="source">Collection to search in.</param>
        /// <returns>Collection containing target or null if not found.</returns>
        private static ObservableCollection<IInventoryItem>? FindParentContainer(IInventoryItem target, ObservableCollection<IInventoryItem> source)
        {
            foreach(var item in source)
            {
                if (ReferenceEquals(item, target)) return source;
                else if (item.Children != null)
                {
                    var result = FindParentContainer(target, item.Children);
                    if (result != null) return result;
                }
            }

            return null;
        }

        private void OnRightClick(object sender, MouseButtonEventArgs e)
        {
            if (ContentContainer.SelectedItem == null || ((IInventoryItem)ContentContainer.SelectedItem).ItemTag == Tags.Drone) return;
            ItemMaker.SetGeneratorValues((IInventoryItem)ContentContainer.SelectedItem);
        }
    }
}
