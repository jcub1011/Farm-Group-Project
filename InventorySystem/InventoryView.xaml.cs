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

        public ObservableCollection<IInventoryItem> Source
        {
            get => (ObservableCollection<IInventoryItem>)GetValue(SourceProperty);
            set
            {
                if (Source != null) Source.CollectionChanged -= OnCollectionChange;
                value.CollectionChanged += OnCollectionChange;
                SetValue(SourceProperty, value);
            }
        }

        public InventoryView()
        {
            InitializeComponent();

            PropertyShower.ItemToModify = (InventoryItem)ContentContainer.SelectedItem;
            ContentContainer.SelectedItemChanged += (_, _) =>
            {
                PropertyShower.ItemToModify = (InventoryItem)ContentContainer.SelectedItem;
                Debug.WriteLine("New item selected.");
            };

            Source ??= new();

            /*
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

            ContentContainer.Items.Add(building);
            ContentContainer.Items.Add(building3);*/
        }

        ~InventoryView()
        {
            Dispose();
        }

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
    }
}
