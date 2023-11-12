using Farm_Group_Project.InventorySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Farm_Group_Project.VisualizationItems
{
    public struct Tags
    {
        public const string Drone = "Drone";
        public const string Building = "Building";
        public const string Crop = "Crop";
        public const string Item = "Item";
        public const string Equipment = "Equipment";
    }

    public interface IItem
    {
        /// <summary>
        /// Display name of the object.
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// The object tag.
        /// </summary>
        public string ItemTag { get; set; }

        /// <summary>
        /// Location of object with respect to the parent container.
        /// Array must have a length of 2 { x, y }.
        /// </summary>
        public double[] Location { get; set; }

        /// <summary>
        /// Width and height of the object.
        /// Array must have a length of 2 where { width, height }.
        /// </summary>
        public double[] Dimensions { get; set; }

        /// <summary>
        /// The price of the object in dollars.
        /// </summary>
        public double Price { get; set; }
    }

    public interface IItemContainer: IItem
    {
        /// <summary>
        /// Adds a child to the canvas of this object.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void AddChild(IItem item);

        /// <summary>
        /// Adds a child to the canvas of this object.
        /// </summary>
        /// <param name="container">The container to add.</param>
        public void AddChild(IItemContainer container);

        /// <summary>
        /// The child to remove from the canvas of this object.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        public void RemoveChild(IItem item);

        /// <summary>
        /// The child to remove from the canvas of this object.
        /// </summary>
        /// <param name="container">The container to remove.</param>
        public void RemoveChild(IItemContainer container);

        /// <summary>
        /// The enumerator for the children of this object's canvas container.
        /// </summary>
        public UIElementCollection ChildObjects { get; }
    }

    public interface IInventoryItemContainer
    {
        public void Add(IInventoryItem item);
        public void Remove(IInventoryItem item);
    }
}
