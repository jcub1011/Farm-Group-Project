using Farm_Group_Project.VisualizationItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Farm_Group_Project
{
    public struct Tags
    {
        public const string Drone = "Drone";
        public const string Building = "Building";
        public const string Crop = "Crop";
        public const string Item = "Item";
        public const string Equipment = "Equipment";
    }

    /// <summary>
    /// Interaction logic for VisualizationControl.xaml
    /// </summary>
    public partial class VisualizationControl : UserControl
    {
        #region Properties
        readonly VirtualDrone drone;
        readonly Building commandCenter;
        #endregion

        public VisualizationControl()
        {
            InitializeComponent();

            // Create drone.
            drone = new(
                "Drone",
                Tags.Drone,
                new double[] { 20, 30 },
                1000
            );

            // Create command center.
            commandCenter = new Building (
                "CommandCenter",
                Tags.Building,
                new double[] { 20, 50 },
                new double[] { 200, 100 },
                10000
            );

            // Add drone to command center.
            commandCenter.AddChild(drone);

            // Add command center to canvas.
            BaseContainer.Children.Add(commandCenter);
        }

        #region Methods
        /// <summary>
        /// Command the drone to move to the given coordinates. Velocity is pixels per second.
        /// </summary>
        /// <param name="coordinates">{ x, y }</param>
        /// <param name="velocity">pixels/s</param>
        public void MoveDrone(double[] coordinates, double velocity)
        {
            drone.MoveTo(coordinates, velocity);
        }

        /// <summary>
        /// Command the drone to stop all move commands.
        /// </summary>
        public void StopDrone()
        {
            drone.CancelMoves();
        }
        #endregion
    }
}
