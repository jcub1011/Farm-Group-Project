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
    public partial class VisualObject : UserControl, IInventoryItemContainer
    {
        public static readonly DependencyProperty ItemNameProperty = DependencyProperty.Register(nameof(ItemName), typeof(string), typeof(VisualObject));
        public string ItemName
        {
            get => (string)GetValue(ItemNameProperty);
            set => SetValue(ItemNameProperty, value);
        }
        /*
        public event PropertyChangedEventHandler? PropertyChanged;

        public static readonly DependencyProperty ChildrenProperty = DependencyProperty.Register(nameof(Children), typeof(ObservableCollection<IInventoryItem>), typeof(VisualObject));
        public ObservableCollection<IInventoryItem>? Children
        {
            get => (ObservableCollection<IInventoryItem>)GetValue(ChildrenProperty);
            set => SetValue(ChildrenProperty, value);
        }

        public static readonly DependencyProperty ItemTagProperty = DependencyProperty.Register(nameof(ItemTag), typeof(string), typeof(VisualObject));
        public string ItemTag { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public static readonly DependencyProperty XProperty = DependencyProperty.Register(nameof(X), typeof(double), typeof(VisualObject));
        public static readonly DependencyProperty YProperty = DependencyProperty.Register(nameof(Y), typeof(double), typeof(VisualObject));
        public double X
        {
            get => (double)GetValue(XProperty);
            set 
            { 
                SetValue(XProperty, value);
                Canvas.SetLeft(this, value);
            }
        }
        public double Y
        {
            get => (double)GetValue(YProperty);
            set 
            { 
                SetValue(YProperty, value);
                Canvas.SetTop(this, value);
            }
        }
        public double[] Location
        {
            get => new double[2] { X, Y };
            set
            {
                X = value[0];
                Y = value[1];
            }
        }

        public static readonly DependencyProperty ObjectWidthProperty = DependencyProperty.Register(nameof(ObjectWidth), typeof(double), typeof(VisualObject));
        public static readonly DependencyProperty ObjectHeightProperty = DependencyProperty.Register(nameof(ObjectHeight), typeof(double), typeof(VisualObject));
        public double ObjectWidth
        {
            get => (double)GetValue(ObjectWidthProperty);
            set
            {
                SetValue(ObjectWidthProperty, value);
                Width = value;
            }
        }
        public double ObjectHeight
        {
            get => (double)GetValue(ObjectHeightProperty);
            set
            {
                SetValue(ObjectHeightProperty, value);
                Canvas.SetTop(this, value);
            }
        }
        public double[] Dimensions
        {
            get => new double[2] { Width, Height };
            set
            {
                ObjectWidth = value[0];
                ObjectHeight = value[1];
            }
        }

        public static readonly DependencyProperty PriceProperty = DependencyProperty.Register(nameof(Price), typeof(double), typeof(VisualObject));
        public double Price { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        */

        public void Add(IInventoryItem item)
        {
            switch(item)
            {
                case Building:
                    ContentHolder.Children.Add((Building)item);
                    break;
                case FarmObject:
                    ContentHolder.Children.Add((FarmObject)item);
                    break;
                default:
                    throw new ArgumentException($"Type {item.GetType()} is unsupported.");
            }
        }

        public void Remove(IInventoryItem item)
        {
            switch (item)
            {
                case Building:
                    ContentHolder.Children.Remove((Building)item);
                    break;
                case FarmObject:
                    ContentHolder.Children.Remove((FarmObject)item);
                    break;
                default:
                    throw new ArgumentException($"Type {item.GetType()} is unsupported.");
            }
        }

        public VisualObject()
        {
            InitializeComponent();
        }

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
