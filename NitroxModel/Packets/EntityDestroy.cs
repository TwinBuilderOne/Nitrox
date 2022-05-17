using System;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets;

[Serializable]
public class EntityDestroy : Packet
{
    public NitroxId EntityId { get; set; }

    public EntityDestroy() { }

    public EntityDestroy(NitroxId entityId)
    {
        EntityId = entityId;
    }
}
