using System;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets
{
    [Serializable]
    public class VehicleDocking : Packet
    {
        public NitroxId VehicleId { get; set; }
        public NitroxId DockId { get; set; }
        public ushort PlayerId { get; set; }

        public VehicleDocking() { }

        public VehicleDocking(NitroxId vehicleId, NitroxId dockId, ushort playerId)
        {
            VehicleId = vehicleId;
            DockId = dockId;
            PlayerId = playerId;
        }
    }
}
