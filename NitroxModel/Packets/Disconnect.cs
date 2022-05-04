using System;

namespace NitroxModel.Packets
{
    [Serializable]
    public class Disconnect : Packet
    {
        public ushort PlayerId { get; set; }

        public Disconnect() { }

        public Disconnect(ushort playerId)
        {
            PlayerId = playerId;
        }
    }
}
