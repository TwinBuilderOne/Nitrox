using System;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets
{
    [Serializable]
    public class ItemContainerRemove : Packet
    {
        public NitroxId OwnerId { get; set; }
        public NitroxId ItemId { get; set; }

        public ItemContainerRemove() { }

        public ItemContainerRemove(NitroxId ownerId, NitroxId itemId)
        {
            OwnerId = ownerId;
            ItemId = itemId;
        }
    }
}
