using System;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class CellVisibilityChanged : Packet
    {
        public ushort PlayerId { get; set; }
        public AbsoluteEntityCell[] Added { get; set; }
        public AbsoluteEntityCell[] Removed { get; set; }

        public CellVisibilityChanged() { }

        public CellVisibilityChanged(ushort playerId, AbsoluteEntityCell[] added, AbsoluteEntityCell[] removed)
        {
            PlayerId = playerId;
            Added = added;
            Removed = removed;
        }
    }
}
