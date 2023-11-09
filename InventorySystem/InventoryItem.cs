using Farm_Group_Project.VisualizationItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm_Group_Project.InventorySystem
{
    public class InventoryItem
    {
        public string ItemName { get; set; }
        public string ItemTag { get; set; }
        public double[] Location { get; set; }
        public double[] Dimensions { get; set; }
        public double Price { get; set; }
        public List<InventoryItem> Children { get; set; }

        public InventoryItem(string itemName, string itemTag, double[] location, double[] dims, double price, List<InventoryItem>? children = null)
        {
            ItemName = itemName;
            ItemTag = itemTag;
            Location = location;
            Dimensions = dims;
            Price = price;
            Children = children ?? new List<InventoryItem>();
        }
    }
}
