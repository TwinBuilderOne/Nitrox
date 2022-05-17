using System;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets;

[Serializable]
public class PlayerCinematicControllerCall : Packet
{
    public ushort PlayerId { get; set; }
    public NitroxId ControllerID { get; set; }
    public int ControllerNameHash { get; set; }
    public string Key { get; set; }
    public bool StartPlaying { get; set; }

    public PlayerCinematicControllerCall() { }

    public PlayerCinematicControllerCall(ushort playerId, NitroxId controllerID, int controllerNameHash, string key, bool startPlaying)
    {
        PlayerId = playerId;
        ControllerID = controllerID;
        ControllerNameHash = controllerNameHash;
        Key = key;
        StartPlaying = startPlaying;
    }
}
