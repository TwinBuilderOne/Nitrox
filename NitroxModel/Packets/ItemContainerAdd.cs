using System;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class ItemContainerAdd : Packet
    {
        public ItemData ItemData { get; set; }

        public ItemContainerAdd() { }

        public ItemContainerAdd(ItemData itemData)
        {
            ItemData = itemData;
        }
    }
}
