using System;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets
{
    [Serializable]
    public class PlayFMODCustomEmitter : Packet
    {
        public NitroxId Id { get; set; }
        public string AssetPath { get; set; }
        public bool Play { get; set; }

        public PlayFMODCustomEmitter() { }

        public PlayFMODCustomEmitter(NitroxId id, string assetPath, bool play)
        {
            Id = id;
            AssetPath = assetPath;
            Play = play;
        }
    }
}
