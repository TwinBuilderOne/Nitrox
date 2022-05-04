using System;
using NitroxModel.DataStructures;

namespace NitroxModel.MultiplayerSession
{
    [Serializable]
    public class PlayerContext
    {
        public string PlayerName { get; set; }
        public ushort PlayerId { get; set; }
        public NitroxId PlayerNitroxId { get; set; }
        public bool WasBrandNewPlayer { get; set; }
        public PlayerSettings PlayerSettings { get; set; }

        public PlayerContext() { }

        public PlayerContext(string playerName, ushort playerId, NitroxId playerNitroxId, bool wasBrandNewPlayer, PlayerSettings playerSettings)
        {
            PlayerName = playerName;
            PlayerId = playerId;
            PlayerNitroxId = playerNitroxId;
            WasBrandNewPlayer = wasBrandNewPlayer;
            PlayerSettings = playerSettings;
        }

        public override string ToString()
        {
            return $"[PlayerContext - PlayerName: {PlayerName}, PlayerId: {PlayerId}, PlayerNitroxId: {PlayerNitroxId}, WasBrandNewPlayer: {WasBrandNewPlayer}, PlayerSettings: {PlayerSettings}]";
        }
    }
}
