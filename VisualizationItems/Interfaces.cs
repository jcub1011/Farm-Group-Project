using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Farm_Group_Project.VisualizationItems
{
    public interface IItem
    {
        public string Name { get; set; }
        public int[] Location { get; set; }
        public int[] Dimensions { get; set; }
        public double Price { get; set; }
    }

    public interface IItemContainer: IItem
    {
        public void AddItem(IItem item);
        public void AddItemContainer(IItemContainer container);
        public List<object> Children { get; }
    }
}
