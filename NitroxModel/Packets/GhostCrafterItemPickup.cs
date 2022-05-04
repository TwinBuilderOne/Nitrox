﻿using System;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class GhostCrafterItemPickup : Packet
    {
        public NitroxId GhostCrafterId { get; set; }
        public NitroxTechType TechType { get; set; }

        public GhostCrafterItemPickup() { }

        public GhostCrafterItemPickup(NitroxId ghostCrafterId, NitroxTechType techType)
        {
            GhostCrafterId = ghostCrafterId;
            TechType = techType;
        }
    }
}
