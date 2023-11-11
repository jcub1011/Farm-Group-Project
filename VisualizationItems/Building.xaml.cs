using Farm_Group_Project.InventorySystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Farm_Group_Project.VisualizationItems
{
    /// <summary>
    /// Interaction logic for Building.xaml
    /// </summary>
    public partial class Building : UserControl, IInventoryItem, IInventoryItemContainer
    {
        private readonly Dictionary<IInventoryItem, UIElement> inventoryItemToElementMap = new();

        public Building(string itemName, string itemTag, double[] location, double[] dimensions, double price)
        {
            InitializeComponent();

            ItemName = itemName;
            ItemTag = itemTag;
            Location = location;
            Dimensions = dimensions;
            Price = price;
        }

        public static Building GenerateObject(IInventoryItem item)
        {
            var building = new Building(item.ItemName, item.ItemTag, item.Location, item.Dimensions, item.Price)
            {
                Children = item.Children
            };
            return building;
        }

        #region Interface Implementations
        public ObservableCollection<IInventoryItem>? Children
        {
            get
            {
                return (ObservableCollection<IInventoryItem>)Content.Children.Cast<IInventoryItem>();
            }
            set
            {
                throw new NotImplementedException($"{nameof(Building)}.{nameof(Children)} doens't support being reassigned.");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string ItemName
        {
            get => Title.Text;
            set { Title.Text = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemName))); }
        }

        string _itemName;
        public string ItemTag
        {
            get => _itemName;
            set
            {
                _itemName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemTag)));
            }
        }

        public double[] Location
        {
            get
            {
                return new double[2] { Canvas.GetLeft(this), Canvas.GetTop(this) };
            }
            set
            {
                Canvas.SetLeft(this, value[0]);
                Canvas.SetTop(this, value[1]);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Location)));
            }
        }

        public double[] Dimensions
        {
            get
            {
                return new double[2] { Width, Height };
            }
            set
            {
                Width = value[0];
                Height = value[1];
                Title.Width = value[0];
                Content.Width = value[0];
                Content.Height = value[1] - Title.Height;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Dimensions)));
            }
        }

        double _price;
        public double Price
        {
            get => _price;
            set
            {
                _price = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Price)));
            }
        }

        /*
        public void AddChild(IInventoryItem item)
        {
            if (item.Children == null)
            {
                // Make object.
                inventoryItemToElementMap.Add(item, FarmObject.GenerateObject(item));
                Content.Children.Add(inventoryItemToElementMap[item]);
            }
            else
            {
                // Make building.
                inventoryItemToElementMap.Add(item, GenerateObject(item));
                Content.Children.Add(inventoryItemToElementMap[item]);
            }
        }

        public void RemoveChild(IInventoryItem item)
        {
            if (inventoryItemToElementMap.ContainsKey(item)) Content.Children.Remove(inventoryItemToElementMap[item]);
        }*/

        public void Add(IInventoryItem item)
        {
            switch (item)
            {
                case Building:
                    Content.Children.Add((Building)item);
                    break;
                case FarmObject:
                    Content.Children.Add((FarmObject)item);
                    break;
                default:
                    throw new ArgumentException($"Type {item.GetType()} is unsupported.");
            }
        }

        public void Remove(IInventoryItem item)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
