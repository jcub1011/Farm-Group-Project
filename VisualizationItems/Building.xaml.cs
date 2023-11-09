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
    /// Interaction logic for Building.xaml
    /// </summary>
    public partial class Building : UserControl, IItemContainer
    {
        public Building()
        {
            InitializeComponent();
        }

        public UIElementCollection ChildObjects => Content.Children;

        public string ItemName
        {
            get => Title.Text;
            set { Title.Text = value; }
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
                Width = value[0];
                Height = value[1];
                Title.Width = value[0];
                Content.Width = value[0];
                Content.Height = value[1] - Title.Height;
            }
        }

        public double Price { get; set; }

        public void AddChild(IItem item)
        {
            Content.Children.Add((UIElement) item);
        }

        public void AddChild(IItemContainer container)
        {
            Content.Children.Add((UIElement)container);
        }

        public void RemoveChild(IItem item)
        {
            Content.Children.Remove((UIElement)item);
        }

        public void RemoveChild(IItemContainer container)
        {
            Content.Children.Remove((UIElement)container);
        }
    }
}
