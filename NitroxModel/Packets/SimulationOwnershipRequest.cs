using System;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets
{
    [Serializable]
    public class SimulationOwnershipRequest : Packet
    {
        public ushort PlayerId { get; set; }
        public NitroxId Id { get; set; }
        public SimulationLockType LockType { get; set; }

        public SimulationOwnershipRequest() { }

        public SimulationOwnershipRequest(ushort playerId, NitroxId id, SimulationLockType lockType)
        {
            PlayerId = playerId;
            Id = id;
            LockType = lockType;
        }
    }
}
