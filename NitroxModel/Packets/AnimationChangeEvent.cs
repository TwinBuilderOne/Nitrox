using System;

namespace NitroxModel.Packets
{
    [Serializable]
    public class AnimationChangeEvent : Packet
    {
        public ushort PlayerId { get; set; }
        public int Type { get; set; }
        public int State { get; set; }

        public AnimationChangeEvent() { }

        public AnimationChangeEvent(ushort playerId, int type, int state)
        {
            PlayerId = playerId;
            Type = type;
            State = state;
        }
    }
}
