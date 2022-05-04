using System;
using NitroxModel.Networking;

namespace NitroxModel.Packets
{
    [Serializable]
    public class PlayerStats : Packet
    {
        public ushort PlayerId { get; set; }
        public float Oxygen { get; set; }
        public float MaxOxygen { get; set; }
        public float Health { get; set; }
        public float Food { get; set; }
        public float Water { get; set; }
        public float InfectionAmount { get; set; }

        public PlayerStats() { }

        public PlayerStats(ushort playerId, float oxygen, float maxOxygen, float health, float food, float water, float infectionAmount)
        {
            PlayerId = playerId;
            Oxygen = oxygen;
            MaxOxygen = maxOxygen;
            Health = health;
            Food = food;
            Water = water;
            InfectionAmount = infectionAmount;
            DeliveryMethod = NitroxDeliveryMethod.DeliveryMethod.UNRELIABLE_SEQUENCED;
            UdpChannel = UdpChannelId.PLAYER_STATS;
        }
    }
}
