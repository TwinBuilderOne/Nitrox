using System;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets
{
    [Serializable]
    public class ToggleLights : Packet
    {
        public NitroxId Id { get; set; }
        public bool IsOn { get; set; }

        public ToggleLights() { }

        public ToggleLights(NitroxId id, bool isOn)
        {
            Id = id;
            IsOn = isOn;
        }
    }
}
