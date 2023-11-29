using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace Farm_Group_Project.DroneInterface
{
    public struct DroneCommands
    {
        public const string BeginWateringMessage = "BeginWatering";
        public const string StopWateringMessage = "StopWatering";
    }

    public static class DroneCommandHandler
    {
        static SerialPort? _dronePort;

        public static void InitDronePort(string portName)
        {
            _dronePort = new SerialPort
            {
                PortName = portName,
                BaudRate = 9600
            };
        }

        public static void OpenDronePort()
        {
            _dronePort?.Open();
        }

        public static void CloseDronePort()
        {
            _dronePort?.Close();
        }

        public static void SendMessage(string message)
        {
            _dronePort?.Write(message);
        }

        public static bool IsDronePortInitialized()
        {
            return _dronePort != null && _dronePort.IsOpen;
        }
    }
}
