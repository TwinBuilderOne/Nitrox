using System;

namespace NitroxModel.Packets
{
    [Serializable]
    public class PlayerKicked : Packet
    {
        public string Reason { get; set; }

        public PlayerKicked() { }

        public PlayerKicked(string reason)
        {
            Reason = reason;
        }
    }
}
