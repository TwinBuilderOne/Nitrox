using System;
using NitroxModel.DataStructures.Unity;

namespace NitroxModel.Packets
{
    [Serializable]
    public class PlayerDeathEvent : Packet
    {
        public string PlayerName { get; set; }
        public NitroxVector3 DeathPosition { get; set; }

        public PlayerDeathEvent() { }

        public PlayerDeathEvent(string playerName, NitroxVector3 deathPosition)
        {
            PlayerName = playerName;
            DeathPosition = deathPosition;
        }
    }
}
