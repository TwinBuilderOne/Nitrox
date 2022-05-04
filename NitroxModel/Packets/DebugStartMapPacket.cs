using System;
using System.Collections.Generic;
using NitroxModel.DataStructures.Unity;

namespace NitroxModel.Packets
{
    [Serializable]
    public class DebugStartMapPacket : Packet
    {
        public IList<NitroxVector3> StartPositions { get; set; }

        public DebugStartMapPacket() { }

        public DebugStartMapPacket(IList<NitroxVector3> startPositions)
        {
            StartPositions = startPositions;
        }
    }
}
