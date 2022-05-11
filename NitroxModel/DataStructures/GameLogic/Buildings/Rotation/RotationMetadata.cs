using System;
using ProtoBufNet;

namespace NitroxModel.DataStructures.GameLogic.Buildings.Rotation
{
    [Serializable]
    [ProtoContract]
    public abstract class RotationMetadata
    {
        [ProtoIgnore]
        public string GhostType { get; set; }

        public RotationMetadata(Type ghostType)
        {
            GhostType = ghostType.ToString();
        }
    }
}
