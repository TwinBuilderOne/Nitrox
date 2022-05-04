using System;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class RemotePlayerEquipmentAdded : Packet
    {
        public ushort PlayerId { get; set; }
        public NitroxTechType TechType { get; set; }

        public RemotePlayerEquipmentAdded() { }

        public RemotePlayerEquipmentAdded(ushort playerId, NitroxTechType techType)
        {
            PlayerId = playerId;
            TechType = techType;
        }
    }
}
