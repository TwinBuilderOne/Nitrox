using System;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class ConstructorBeginCrafting : Packet
    {
        public VehicleModel VehicleModel { get; set; }
        public NitroxId ConstructorId { get; set; }
        public float Duration { get; set; }

        public ConstructorBeginCrafting() { }

        public ConstructorBeginCrafting(VehicleModel vehicleModel, NitroxId constructorId, float duration)
        {
            ConstructorId = constructorId;
            VehicleModel = vehicleModel;
            Duration = duration;
        }
    }
}
