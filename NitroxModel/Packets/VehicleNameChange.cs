using System;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.Util;

namespace NitroxModel.Packets
{
    [Serializable]
    public class VehicleNameChange : Packet
    {
        public Optional<NitroxId> ParentId { get; set; }
        public NitroxId VehicleId { get; set; }
        public string Name { get; set; }

        public VehicleNameChange() { }

        public VehicleNameChange(NitroxId parentId, NitroxId vehicleId, string name)
        {
            ParentId = Optional.OfNullable(parentId);
            VehicleId = vehicleId;
            Name = name;
        }
    }
}
