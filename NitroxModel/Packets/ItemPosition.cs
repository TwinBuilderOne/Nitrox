using System;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.Unity;

namespace NitroxModel.Packets
{
    [Serializable]
    public class ItemPosition : Packet
    {
        public NitroxId Id { get; set; }
        public NitroxVector3 Position { get; set; }
        public NitroxQuaternion Rotation { get; set; }

        public ItemPosition() { }

        public ItemPosition(NitroxId id, NitroxVector3 position, NitroxQuaternion rotation)
        {
            Id = id;
            Position = position;
            Rotation = rotation;
        }
    }
}
