using Microsoft.VisualBasic;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Farm_Group_Project.VisualizationItems
{
    internal struct MoveCommand
    {
        public double[] Coordinates { get; set; }
        public double Velocity { get; set; }
    }

    /// <summary>
    /// Interaction logic for VirtualDrone.xaml
    /// </summary>
    public partial class VirtualDrone : UserControl, IItem
    {
        const double UPDATE_RATE = 60;
        bool _isMoving;
        bool _cancel;
        Queue<MoveCommand> _movesToComplete;

        /// <summary>
        /// Location of object with respect to the parent container. Instantly transports the drone. 
        /// Use MoveTo to ease drone to a different location.
        /// Array must have a length of 2 { x, y }.
        /// </summary>
        public double[] Location
        {
            get
            {
                return new double[2] { Canvas.GetLeft(this), Canvas.GetTop(this) };
            }
            set
            {
                if (value.Length != 2) throw new Exception("Array must have a length of 2. Ex { x, y }.");
                Canvas.SetLeft(this, value[0]);
                Canvas.SetTop(this, value[1]);
            }
        }

        public string ItemName
        {
            get => DroneName.Text;
            set => DroneName.Text = value;
        }

        public string ItemTag { get; set; } = Tags.Drone;

        public double[] Dimensions
        {
            get
            {
                return new double[] { Width, Height };
            }
            set
            {
                throw new Exception("Drones can't have their dimensions edited.");
            }
        }

        public double Price { get; set; }

        double GetDistance(double[] coords1, double[] coords2)
        {
            return Math.Sqrt(Math.Pow(coords2[0] - coords1[0], 2) + Math.Pow(coords2[1] - coords1[1], 2));
        }

        public VirtualDrone(string itemName, string itemTag, double[] location, double price)
        {
            InitializeComponent();
            _isMoving = false;
            _cancel = false;
            _movesToComplete = new Queue<MoveCommand>();

            ItemName = itemName;
            ItemTag = itemTag;
            Location = location;
            Price = price;
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
        public async void MoveTo(double[] coords, double velocity)
        {
            // Checks if the drone is busy and queues the command if it is.
            if (_isMoving)
            {
                _movesToComplete.Enqueue(new MoveCommand()
                {
                    Coordinates = coords,
                    Velocity = velocity
                });
                return;
            }
            _isMoving = true;

            // Calcuate how many pixels to move each update.
            double seconds = GetDistance(Location, coords) / velocity;

            long fullTicksToComplete = (long)(seconds * UPDATE_RATE);
            double ticksToComplete = (long)(seconds * UPDATE_RATE);

            double deltaX = (coords[0] - Location[0]) / ticksToComplete;
            double deltaY = (coords[1] - Location[1]) / ticksToComplete;

            while (fullTicksToComplete > 0)
            {
                if (_cancel)
                {
                    _cancel = false;
                    _isMoving = false;
                    return;
                }

                Location = new double[2] { Location[0] + deltaX, Location[1] + deltaY };
                await Task.Delay((int)(1 / UPDATE_RATE * 1000));
                fullTicksToComplete--;
            }

            _isMoving = false;
            Location = coords;

            if (_movesToComplete.Count > 0)
            {
                var next = _movesToComplete.Dequeue();
                MoveTo(next.Coordinates, next.Velocity);
            }
        }
    }
}
