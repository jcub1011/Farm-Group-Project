using Farm_Group_Project.InventorySystem;
using Microsoft.VisualBasic;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Farm_Group_Project.VisualizationItems
{
    internal struct MoveCommand
    {
        public double TargetX { get; set; }
        public double TargetY { get; set; }
        public double Velocity { get; set; }
    }

    /// <summary>
    /// Interaction logic for VirtualDrone.xaml
    /// </summary>
    public partial class VirtualDrone : UserControl, IInventoryItem, INotifyPropertyChanged
    {
        const double UPDATE_RATE = 60;
        const double SCAN_TIME = 1;

        bool _isMoving;
        bool _cancel;
        Queue<MoveCommand> _movesToComplete;
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raised whenever the postion of the drone is changed.
        /// </summary>
        public event EventHandler UpdateCanvas;

        /// <summary>
        /// This object cannot contain children. It always returns null and this value cannot be set.
        /// </summary>
        public ObservableCollection<IInventoryItem>? Children { get => null; set => throw new NotImplementedException("Drone doesn't support child objects."); }

        public double X
        {
            get => Margin.Left;
            set
            {
                Margin = new System.Windows.Thickness(value, Y, 0, 0);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(X)));
            }
        }

        public double Y
        {
            get => Margin.Top;
            set
            {
                Margin = new System.Windows.Thickness(X, value, 0, 0);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Y)));
            }
        }

        public string ItemName
        {
            get => DroneName.Text;
            set
            {
                DroneName.Text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemName)));
            }
        }

        public string ItemTag { get; set; } = Tags.Drone;

        public double ItemWidth
        {
            get => ActualWidth;
            set => throw new NotImplementedException("Drones can't have their dimensions changed.");
        }

        public double ItemHeight
        {
            get => ActualHeight;
            set => throw new NotImplementedException("Drones can't have their dimensions changed.");
        }

        public double Price { get; set; }

        double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        public VirtualDrone(string itemName, string itemTag, double x, double y, double price)
        {
            InitializeComponent();
            _isMoving = false;
            _cancel = false;
            _movesToComplete = new Queue<MoveCommand>();

            ItemName = itemName;
            ItemTag = itemTag;
            X = x;
            Y = y;
            Price = price;
        }

        public VirtualDrone(IInventoryItem item)
        {
            InitializeComponent();
            _isMoving = false;
            _cancel = false;
            _movesToComplete = new();

            ItemName = item.ItemName;
            ItemTag = item.ItemTag;
            X = item.X;
            Y = item.Y;
            Price = item.Price;
        }

        public void CancelMoves()
        {
            _cancel = true;
            _isMoving = false;
            _movesToComplete.Clear();
        }

        /// <summary>
        /// Moves the drone from its current position to the given coords at the specified velocity (pixels per second).
        /// Doesn't complete until all the previous move operations have finished.
        /// Not thread safe.
        /// </summary>
        /// <param name="coords">Target position.</param>
        /// <param name="velocity">Pixels per second.</param>
        public async void MoveTo(double newX, double newY, double velocity)
        {
            // Checks if the drone is busy and queues the command if it is.
            if (_isMoving)
            {
                _movesToComplete.Enqueue(new MoveCommand()
                {
                    TargetX = newX,
                    TargetY = newY,
                    Velocity = velocity
                });
                return;
            }
            _isMoving = true;

            // Calcuate how many pixels to move each update.
            double seconds = GetDistance(X, Y, newX, newY) / velocity;

            long fullTicksToComplete = (long)(seconds * UPDATE_RATE);
            double ticksToComplete = (long)(seconds * UPDATE_RATE);

            double deltaX = (newX - X) / ticksToComplete;
            double deltaY = (newY - Y) / ticksToComplete;

            while (fullTicksToComplete > 0)
            {
                if (_cancel)
                {
                    _cancel = false;
                    _isMoving = false;
                    return;
                }

                X += deltaX;
                Y += deltaY;
                await Task.Delay((int)(1 / UPDATE_RATE * 1000));
                fullTicksToComplete--;
            }

            // Simulated scan time.
            await Task.Delay((int)(SCAN_TIME * 1000));

            _isMoving = false;
            X = newX;
            Y = newY;

            if (_movesToComplete.Count > 0)
            {
                var next = _movesToComplete.Dequeue();
                MoveTo(next.TargetX, next.TargetY, next.Velocity);
            }
        }
    }
}
