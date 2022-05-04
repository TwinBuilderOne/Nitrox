using System;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets
{
    [Serializable]
    public class VehicleDestroyed : Packet
    {
        public NitroxId Id { get; set; }
        public string PlayerName { get; set; }
        public bool GetPilotingMode { get; set; }

        public VehicleDestroyed() { }

        public VehicleDestroyed(NitroxId id, string playerName, bool getPilotingMode)
        {
            Id = id;
            PlayerName = playerName;
            GetPilotingMode = getPilotingMode;
        }
    }
}
