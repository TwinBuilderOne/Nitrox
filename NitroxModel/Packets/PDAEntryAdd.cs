using System;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class PDAEntryAdd : Packet
    {
        public NitroxTechType TechType { get; set; }
        public float Progress { get; set; }
        public int Unlocked { get; set; }

        public PDAEntryAdd() { }

        public PDAEntryAdd(NitroxTechType techType, float progress, int unlocked)
        {
            TechType = techType;
            Progress = progress;
            Unlocked = unlocked;
        }
    }
}
