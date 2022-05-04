using System;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class ModuleAdded : Packet
    {
        public EquippedItemData EquippedItemData { get; set; }
        public bool PlayerModule { get; set; }

        public ModuleAdded() { }

        public ModuleAdded(EquippedItemData equippedItemData, bool playerModule)
        {
            EquippedItemData = equippedItemData;
            PlayerModule = playerModule;
        }
    }
}
