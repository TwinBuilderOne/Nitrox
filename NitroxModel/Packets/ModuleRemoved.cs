using System;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets
{
    [Serializable]
    public class ModuleRemoved : Packet
    {
        public NitroxId OwnerId { get; set; }
        public string Slot { get; set; }
        public NitroxId ItemId { get; set; }
        public bool PlayerModule { get; set; }

        public ModuleRemoved() { }

        public ModuleRemoved(NitroxId ownerId, string slot, NitroxId itemId, bool playerModule)
        {
            OwnerId = ownerId;
            Slot = slot;
            ItemId = itemId;
            PlayerModule = playerModule;
        }
    }
}
