using System;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.Unity;
using NitroxModel.DataStructures.Util;

namespace NitroxModel.Packets
{
    [Serializable]
    public class PlayerTeleported : Packet
    {
        public string PlayerName { get; set; }
        public NitroxVector3 DestinationFrom { get; set; }
        public NitroxVector3 DestinationTo { get; set; }
        public Optional<NitroxId> SubRootID { get; set; }

        public PlayerTeleported() { }

        public PlayerTeleported(string playerName, NitroxVector3 destinationFrom, NitroxVector3 destinationTo, Optional<NitroxId> subRootID)
        {
            PlayerName = playerName;
            DestinationFrom = destinationFrom;
            DestinationTo = destinationTo;
            SubRootID = subRootID;
        }
    }
}
