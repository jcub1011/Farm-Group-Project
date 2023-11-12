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
        double X { get; set; }
        double Y { get; set; }
        double ItemWidth { get; set; }
        double ItemHeight { get; set; }
        double Price { get; set; }

        /// <summary>
        /// When null, object cannot carry children.
        /// </summary>
        ObservableCollection<IInventoryItem>? Children { get; }
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

        double _x;
        public double X
        {
            get => _x;
            set
            {
                _x = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(X)));
            }
        }

        double _y;
        public double Y
        {
            get => _y;
            set
            {
                _y = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Y)));
            }
        }

        double _itemWidth;
        public double ItemWidth
        {
            get => _itemWidth;
            set
            {
                _itemWidth = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemWidth)));
            }
        }

        double _itemHeight;
        public double ItemHeight
        {
            get => _itemHeight;
            set
            {
                _itemHeight = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemHeight)));
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
            private set
            {
                _children = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Children)));
            }
        }

        /// <summary>
        /// Creates a new inventory item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <param name="itemTag">Tag of the item.</param>
        /// <param name="x">X postion.</param>
        /// <param name="y">Y position.</param>
        /// <param name="width">Width of the item.</param>
        /// <param name="height">Height of the item.</param>
        /// <param name="price">Price of the item.</param>
        /// <param name="children">Children of the item.</param>
        public InventoryItem(string itemName, string itemTag, double x, double y, double width, double height, double price, ObservableCollection<IInventoryItem>? children = null)
        {
            ItemName = itemName;
            ItemTag = itemTag;
            X = x;
            Y = y;
            ItemWidth = width;
            ItemHeight = height;
            Price = price;
            if (TagEvaluator.IsChildCarryingTag(itemTag)) Children = children ?? new ObservableCollection<IInventoryItem>();
            else Children = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
