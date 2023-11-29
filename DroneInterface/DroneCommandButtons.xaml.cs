using Farm_Group_Project.InventorySystem;
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

namespace Farm_Group_Project.DroneInterface
{
    /// <summary>
    /// Interaction logic for DroneCommandButtons.xaml
    /// </summary>
    public partial class DroneCommandButtons : UserControl
    {
        public DroneCommandButtons()
        {
            InitializeComponent();
        }

        public delegate void ButtonClicked();
        public event ButtonClicked OnVisitItemClicked;
        public event ButtonClicked OnScanFarmClicked;
        public event ButtonClicked OnWaterItemClicked;

        private void OnVisitItem(object sender, RoutedEventArgs e)
        {
            OnVisitItemClicked?.Invoke();
        }

        private void OnScanFarm(object sender, RoutedEventArgs e)
        {
            OnScanFarmClicked?.Invoke();
        }

        private void OnWaterSelectedArea(object sender, RoutedEventArgs e)
        {
            OnWaterItemClicked?.Invoke();
        }
    }
}
