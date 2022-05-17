using System;
using NitroxModel.DataStructures.Unity;

namespace NitroxModel.Packets
{
    [Serializable]
    public class PlayerDeathEvent : Packet
    {
        public ushort PlayerId { get; set; }
        public NitroxVector3 DeathPosition { get; set; }

        public PlayerDeathEvent() { }

        public PlayerDeathEvent(ushort playerId, NitroxVector3 deathPosition)
        {
            PlayerId = playerId;
            DeathPosition = deathPosition;
        }
    }
}
