﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using LZ4;
using NitroxModel.Networking;
using BinaryConverter = BinaryPack.BinaryConverter;

namespace NitroxModel.Packets
{
    [Serializable]
    public abstract class Packet
    {
        private static readonly Dictionary<Type, PropertyInfo[]> cachedPropertiesByType = new();
        private static readonly StringBuilder toStringBuilder = new();

        public NitroxDeliveryMethod.DeliveryMethod DeliveryMethod { get; protected set; } = NitroxDeliveryMethod.DeliveryMethod.RELIABLE_ORDERED;
        public UdpChannelId UdpChannel { get; protected set; } = UdpChannelId.DEFAULT;

        public enum UdpChannelId
        {
            DEFAULT = 0,
            PLAYER_MOVEMENT = 1,
            VEHICLE_MOVEMENT = 2,
            PLAYER_STATS = 3
        }

        public byte[] Serialize()
        {
            using MemoryStream ms = new();
            using LZ4Stream lz4Stream = new(ms, LZ4StreamMode.Compress);
            BinaryConverter.Serialize(new Wrapper(this), lz4Stream);
            return ms.ToArray();
        }

        public static Packet Deserialize(byte[] data)
        {
            using MemoryStream stream = new(data);
            using LZ4Stream lz4Stream = new(stream, LZ4StreamMode.Decompress);
            return BinaryConverter.Deserialize<Wrapper>(lz4Stream).Packet;
        }

        public WrapperPacket ToWrapperPacket()
        {
            return new WrapperPacket(Serialize());
        }

        public override string ToString()
        {
            Type packetType = GetType();

            if (!cachedPropertiesByType.TryGetValue(packetType, out PropertyInfo[] properties))
            {
                properties = packetType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                       .Where(x => x.Name is not nameof(DeliveryMethod) and not nameof(UdpChannel)).ToArray();
                cachedPropertiesByType.Add(packetType, properties);
            }

            toStringBuilder.Clear();
            toStringBuilder.Append($"[{packetType.Name}: ");
            foreach (PropertyInfo property in properties)
            {
                object propertyValue = property.GetValue(this);
                if (propertyValue is IList propertyList)
                {
                    toStringBuilder.Append($"{property.Name}: {propertyList.Count}, ");
                }
                else
                {
                    toStringBuilder.Append($"{property.Name}: {propertyValue}, ");
                }
            }
            toStringBuilder.Remove(toStringBuilder.Length - 2, 2);
            toStringBuilder.Append("]");

            return toStringBuilder.ToString();
        }

        // Wrapper class used to serialize packets in BinaryPack
        // We cannot serialize Packets directly because
        // 1) We will not know what type to deserialize to and
        // 2) The root object must use ObjectProcessor<T> so it can't be abstract
        // This class solves both problems and only adds a single byte to the data
        private class Wrapper
        {
            public Packet Packet { get; set; }

            public Wrapper() { }

            public Wrapper(Packet packet)
            {
                Packet = packet;
            }
        }
    }
}
