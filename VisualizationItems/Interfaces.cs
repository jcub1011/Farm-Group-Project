using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Farm_Group_Project.VisualizationItems
{
    public interface IItem
    {
        public string Name { get; set; }
        public double[] Location { get; set; }
        public double[] Dimensions { get; set; }
        public double Price { get; set; }
    }

    public interface IItemContainer: IItem
    {
        public void AddChild(IItem item);
        public void AddChild(IItemContainer container);
        public void RemoveChild(IItem item);
        public void RemoveChild(IItemContainer container);
        public System.Collections.IEnumerator Children { get; }
    }
}
