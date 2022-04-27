using System;

namespace NitroxModel.Packets
{
    [Serializable]
    public abstract class CorrelatedPacket : Packet
    {
        public string CorrelationId { get; set; }

        protected CorrelatedPacket(string correlationId)
        {
            CorrelationId = correlationId;
        }
    }
}
