using System;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets
{
    [Serializable]
    public class VehicleOnPilotModeChanged : Packet
    {
        public NitroxId VehicleId { get; set; }
        public ushort PlayerId { get; set; }
        public bool IsPiloting { get; set; }

        public VehicleOnPilotModeChanged() { }

        public VehicleOnPilotModeChanged(NitroxId vehicleId, ushort playerId, bool isPiloting)
        {
            VehicleId = vehicleId;
            PlayerId = playerId;
            IsPiloting = isPiloting;
        }
    }
}
