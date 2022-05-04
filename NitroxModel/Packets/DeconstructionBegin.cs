using System;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets
{
    [Serializable]
    public class DeconstructionBegin : Packet
    {
        public NitroxId Id { get; set; }

        public DeconstructionBegin() { }

        public DeconstructionBegin(NitroxId id)
        {
            Id = id;
        }
    }
}
