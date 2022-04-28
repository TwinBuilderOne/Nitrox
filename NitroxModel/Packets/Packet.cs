using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NitroxModel.Networking;
using BinaryConverter = BinaryPack.BinaryConverter;

namespace NitroxModel.Packets
{
    [Serializable]
    public abstract class Packet
    {
        private static readonly Dictionary<Type, PropertyInfo[]> cachedPropertiesByType = new();
        private static readonly StringBuilder toStringBuilder = new();

        static Packet()
        {
            static IEnumerable<Type> FindTypesInModelAssemblies()
            {
                return AppDomain.CurrentDomain.GetAssemblies()
                                              .Where(assembly => new string[] { "NitroxModel", "NitroxModel-Subnautica" }
                                                                 .Contains(assembly.GetName().Name))
                                              .SelectMany(assembly => assembly.GetTypes());
            }

            static IEnumerable<Type> FindUnionBaseTypes() => FindTypesInModelAssemblies()
                .Where(t => t.IsAbstract && !t.IsSealed && (!t.BaseType?.IsAbstract ?? true) && !t.ContainsGenericParameters);

            foreach (Type type in FindUnionBaseTypes())
            {
                BinaryConverter.RegisterUnion(type, FindTypesInModelAssemblies().Where(t => type.IsAssignableFrom(t)
                                                                                            && !t.IsAbstract && !t.IsInterface)
                                                                                .ToArray());
            }
        }

        public NitroxDeliveryMethod.DeliveryMethod DeliveryMethod { get; set; } = NitroxDeliveryMethod.DeliveryMethod.RELIABLE_ORDERED;
        public UdpChannelId UdpChannel { get; set; } = UdpChannelId.DEFAULT;

        public enum UdpChannelId
        {
            DEFAULT = 0,
            PLAYER_MOVEMENT = 1,
            VEHICLE_MOVEMENT = 2,
            PLAYER_STATS = 3
        }

        public byte[] Serialize()
        {
            return BinaryConverter.Serialize(new Wrapper(this));
        }

        public static Packet Deserialize(byte[] data)
        {
            return BinaryConverter.Deserialize<Wrapper>(data).Packet;
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
        public class Wrapper
        {
            public Packet Packet { get; set; }

            public Wrapper()
            { }

            public Wrapper(Packet packet)
            {
                Packet = packet;
            }
        }
    }
}
