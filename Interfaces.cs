using Farm_Group_Project.InventorySystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

    public static class TagEvaluator
    {
        public static bool IsChildCarryingTag(string tag)
        {
            // By default tags return false. If tag should return true then add a new case.
            return tag switch
            {
                Tags.Building => true,
                _ => false,
            };
        }

        public static ObservableCollection<string> GetTagList()
        {
            var tags = typeof(Tags).GetFields();
            var list = new ObservableCollection<string>();
            var temp = new Tags();

            foreach ( var tag in tags)
            {
                if (tag.GetValue(temp) is not string result) continue;
                list.Add(result);
            }

            return list;
        }
    }
}
