using System;
using NitroxModel.DataStructures.Unity;

namespace NitroxModel.Packets
{
    [Serializable]
    public class PlayFMODAsset : Packet
    {
        public string AssetPath { get; set; }
        public NitroxVector3 Position { get; set; }
        public float Volume { get; set; }
        public float Radius { get; set; }
        public bool IsGlobal { get; set; }

        public PlayFMODAsset() { }

        public PlayFMODAsset(string assetPath, NitroxVector3 position, float volume, float radius, bool isGlobal)
        {
            AssetPath = assetPath;
            Position = position;
            Volume = volume;
            Radius = radius;
            IsGlobal = isGlobal;
        }
    }
}
