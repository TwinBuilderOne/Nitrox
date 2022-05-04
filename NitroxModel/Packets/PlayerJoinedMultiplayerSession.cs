using System;
using System.Collections.Generic;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.GameLogic;
using NitroxModel.DataStructures.Util;
using NitroxModel.MultiplayerSession;

namespace NitroxModel.Packets
{
    [Serializable]
    public class PlayerJoinedMultiplayerSession : Packet
    {
        public PlayerContext PlayerContext { get; set; }
        public Optional<NitroxId> SubRootId { get; set; }
        public List<NitroxTechType> EquippedTechTypes { get; set; }
        public List<ItemData> InventoryItems { get; set; }

        public PlayerJoinedMultiplayerSession() { }

        public PlayerJoinedMultiplayerSession(PlayerContext playerContext, Optional<NitroxId> subRootId, List<NitroxTechType> equippedTechTypes, List<ItemData> inventoryItems)
        {
            PlayerContext = playerContext;
            SubRootId = subRootId;
            EquippedTechTypes = equippedTechTypes;
            InventoryItems = inventoryItems;
        }
    }
}
