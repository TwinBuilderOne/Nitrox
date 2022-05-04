﻿using System;
using ProtoBufNet;

namespace NitroxModel.DataStructures.GameLogic
{
    [Serializable]
    [ProtoContract]
    public class EquippedItemData : ItemData
    {
        [ProtoMember(1)]
        public string Slot { get; set; }

        [ProtoMember(2)]
        public NitroxTechType TechType { get; set; }

        public EquippedItemData()
        {
            // Constructor for serialization.
        }

        public EquippedItemData(NitroxId containerId, NitroxId itemId, byte[] serializedData, string slot, NitroxTechType techType) : base(containerId, itemId, serializedData)
        {
            Slot = slot;
            TechType = techType;
        }

        public override string ToString()
        {
            return "[EquippedItemData ContainerGuid: " + ContainerId + "Id: " + ItemId + " Slot: " + Slot + " TechType: " + TechType + "]";
        }
    }
}
