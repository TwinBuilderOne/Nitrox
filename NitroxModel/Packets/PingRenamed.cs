using System;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets
{
    [Serializable]
    public class PingRenamed : Packet
    {
        public NitroxId Id { get; set; }
        public string Name { get; set; }
        public byte[] BeaconGameObjectSerialized { get; set; }

        public PingRenamed() { }

        public PingRenamed(NitroxId id, string name, byte[] beaconGameObjectSerialized)
        {
            Id = id;
            Name = name;
            BeaconGameObjectSerialized = beaconGameObjectSerialized;
        }
    }
}
