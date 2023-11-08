using Farm_Group_Project.VisualizationItems;
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

namespace Farm_Group_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var testChild = new Building();
            testChild.ItemName = "Test Building";
            testChild.Dimensions = new double[2] { 200, 200 };
            testChild.Location = new double[2] { 100, 100 };

            var innerTestChild = new Building();
            innerTestChild.ItemName = "Inner Test Building";
            innerTestChild.Dimensions = new double[2] { 100, 100 };
            innerTestChild.Location = new double[2] { 10, 10 };

            testChild.AddChild(innerTestChild);

            TestContainer.Children.Add(testChild);
        }
    }
}
