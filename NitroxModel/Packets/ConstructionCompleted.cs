using System;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets
{
    [Serializable]
    public class ConstructionCompleted : Packet
    {
        public NitroxId PieceId { get; set; }
        public NitroxId BaseId { get; set; }

        public ConstructionCompleted() { }

        public ConstructionCompleted(NitroxId id, NitroxId baseId)
        {
            PieceId = id;
            BaseId = baseId;
        }
    }
}
