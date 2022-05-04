using System;
using NitroxModel.DataStructures;
using NitroxModel.Packets;

namespace NitroxModel_Subnautica.Packets
{
    [Serializable]
    public class CyclopsChangeEngineMode : Packet
    {
        public NitroxId Id { get; set; }
        public CyclopsMotorMode.CyclopsMotorModes Mode { get; set; }

        public CyclopsChangeEngineMode() { }

        public CyclopsChangeEngineMode(NitroxId id, CyclopsMotorMode.CyclopsMotorModes mode)
        {
            Id = id;
            Mode = mode;
        }

        public override string ToString()
        {
            return $"[CyclopsChangeEngineMode - Id: {Id}, Mode: {Mode}]";
        }
    }
}
