using Farm_Group_Project.VisualizationItems;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

            /* Test code I used to ensure object functionality.
            var testChild = new Building();
            testChild.ItemName = "Test Building";
            testChild.Dimensions = new double[2] { 200, 200 };
            testChild.Location = new double[2] { 100, 100 };

            var innerTestChild = new Building();
            innerTestChild.ItemName = "Inner Test Building";
            innerTestChild.Dimensions = new double[2] { 100, 100 };
            innerTestChild.Location = new double[2] { 10, 100 };

            var item = new FarmObject();
            item.ItemName = "Test object";
            item.Location = new double[2] { 15, 30 };

            var otherItem = new FarmObject();
            otherItem.ItemName = "Other test object";
            otherItem.Location = new double[2] { 15, 30 };

            Debug.WriteLine($"Current Directory: \n {AppContext.BaseDirectory}");
            ResourceManager.SetResourceDirectory();

            testChild.AddChild(innerTestChild);
            innerTestChild.AddChild(item);

            TestContainer.Children.Add(testChild);
            testChild.AddChild(otherItem);

            foreach(var child in testChild.ChildObjects)
            {
                Debug.WriteLine(child.GetType());
            }
            */
        }
    }
}
