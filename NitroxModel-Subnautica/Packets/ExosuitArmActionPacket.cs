using System;
using NitroxModel.DataStructures;
using NitroxModel.Packets;
using UnityEngine;

namespace NitroxModel_Subnautica.Packets
{
    [Serializable]
    public class ExosuitArmActionPacket : Packet
    {
        public TechType TechType { get; set; }
        public NitroxId ArmId { get; set; }
        public ExosuitArmAction ArmAction { get; set; }
        public Vector3? OpVector { get; set; }
        public Quaternion? OpRotation { get; set; }

        public ExosuitArmActionPacket() { }

        public ExosuitArmActionPacket(TechType techType, NitroxId armId, ExosuitArmAction armAction, Vector3? opVector, Quaternion? opRotation)
        {
            TechType = techType;
            ArmId = armId;
            ArmAction = armAction;
            OpVector = opVector;
            OpRotation = opRotation;
        }

        public override string ToString()
        {
            return $"[ExosuitArmAction - TechType: {TechType}, ArmId:{ArmId}, ArmAction: {ArmAction}, Vector: {OpVector}, Rotation: {OpRotation}]";
        }
    }

    public enum ExosuitArmAction
    {
        START_USE_TOOL,
        END_USE_TOOL,
        ALT_HIT
    }
}
