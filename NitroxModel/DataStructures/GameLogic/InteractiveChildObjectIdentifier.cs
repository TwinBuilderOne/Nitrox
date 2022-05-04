﻿using System;
using ProtoBufNet;

namespace NitroxModel.DataStructures.GameLogic
{
    [Serializable]
    [ProtoContract]
    public class InteractiveChildObjectIdentifier
    {
        [ProtoMember(1)]
        public NitroxId Id { get; set; }

        [ProtoMember(2)]
        public string GameObjectNamePath { get; set; }

        public InteractiveChildObjectIdentifier()
        {
            // Constructor for serialization.
        }

        public InteractiveChildObjectIdentifier(NitroxId id, string gameObjectNamePath)
        {
            Id = id;
            GameObjectNamePath = gameObjectNamePath;
        }

        public override string ToString()
        {
            return "[InteractiveChildObjectIdentifier - Id: " + Id + " GameObjectNamePath: " + GameObjectNamePath + "]";
        }
    }
}
