//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Collections.Specialized;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;

//namespace Farm_Group_Project.VisualizationItems
//{
//    public class BuildingEntity : IItemContainer
//    {
//        #region Properties
//        public Building DisplayClass { get; set; }
//        private readonly ObservableCollection<Control> _children;
//        #endregion

//        #region Interface Implementations
//        List<object> IItemContainer.Children
//        {
//            get => _children.ToList<object>();
//        }

//        public string Name
//        {
//            get;
//            set;
//        }
//        public int[] Location
//        {
//            get;
//            set;
//        }
//        public int[] Dimensions
//        {
//            get;
//            set;
//        }
//        public double Price
//        {
//            get;
//            set;
//        }

//        public void AddChild(IItem item)
//        {
//            var child = new Canvas();

//            // Set properties.
//            child.Width = item.Dimensions[0];
//            child.Height = item.Dimensions[1];


//            _children.Add((Control)item);
//        }

//        public void AddChild(IItemContainer container)
//        {
//            _children.Add((Control)container);
//        }

//        public void RemoveChild(IItem item)
//        {
//            _children.Remove((Control)item);
//        }

//        public void RemoveChild(IItemContainer container)
//        {
//            _children.Remove((Control)container);
//        }
//        #endregion

//        #region Methods
//        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
//        {
//            switch (e.Action)
//            {
//                case NotifyCollectionChangedAction.Add:
//                    foreach (var item in e.NewItems)
//                    {
//                        DisplayClass.Content.Children.Add((UIElement)item);
//                    }
//                    break;
//                case NotifyCollectionChangedAction.Remove:
//                    foreach (var item in e.OldItems)
//                    {
//                        DisplayClass.Content.Children.Remove((UIElement)item);
//                    }
//                    break;
//                default:
//                    throw new ArgumentException($"Usupported action on {nameof(_children)}. Only supports Add and Remove.");
//            }
//        }
//        #endregion

//        #region Constuctors
//        public BuildingEntity()
//        {
//            Name = "";
//            _children = new();
//            // _children.CollectionChanged += OnCollectionChanged;
//        }

//        ~BuildingEntity()
//        {
//            //_children.CollectionChanged -= OnCollectionChanged;
//        }
//        #endregion
//    }
//}
