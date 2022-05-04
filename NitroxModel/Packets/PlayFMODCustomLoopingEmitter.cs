using System;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets
{
    [Serializable]
    public class PlayFMODCustomLoopingEmitter : Packet
    {
        public NitroxId Id { get; set; }
        public string AssetPath { get; set; }

        public PlayFMODCustomLoopingEmitter() { }

        public PlayFMODCustomLoopingEmitter(NitroxId id, string assetPath)
        {
            Id = id;
            AssetPath = assetPath;
        }
    }
}
