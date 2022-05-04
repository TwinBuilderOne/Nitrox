using System;
using NitroxModel.DataStructures;
using NitroxModel.Packets;

namespace NitroxModel_Subnautica.Packets
{
    [Serializable]
    public class RocketPreflightComplete : Packet
    {
        public NitroxId Id { get; set; }
        public PreflightCheck FlightCheck { get; set; }

        public RocketPreflightComplete() { }

        public RocketPreflightComplete(NitroxId id, PreflightCheck flightCheck)
        {
            Id = id;
            FlightCheck = flightCheck;
        }

        public override string ToString()
        {
            return $"[RocketPreflightComplete - Id: {Id}, FlightCheck: {FlightCheck}]";
        }
    }
}
