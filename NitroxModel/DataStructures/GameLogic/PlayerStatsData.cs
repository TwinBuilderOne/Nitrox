using System;
using ProtoBufNet;

namespace NitroxModel.DataStructures.GameLogic
{
    [Serializable]
    [ProtoContract]
    public class PlayerStatsData
    {
        [ProtoMember(1)]
        public float Oxygen { get; set; }

        [ProtoMember(2)]
        public float MaxOxygen { get; set; }

        [ProtoMember(3)]
        public float Health { get; set; }

        [ProtoMember(4)]
        public float Food { get; set; }

        [ProtoMember(5)]
        public float Water { get; set; }
        [ProtoMember(6)]
        public float InfectionAmount { get; set; }

        public PlayerStatsData()
        {
            // Constructor for serialization.
        }

        public PlayerStatsData(float oxygen, float maxOxygen, float health, float food, float water, float infectionAmount)
        {
            Oxygen = oxygen;
            MaxOxygen = maxOxygen;
            Health = health;
            Food = food;
            Water = water;
            InfectionAmount = infectionAmount;
        }

        public override string ToString()
        {
            return "[Oxygen: " + Oxygen + " MaxOxygen: " + MaxOxygen + " Health: " + Health + " Food: " + Food + " Water: " + Water + " InfectionAmount: " + InfectionAmount + " ]";
        }
    }
}
