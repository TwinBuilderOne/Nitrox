using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NitroxModel.Packets;

namespace NitroxTest.Model.Packets
{
    [TestClass]
    public class PacketsSerializableTest
    {
        [TestMethod]
        public void InitSerializerTest()
        {
            // TODO: load NitroxModel-Subnautica assembly first
            Packet.InitSerializer();
        }

        [TestMethod]
        public void PacketSerializationTest()
        {
            Type testedType = null;
            List<Packet> packets = new();

            foreach (Type type in typeof(Packet).Assembly.GetTypes()
            .Where(p => typeof(Packet).IsAssignableFrom(p) && p.IsClass && !p.IsAbstract))
            {
                testedType = type;
                // TODO: Recursively initialize reference types (their serialization code will not be called if they're null)
                packets.Add((Packet)FormatterServices.GetUninitializedObject(type));
            }

            foreach (Packet packet in packets)
            {
                testedType = packet.GetType();
                Packet deserialized = Packet.Deserialize(packet.Serialize());
                // TODO: Check for equality between packets
            }
        }
    }
}
