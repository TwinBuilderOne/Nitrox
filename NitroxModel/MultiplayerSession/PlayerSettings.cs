using System;
using NitroxModel.DataStructures.Unity;

namespace NitroxModel.MultiplayerSession
{
    [Serializable]
    public class PlayerSettings
    {
        public NitroxColor PlayerColor { get; set; }

        public PlayerSettings() { }

        public PlayerSettings(NitroxColor playerColor)
        {
            PlayerColor = playerColor;
        }
    }
}
