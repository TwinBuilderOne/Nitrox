using System;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.GameLogic.Entities.Metadata;

namespace NitroxModel.Packets
{
    [Serializable]
    public class EntityMetadataUpdate : Packet
    {
        public NitroxId Id { get; set; }

        public EntityMetadata NewValue { get; set; }

        public EntityMetadataUpdate() { }

        public EntityMetadataUpdate(NitroxId id, EntityMetadata newValue)
        {
            Id = id;
            NewValue = newValue;
        }
    }
}
