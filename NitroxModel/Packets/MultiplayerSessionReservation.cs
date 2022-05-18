using System;
using NitroxModel.MultiplayerSession;

namespace NitroxModel.Packets
{
    [Serializable]
    public class MultiplayerSessionReservation : CorrelatedPacket
    {
        public MultiplayerSessionReservationState ReservationState { get; set; }
        public ushort PlayerId { get; set; }
        public string ReservationKey { get; set; }

        public MultiplayerSessionReservation() : base(string.Empty) { }

        public MultiplayerSessionReservation(string correlationId, MultiplayerSessionReservationState reservationState) : base(correlationId)
        {
            ReservationState = reservationState;
        }

        public MultiplayerSessionReservation(string correlationId, ushort playerId, string reservationKey) : this(correlationId, MultiplayerSessionReservationState.RESERVED)
        {
            PlayerId = playerId;
            ReservationKey = reservationKey;
        }
    }
}
