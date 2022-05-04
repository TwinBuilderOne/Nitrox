using System;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class VehicleCreated : Packet
    {
        public string PlayerName { get; set; }
        public VehicleModel CreatedVehicle { get; set; }

        public VehicleCreated() { }

        public VehicleCreated(VehicleModel createdVehicle, string playerName)
        {
            CreatedVehicle = createdVehicle;
            PlayerName = playerName;
        }
    }
}
