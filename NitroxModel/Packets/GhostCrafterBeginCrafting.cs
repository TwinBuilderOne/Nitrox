using System;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class GhostCrafterBeginCrafting : Packet
    {
        public NitroxId GhostCrafterId { get; set; }
        public NitroxTechType TechType { get; set; }
        public float Duration { get; set; }

        public GhostCrafterBeginCrafting() { }

        public GhostCrafterBeginCrafting(NitroxId ghostCrafterId, NitroxTechType techType, float duration)
        {
            GhostCrafterId = ghostCrafterId;
            TechType = techType;
            Duration = duration;
        }
    }
}
