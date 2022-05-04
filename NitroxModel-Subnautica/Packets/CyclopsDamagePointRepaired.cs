using System;
using NitroxModel.DataStructures;
using NitroxModel.Packets;

namespace NitroxModel_Subnautica.Packets
{
    [Serializable]
    public class CyclopsDamagePointRepaired : Packet
    {
        public NitroxId Id { get; set; }
        public int DamagePointIndex { get; set; }
        public float RepairAmount { get; set; }

        public CyclopsDamagePointRepaired() { }

        /// <param name="id">The Cyclops id</param>
        /// <param name="repairAmount">The amount to repair the damage by. A large repair amount is passed if the point is meant to be fully repaired</param>
        public CyclopsDamagePointRepaired(NitroxId id, int damagePointIndex, float repairAmount)
        {
            Id = id;
            DamagePointIndex = damagePointIndex;
            RepairAmount = repairAmount;
        }

        public override string ToString()
        {
            return $"[CyclopsDamagePointRepaired - Id: {Id}, DamagePointIndex: {DamagePointIndex}, RepairAmount: {RepairAmount}]";
        }
    }
}
