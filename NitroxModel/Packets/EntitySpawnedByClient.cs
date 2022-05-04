using System;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class EntitySpawnedByClient : Packet
    {
        public Entity Entity { get; set; }

        public EntitySpawnedByClient() { }

        public EntitySpawnedByClient(Entity entity)
        {
            Entity = entity;
        }
    }
}
