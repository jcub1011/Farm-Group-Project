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

namespace Farm_Group_Project.InventorySystem
{
    /// <summary>
    /// Interaction logic for ItemGenerator.xaml
    /// </summary>
    public partial class ItemGenerator : UserControl
    {
        public ItemGenerator()
        {
            InitializeComponent();
            ItemPropertyViewer.ItemToModify = new("default", "default", 0, 0, 0, 0, 0);
        }

        public void SetGeneratorValues(IInventoryItem item)
        {
            var temp = new InventoryItem(
                item.ItemName,
                item.ItemTag,
                item.X,
                item.Y,
                item.ItemWidth,
                item.ItemHeight,
                item.Price);
            ItemPropertyViewer.ItemToModify = temp;
        }

        public delegate void ItemCreated(IInventoryItem item);
        public event ItemCreated OnItemCreatedChild;
        public event ItemCreated OnItemCreatedRoot;

        public bool CanAddChild
        {
            get => AddChildButton.IsEnabled;
            set => AddChildButton.IsEnabled = value;
        }

        private void OnAddChild(object sender, RoutedEventArgs e)
        {
            OnItemCreatedChild?.Invoke(new InventoryItem(ItemPropertyViewer.ItemToModify.ItemName,
                ItemPropertyViewer.ItemToModify.ItemTag,
                ItemPropertyViewer.ItemToModify.X,
                ItemPropertyViewer.ItemToModify.Y,
                ItemPropertyViewer.ItemToModify.ItemWidth,
                ItemPropertyViewer.ItemToModify.ItemHeight,
                ItemPropertyViewer.ItemToModify.Price));
        }

        private void OnAddRoot(object sender, RoutedEventArgs e)
        {
            OnItemCreatedRoot?.Invoke(new InventoryItem(ItemPropertyViewer.ItemToModify.ItemName,
                ItemPropertyViewer.ItemToModify.ItemTag,
                ItemPropertyViewer.ItemToModify.X,
                ItemPropertyViewer.ItemToModify.Y,
                ItemPropertyViewer.ItemToModify.ItemWidth,
                ItemPropertyViewer.ItemToModify.ItemHeight,
                ItemPropertyViewer.ItemToModify.Price));
        }
    }
}
