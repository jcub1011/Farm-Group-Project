using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Farm_Group_Project.VisualizationItems
{
    public class BuildingEntity
    {
        #region Properties
        public string Name { private set; get; }
        private readonly ObservableCollection<Control> _children;
        public ObservableCollection<Control> Children { 
            get
            {
                return _children;
            } 
        }
        #endregion

        #region Methods
        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Constuctors
        public BuildingEntity()
        {
            Name = "";
            _children = new();
            _children.CollectionChanged += OnCollectionChanged;
        }

        ~BuildingEntity()
        {
            _children.CollectionChanged -= OnCollectionChanged;
        }
        #endregion
    }
}
