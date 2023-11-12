using Farm_Group_Project.InventorySystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public static readonly DependencyProperty ItemNameProperty = DependencyProperty.Register(nameof(ItemName), typeof(string), typeof(VisualObject));
        /// <summary>
        /// Name displayed above the object.
        /// </summary>
        public string ItemName
        {
            get => (string)GetValue(ItemNameProperty);
            set => SetValue(ItemNameProperty, value);
        }

        public VisualObject()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates a visual object based on an inventory item.
        /// </summary>
        /// <param name="item"></param>
        public VisualObject(IInventoryItem item)
        {
            InitializeComponent();

            DisplayText.Text = item.ItemName;

            ContentHolder.Width = item.Dimensions[0];
            ContentHolder.Height = item.Dimensions[1];

            Canvas.SetLeft(this, item.Location[0]);
            Canvas.SetTop(this, item.Location[1]);

            if (item.Children != null)
            {
                foreach (IInventoryItem child in item.Children)
                {
                    ContentHolder.Children.Add(new VisualObject(child));
                }
            }
        }
    }
}
