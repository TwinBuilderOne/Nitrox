using System;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.GameLogic;
using NitroxModel.DataStructures.Unity;
using NitroxModel.DataStructures.Util;

namespace NitroxModel.Packets
{
    [Serializable]
    public class DroppedItem : Packet
    {
        public NitroxId Id { get; set; }
        public Optional<NitroxId> WaterParkId { get; set; }
        public NitroxTechType TechType { get; set; }
        public NitroxVector3 ItemPosition { get; set; }
        public NitroxQuaternion ItemRotation { get; set; }
        public byte[] Bytes { get; set; }

        public DroppedItem() { }

        public DroppedItem(NitroxId id, Optional<NitroxId> waterParkId, NitroxTechType techType, NitroxVector3 itemPosition, NitroxQuaternion itemRotation, byte[] bytes)
        {
            Id = id;
            WaterParkId = waterParkId;
            ItemPosition = itemPosition;
            ItemRotation = itemRotation;
            TechType = techType;
            Bytes = bytes;
        }
    }
}
