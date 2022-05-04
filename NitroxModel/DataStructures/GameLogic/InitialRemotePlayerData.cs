﻿using System;
using System.Collections.Generic;
using NitroxModel.DataStructures.Unity;
using NitroxModel.DataStructures.Util;
using NitroxModel.MultiplayerSession;

namespace NitroxModel.DataStructures.GameLogic
{
    [Serializable]
    public class InitialRemotePlayerData
    {
        public PlayerContext PlayerContext { get; set; }
        public NitroxVector3 Position { get; set; }
        public Optional<NitroxId> SubRootId { get; set; }
        public List<NitroxTechType> EquippedTechTypes { get; set; }

        public InitialRemotePlayerData()
        {
            // Constructor for serialization.
        }

        public InitialRemotePlayerData(PlayerContext playerContext, NitroxVector3 position, Optional<NitroxId> subRootId, List<NitroxTechType> equippedTechTypes)
        {
            PlayerContext = playerContext;
            Position = position;
            SubRootId = subRootId;
            EquippedTechTypes = equippedTechTypes;
        }
    }
}
