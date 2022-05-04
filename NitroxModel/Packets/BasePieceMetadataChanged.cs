using System;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.GameLogic.Buildings.Metadata;

namespace NitroxModel.Packets
{
    [Serializable]
    public class BasePieceMetadataChanged : Packet
    {
        public NitroxId PieceId { get; set; }
        public BasePieceMetadata Metadata { get; set; }

        public BasePieceMetadataChanged() { }

        public BasePieceMetadataChanged(NitroxId pieceId, BasePieceMetadata metadata)
        {
            PieceId = pieceId;
            Metadata = metadata;
        }
    }
}
