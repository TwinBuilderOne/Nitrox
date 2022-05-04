using System;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets
{
    [Serializable]
    public class ConstructionAmountChanged : Packet
    {
        public NitroxId Id { get; set; }
        public float ConstructionAmount { get; set; }

        public ConstructionAmountChanged() { }

        public ConstructionAmountChanged(NitroxId id, float constructionAmount)
        {
            Id = id;
            ConstructionAmount = constructionAmount;
        }
    }
}
