using System;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.GameLogic.Buildings.Rotation;
using ProtoBufNet;

namespace NitroxModel_Subnautica.DataStructures.GameLogic.Buildings.Rotation
{
    [Serializable]
    [ProtoContract]
    public class BaseModuleBuilderMetadata : BuilderMetadata
    {
        // Base modules anchor based on a face.  This can be constructed via these two attributes.
        [ProtoMember(1)]
        public NitroxInt3 Cell { get; set; }

        [ProtoMember(2)]
        public int Direction { get; set; }

        public BaseModuleBuilderMetadata()
        {
            // Constructor for serialization.
        }

        public BaseModuleBuilderMetadata(NitroxInt3 cell, int direction)
        {
            Cell = cell;
            Direction = direction;
        }

        public override string ToString()
        {
            return $"[BaseModuleRotationMetadata - Cell: {Cell}, Direction: {Direction}]";
        }
    }
}
