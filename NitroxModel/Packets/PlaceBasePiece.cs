using System;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class PlaceBasePiece : Packet
    {
        public BasePiece BasePiece { get; set; }

        public PlaceBasePiece() { }

        public PlaceBasePiece(BasePiece basePiece)
        {
            BasePiece = basePiece;
        }
    }
}
