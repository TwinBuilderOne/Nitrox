using System;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class PlayerEquipmentAdded : Packet
    {
        public NitroxTechType TechType { get; set; }
        public EquippedItemData EquippedItem { get; set; }

        public PlayerEquipmentAdded() { }

        public PlayerEquipmentAdded(NitroxTechType techType, EquippedItemData equippedItem)
        {
            TechType = techType;
            EquippedItem = equippedItem;
        }
    }
}
