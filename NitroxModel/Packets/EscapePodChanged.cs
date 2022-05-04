using System;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.Util;

namespace NitroxModel.Packets
{
    [Serializable]
    public class EscapePodChanged : Packet
    {
        public ushort PlayerId { get; set; }
        public Optional<NitroxId> EscapePodId { get; set; }

        public EscapePodChanged() { }

        public EscapePodChanged(ushort playerId, Optional<NitroxId> escapePodId)
        {
            PlayerId = playerId;
            EscapePodId = escapePodId;
        }
    }
}

