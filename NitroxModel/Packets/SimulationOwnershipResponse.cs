using System;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets
{
    [Serializable]
    public class SimulationOwnershipResponse : Packet
    {
        public NitroxId Id { get; set; }
        public bool LockAcquired { get; set; }
        public SimulationLockType LockType { get; set; }

        public SimulationOwnershipResponse() { }

        public SimulationOwnershipResponse(NitroxId id, bool lockAcquired, SimulationLockType lockType)
        {
            Id = id;
            LockAcquired = lockAcquired;
            LockType = lockType;
        }
    }
}
