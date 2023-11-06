using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Farm_Group_Project.VisualizationItems
{
    public class BuildingEntity
    {
        #region Properties
        public string Name { private set; get; }
        public Building DisplayClass { get; set; }
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
                    foreach (var item in e.NewItems)
                    {
                        DisplayClass.Content.Children.Add((UIElement)item);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems)
                    {
                        DisplayClass.Content.Children.Remove((UIElement)item);
                    }
                    break;
                default:
                    throw new ArgumentException($"Usupported action on {nameof(Children)}. Only supports Add and Remove.");
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
