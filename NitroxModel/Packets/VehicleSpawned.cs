using System;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class VehicleSpawned : Packet
    {
        public byte[] SerializedData { get; set; }
        public VehicleModel VehicleModel { get; set; }

        public VehicleSpawned() { }

        public VehicleSpawned(byte[] serializedData, VehicleModel vehicleModel)
        {
            SerializedData = serializedData;
            VehicleModel = vehicleModel;
        }
    }
}
