using System;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class AddEscapePod : Packet
    {
        public EscapePodModel EscapePod { get; set; }

        public AddEscapePod() { }

        public AddEscapePod(EscapePodModel escapePod)
        {
            EscapePod = escapePod;
        }
    }
}
