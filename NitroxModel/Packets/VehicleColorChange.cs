using System;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.Unity;
using NitroxModel.DataStructures.Util;

namespace NitroxModel.Packets
{
    [Serializable]
    public class VehicleColorChange : Packet
    {
        public Optional<NitroxId> ParentId { get; set; }
        public NitroxId VehicleId { get; set; }
        public int Index { get; set; }
        public NitroxVector3 HSB { get; set; }
        public NitroxColor Color { get; set; }

        public VehicleColorChange() { }

        public VehicleColorChange(int index, NitroxId parentId, NitroxId vehicleId, NitroxVector3 hsb, NitroxColor color)
        {
            ParentId = Optional.OfNullable(parentId);
            VehicleId = vehicleId;
            Index = index;
            HSB = hsb;
            Color = color;
        }
    }
}
