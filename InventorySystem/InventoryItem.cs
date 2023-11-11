using Farm_Group_Project.VisualizationItems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm_Group_Project.InventorySystem
{
    public interface IInventoryItem: INotifyPropertyChanged
    {
        string ItemName { get; set; }
        string ItemTag { get; set; }
        double[] Location { get; set; }
        double[] Dimensions { get; set; }
        double Price { get; set; }
        ObservableCollection<IInventoryItem>? Children { get; set; }
    }

    public class InventoryItem: IInventoryItem
    {
        string _itemName;
        public string ItemName
        {
            get => _itemName;
            set
            {
                _itemName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemName)));
            }
        }

        string _itemTag;
        public string ItemTag
        {
            get => _itemTag;
            set
            {
                _itemTag = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemTag)));
            }
        }

        double[] _location;
        public double[] Location
        {
            get => _location;
            set
            {
                if (value.Length != 2) throw new ArgumentException("Location must be an array of length 2 of format { x, y }.");
                _location = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Location)));
            }
        }

        double[] _dimensions;
        public double[] Dimensions
        {
            get => _dimensions;
            set
            {
                if (value.Length != 2) throw new ArgumentException("Dimension must be an array of length 2 of format { width, height }.");
                _dimensions = value;
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

        ObservableCollection<IInventoryItem>? _children;
        public ObservableCollection<IInventoryItem>? Children
        {
            get => _children;
            set
            {
                _children = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Children)));
            }
        }

        public InventoryItem(string itemName, string itemTag, double[] location, double[] dims, double price, ObservableCollection<IInventoryItem>? children = null)
        {
            ItemName = itemName;
            ItemTag = itemTag;
            Location = location;
            Dimensions = dims;
            Price = price;
            Children = children ?? new();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
