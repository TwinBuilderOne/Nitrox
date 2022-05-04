using System;
using NitroxModel.DataStructures;
using NitroxModel.Packets;

namespace NitroxModel_Subnautica.Packets
{
    [Serializable]
    public class CyclopsToggleEngineState : Packet
    {
        public NitroxId Id { get; set; }
        public bool IsOn { get; set; }
        public bool IsStarting { get; set; }

        public CyclopsToggleEngineState() { }

        public CyclopsToggleEngineState(NitroxId id, bool isOn, bool isStarting)
        {
            Id = id;
            IsOn = isOn;
            IsStarting = isStarting;
        }

        public override string ToString()
        {
            return $"[CyclopsToggleEngineState - Id: {Id}, IsOn: {IsOn}, IsStarting: {IsStarting}]";
        }
    }
}
