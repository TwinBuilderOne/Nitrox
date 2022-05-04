using System;
using ProtoBufNet;

namespace NitroxModel.DataStructures.GameLogic.Entities.Metadata
{
    [Serializable]
    [ProtoContract]
    public class StarshipDoorMetadata : EntityMetadata
    {
        [ProtoMember(1)]
        public bool DoorLocked { get; set; }
        [ProtoMember(2)]
        public bool DoorOpen { get; set; }

        public StarshipDoorMetadata()
        {
            //Constructor for serialization.
        }

        public StarshipDoorMetadata(bool doorLocked, bool doorOpen)
        {
            DoorLocked = doorLocked;
            DoorOpen = doorOpen;
        }

        public override string ToString()
        {
            return $"[StarshipDoorMetadata DoorLocked: {DoorLocked} DoorOpen: {DoorOpen}]";
        }
    }
}
