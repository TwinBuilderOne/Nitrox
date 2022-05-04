using System;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets
{
    [Serializable]
    public class WeldAction : Packet
    {
        public NitroxId Id { get; set; }
        public float HealthAdded { get; set; }

        public WeldAction() { }

        public WeldAction(NitroxId id, float healthAdded)
        {
            Id = id;
            HealthAdded = healthAdded;
        }
    }
}
