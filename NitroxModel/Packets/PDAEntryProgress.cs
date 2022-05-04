using System;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class PDAEntryProgress : Packet
    {
        public NitroxTechType TechType { get; set; }
        public float Progress { get; set; }
        public int Unlocked { get; set; }
        public NitroxId NitroxId { get; set; }

        public PDAEntryProgress() { }

        public PDAEntryProgress(NitroxTechType techType, float progress, int unlocked, NitroxId nitroxId)
        {
            TechType = techType;
            Progress = progress;
            Unlocked = unlocked;
            NitroxId = nitroxId;
        }
    }
}
