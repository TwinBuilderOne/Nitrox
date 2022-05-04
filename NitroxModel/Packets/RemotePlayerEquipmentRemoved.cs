using System;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class RemotePlayerEquipmentRemoved : Packet
    {
        public ushort PlayerId { get; set; }
        public NitroxTechType TechType { get; set; }

        public RemotePlayerEquipmentRemoved() { }

        public RemotePlayerEquipmentRemoved(ushort playerId, NitroxTechType techType)
        {
            PlayerId = playerId;
            TechType = techType;
        }
    }
}
