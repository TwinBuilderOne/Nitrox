using System;
using NitroxModel.DataStructures;
using NitroxModel.Packets;

namespace NitroxModel_Subnautica.Packets
{
    [Serializable]
    public class CyclopsChangeSilentRunning : Packet
    {
        public NitroxId Id { get; set; }
        public bool IsOn { get; set; }

        public CyclopsChangeSilentRunning() { }

        public CyclopsChangeSilentRunning(NitroxId id, bool isOn)
        {
            Id = id;
            IsOn = isOn;
        }

        public override string ToString()
        {
            return $"[CyclopsBeginSilentRunning - Id: {Id} , IsOn: {IsOn}]";
        }
    }
}
