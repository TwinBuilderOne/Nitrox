using System;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets
{
    [Serializable]
    public class VehicleUndocking : Packet
    {
        public NitroxId VehicleId { get; set; }
        public NitroxId DockId { get; set; }
        public ushort PlayerId { get; set; }
        public bool UndockingStart { get; set; }

        public VehicleUndocking() { }

        public VehicleUndocking(NitroxId vehicleId, NitroxId dockId, ushort playerId, bool undockingStart)
        {
            VehicleId = vehicleId;
            DockId = dockId;
            PlayerId = playerId;
            UndockingStart = undockingStart;
        }
    }
}
