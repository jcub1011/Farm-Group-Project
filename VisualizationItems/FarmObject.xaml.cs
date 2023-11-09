using System;
using System.Collections.Generic;
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
    /// Interaction logic for FarmObject.xaml
    /// </summary>
    public partial class FarmObject : UserControl, IItem
    {
        public FarmObject(string itemName, string itemTag, double[] location, double[]dimensions, double price)
        {
            InitializeComponent();

            ItemName = itemName;
            ItemTag = itemTag;
            Location = location;
            Dimensions = dimensions;
            Price = price;
        }

        #region Interface Implementations
        public string ItemName
        {
            get => ObjectName.Text;
            set => ObjectName.Text = value;
        }

        public string ItemTag { get; set; }

        public double[] Location
        {
            get
            {
                return new double[2] { Canvas.GetLeft(this), Canvas.GetTop(this) };
            }
            set
            {
                if (value.Length != 2) throw new Exception("Array must have a length of 2. Ex { x, y }.");
                Canvas.SetLeft(this, value[0]);
                Canvas.SetTop(this, value[1]);
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
                if (value.Length != 2) throw new Exception("Array must have a length of 2. Ex { width, height }.");
                Width = value[0];
                Height = value[1];
                ObjectStroke.Width = value[0];
                ObjectStroke.Height = value[1];
            }
        }

        public double Price { get; set; }
        #endregion
    }
}
