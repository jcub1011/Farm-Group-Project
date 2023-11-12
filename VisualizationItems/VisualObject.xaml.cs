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
    /// Interaction logic for VisualObject.xaml
    /// </summary>
    public partial class VisualObject : UserControl
    {
        public double X
        {
            get => Canvas.GetLeft(this);
            set => Canvas.SetLeft(this, value);
        }

        public double Y
        {
            get => Canvas.GetTop(this);
            set => Canvas.SetTop(this, value);
        }

        public double ItemWidth
        {
            get => ContentHolder.Width;
            set => ContentHolder.Width = value;
        }

        public double ItemHeight
        {
            get => ContentHolder.Height;
            set => ContentHolder.Height = value;
        }

        public string ItemTag { get; private set; }

        /// <summary>
        /// Creates a visual object based on an inventory item.
        /// </summary>
        /// <param name="item"></param>
        public VisualObject(IInventoryItem item)
        {
            InitializeComponent();

            DisplayText.Text = item.ItemName;
            ItemTag = item.ItemTag;
            X = item.X;
            Y = item.Y;
            ItemWidth = item.ItemWidth;
            ItemHeight = item.ItemHeight;

            if (item.Children != null)
            {
                foreach (IInventoryItem child in item.Children)
                {
                    ContentHolder.Children.Add(new VisualObject(child));
                }
            }

            // Subscribe Property Changed
            item.PropertyChanged += (_, e) =>
            {
                Debug.WriteLine($"Property changed: {e.PropertyName}");
                switch(e.PropertyName)
                {
                    case nameof(item.ItemName):
                        DisplayText.Text = item.ItemName;
                        break;
                    case nameof(item.X):
                        X = item.X;
                        break;
                    case nameof(item.Y):
                        Y = item.Y;
                        break;
                    case nameof(item.ItemWidth):
                        ItemWidth = item.ItemWidth;
                        break;
                    case nameof(item.ItemHeight):
                        ItemHeight = item.ItemHeight;
                        break;
                    default:
                        break;
                }
            };
        }
    }
}
