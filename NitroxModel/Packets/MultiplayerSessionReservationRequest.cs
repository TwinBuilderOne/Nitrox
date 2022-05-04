﻿using System;
using NitroxModel.MultiplayerSession;

namespace NitroxModel.Packets
{
    [Serializable]
    public class MultiplayerSessionReservationRequest : CorrelatedPacket
    {
        public PlayerSettings PlayerSettings { get; set; }
        public AuthenticationContext AuthenticationContext { get; set; }

        public MultiplayerSessionReservationRequest() : base("") { }

        public MultiplayerSessionReservationRequest(string reservationCorrelationId, PlayerSettings playerSettings, AuthenticationContext authenticationContext) : base(reservationCorrelationId)
        {
            PlayerSettings = playerSettings;
            AuthenticationContext = authenticationContext;
        }
    }
}
