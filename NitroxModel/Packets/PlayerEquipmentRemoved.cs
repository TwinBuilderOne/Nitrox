using System;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class PlayerEquipmentRemoved : Packet
    {
        public NitroxTechType TechType { get; set; }
        public NitroxId EquippedItemId { get; set; }

        public PlayerEquipmentRemoved() { }

        public PlayerEquipmentRemoved(NitroxTechType techType, NitroxId equippeditemId)
        {
            TechType = techType;
            EquippedItemId = equippeditemId;
        }
    }
}
