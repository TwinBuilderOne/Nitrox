using System;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets
{
    [Serializable]
    public class PlayFMODStudioEmitter : Packet
    {
        public NitroxId Id { get; set; }
        public string AssetPath { get; set; }
        public bool Play { get; set; }
        public bool AllowFadeout { get; set; }

        public PlayFMODStudioEmitter() { }

        public PlayFMODStudioEmitter(NitroxId id, string assetPath, bool play, bool allowFadeout)
        {
            Id = id;
            AssetPath = assetPath;
            Play = play;
            AllowFadeout = allowFadeout;
        }
    }
}
