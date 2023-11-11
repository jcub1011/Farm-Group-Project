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

            PropertyShower.ItemToModify = (InventoryItem)ContentContainer.SelectedItem;
            ContentContainer.SelectedItemChanged += (_, _) =>
            {
                PropertyShower.ItemToModify = (InventoryItem)ContentContainer.SelectedItem;
                Debug.WriteLine("New item selected.");
            };

            Source ??= new();
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
