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
        public void PacketSerializationTest()
        {
            Type testedType = null;
            List<Packet> packets = new();

            foreach (Type type in typeof(Packet).Assembly.GetTypes()
            .Where(p => typeof(Packet).IsAssignableFrom(p) && p.IsClass && !p.IsAbstract))
            {
                testedType = type;
                packets.Add((Packet)FormatterServices.GetUninitializedObject(type));
            }

            foreach (Packet packet in packets)
            {
                testedType = packet.GetType();

                Packet.Deserialize(packet.Serialize()); // Consider testing for equality? Would be annoying to implement though
            }
        }
    }


}
