using System;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class StorageSlotItemAdd : Packet
    {
        public ItemData ItemData { get; set; }

        public StorageSlotItemAdd() { }

        public StorageSlotItemAdd(ItemData itemData)
        {
            ItemData = itemData;
        }
    }
}
