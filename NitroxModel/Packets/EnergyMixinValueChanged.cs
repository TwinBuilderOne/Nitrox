using System;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class EnergyMixinValueChanged : Packet
    {
        public NitroxId OwnerId { get; set; }
        public float Value { get; set; }
        public ItemData BatteryData { get; set; }

        public EnergyMixinValueChanged() { }

        public EnergyMixinValueChanged(NitroxId ownerId, float value, ItemData batteryData)
        {
            OwnerId = ownerId;
            Value = value;
            BatteryData = batteryData;
        }
    }
}
