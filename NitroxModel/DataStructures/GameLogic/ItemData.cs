using System;
using ProtoBufNet;

namespace NitroxModel.DataStructures.GameLogic
{
    [Serializable]
    [ProtoContract]
    [ProtoInclude(50, typeof(EquippedItemData))]
    [ProtoInclude(51, typeof(PlantableItemData))]
    public class ItemData
    {
        [ProtoMember(1)]
        public NitroxId ContainerId { get; set; }

        [ProtoMember(2)]
        public NitroxId ItemId { get; set; }

        [ProtoMember(3)]
        public byte[] SerializedData { get; set; }

        public ItemData()
        {
            // Constructor for serialization.
        }

        public ItemData(NitroxId containerId, NitroxId itemId, byte[] serializedData)
        {
            ContainerId = containerId;
            ItemId = itemId;
            SerializedData = serializedData;
        }

        public override string ToString()
        {
            return "[ItemData ContainerId: " + ContainerId + "Id: " + ItemId + "]";
        }
    }
}
