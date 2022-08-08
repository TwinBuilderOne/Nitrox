using System;
using NitroxModel.MultiplayerSession;

namespace NitroxModel.Packets
{
    [Serializable]
    public class MultiplayerSessionReservation : CorrelatedPacket
    {
        public MultiplayerSessionReservationState ReservationState { get; }
        public ushort PlayerId { get; }
        public string ReservationKey { get; }

        public MultiplayerSessionReservation(string correlationId, MultiplayerSessionReservationState reservationState) : base(correlationId)
        {
            ReservationState = reservationState;
        }

        public MultiplayerSessionReservation(string correlationId, ushort playerId, string reservationKey, MultiplayerSessionReservationState reservationState) : this(correlationId, reservationState)
        {
            PlayerId = playerId;
            ReservationKey = reservationKey;
        }

        public MultiplayerSessionReservation(string correlationId, ushort playerId, string reservationKey)
            : this(correlationId, playerId, reservationKey, MultiplayerSessionReservationState.RESERVED) { }
    }
}
