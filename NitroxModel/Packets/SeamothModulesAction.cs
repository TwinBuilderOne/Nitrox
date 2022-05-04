using System;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.GameLogic;
using NitroxModel.DataStructures.Unity;

namespace NitroxModel.Packets
{
    [Serializable]
    public class SeamothModulesAction : Packet
    {
        public NitroxTechType TechType { get; set; }
        public int SlotID { get; set; }
        public NitroxId Id { get; set; }
        public NitroxVector3 Forward { get; set; }
        public NitroxQuaternion Rotation { get; set; }

        public SeamothModulesAction() { }

        public SeamothModulesAction(NitroxTechType techType, int slotID, NitroxId id, NitroxVector3 forward, NitroxQuaternion rotation)
        {
            TechType = techType;
            SlotID = slotID;
            Id = id;
            Forward = forward;
            Rotation = rotation;
        }
    }
}
