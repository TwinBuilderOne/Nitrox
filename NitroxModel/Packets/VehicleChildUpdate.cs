using System;
using System.Collections.Generic;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class VehicleChildUpdate : Packet
    {
        public NitroxId VehicleId { get; set; }
        public List<InteractiveChildObjectIdentifier> InteractiveChildIdentifiers { get; set; }

        public VehicleChildUpdate() { }

        public VehicleChildUpdate(NitroxId vehicleId, List<InteractiveChildObjectIdentifier> interactiveChildIdentifiers)
        {
            VehicleId = vehicleId;
            InteractiveChildIdentifiers = interactiveChildIdentifiers;
        }
    }
}
