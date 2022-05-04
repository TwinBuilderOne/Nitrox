using System;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.Util;

namespace NitroxModel.Packets
{
    [Serializable]
    public class SubRootChanged : Packet
    {
        public ushort PlayerId { get; set; }
        public Optional<NitroxId> SubRootId { get; set; }

        public SubRootChanged() { }

        public SubRootChanged(ushort playerId, Optional<NitroxId> subRootId)
        {
            PlayerId = playerId;
            SubRootId = subRootId;
        }
    }
}
