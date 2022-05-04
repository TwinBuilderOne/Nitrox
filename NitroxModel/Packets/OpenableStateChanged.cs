using System;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets
{
    [Serializable]
    public class OpenableStateChanged : Packet
    {
        public NitroxId Id { get; set; }
        public bool IsOpen { get; set; }
        public float Duration { get; set; }

        public OpenableStateChanged() { }

        public OpenableStateChanged(NitroxId id, bool isOpen, float duration)
        {
            Id = id;
            IsOpen = isOpen;
            Duration = duration;
        }
    }
}
