using System;
using System.Collections.Generic;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class CellEntities : Packet
    {
        public List<Entity> Entities { get; set; }

        public CellEntities() { }

        public CellEntities(List<Entity> entities)
        {
            Entities = entities;
        }

        public CellEntities(Entity entity)
        {
            Entities = new List<Entity>
            {
                entity
            };
        }
    }
}
