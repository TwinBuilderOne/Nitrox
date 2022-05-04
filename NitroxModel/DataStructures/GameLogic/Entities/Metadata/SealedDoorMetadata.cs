using System;
using ProtoBufNet;

namespace NitroxModel.DataStructures.GameLogic.Entities.Metadata
{
    [Serializable]
    [ProtoContract]
    public class SealedDoorMetadata : EntityMetadata
    {
        [ProtoMember(1)]
        public bool Sealed { get; set; }

        [ProtoMember(2)]
        public float OpenedAmount { get; set; }

        public SealedDoorMetadata()
        {
            //Constructor for serialization.
        }

        public SealedDoorMetadata(bool Sealed, float OpenedAmount)
        {
            this.Sealed = Sealed;
            this.OpenedAmount = OpenedAmount;
        }
    }
}
