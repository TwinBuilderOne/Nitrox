using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using BinaryPack.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NitroxModel.Packets;

namespace NitroxTest.Model.Packets
{
    [TestClass]
    public class PacketsSerializableTest
    {
        public IEnumerable<Type> ModelTypes =>
            AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => Enumerable.Contains(new string[] { "NitroxModel", "NitroxModel-Subnautica" }, assembly.GetName().Name))
            .SelectMany(assembly => assembly.GetTypes());

        public IEnumerable<Type> PacketTypes =>
            ModelTypes.Where(p => typeof(Packet).IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);

        // A lot of this is taken from BinaryPack
        [TestMethod]
        public void PacketsAreSerializable()
        {
            List<Type> visitedTypes = new();

            Type[] explicitlySupportedTypes = new Type[] { typeof(string), typeof(BitArray) };
            Type[] explicitlySupportedGenericTypes = new Type[]
            {
                typeof(List<>),
                typeof(IReadOnlyList<>),
                typeof(ICollection<>),
                typeof(IReadOnlyCollection<>),
                typeof(IEnumerable<>),
                typeof(Dictionary<,>),
                typeof(IDictionary<,>),
                typeof(IReadOnlyDictionary<,>),
                typeof(Nullable<>)
            };

            void CheckType(Type t)
            {
                bool IsUnmanaged(Type type)
                {
                    if (!type.IsValueType)
                    {
                        return false;
                    }

                    if (type.IsGenericType && !IsValueTuple(type))
                    {
                        return false;
                    }

                    return
                        type.IsPrimitive ||
                        type == typeof(decimal) ||
                        type.IsPointer ||
                        type.IsEnum ||
                        type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).All(f => IsUnmanaged(f.FieldType));
                }

                bool IsValueTuple(Type type)
                {
                    if (!type.IsGenericType)
                    {
                        return false;
                    }

                    Type openType = type.GetGenericTypeDefinition();

                    return
                        openType == typeof(ValueTuple<>) ||
                        openType == typeof(ValueTuple<,>) ||
                        openType == typeof(ValueTuple<,,>) ||
                        openType == typeof(ValueTuple<,,,>) ||
                        openType == typeof(ValueTuple<,,,,>) ||
                        openType == typeof(ValueTuple<,,,,,>) ||
                        openType == typeof(ValueTuple<,,,,,,>) ||
                        openType == typeof(ValueTuple<,,,,,,,>) && IsValueTuple(type.GetGenericArguments()[7]);
                }

                bool IsGenericType(Type type, Type target) => type.IsGenericType && type.GetGenericTypeDefinition() == target;

                if (visitedTypes.Contains(t))
                {
                    return;
                }

                visitedTypes.Add(t);

                if (explicitlySupportedTypes.Contains(t)
                    || IsUnmanaged(t))
                {
                    return;
                }

                if (explicitlySupportedGenericTypes.Any(type => IsGenericType(t, type)))
                {
                    foreach (Type param in t.GetGenericArguments())
                    {
                        CheckType(param);
                    }

                    return;
                }

                if (t.IsArray)
                {
                    if (t.GetArrayRank() > 1)
                    {
                        Array array = (Array)Activator.CreateInstance(t,
                            Enumerable.Repeat(0, t.GetArrayRank()).Cast<object>().ToArray());

                        for (int dimension = 0; dimension < array.Rank; dimension++)
                        {
                            if (array.GetLowerBound(dimension) != 0)
                            {
                                Assert.Fail($"Type {t} is a variable-bound array, which is not supported");
                            }
                        }
                    }

                    CheckType(t.GetElementType());

                    return;
                }

                if (t.IsAbstract)
                {
                    foreach (Type sub in ModelTypes.Where(s => t.IsAssignableFrom(s) && s.IsClass && !s.IsAbstract))
                    {
                        CheckType(sub);
                    }

                    return;
                }

                if (!t.IsValueType && t.GetConstructor(Type.EmptyTypes) == null)
                {
                    Assert.Fail($"Type {t} does not have a public parameterless constructor");
                }

                bool empty = true;

                foreach (PropertyInfo property in t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(p => p.CanRead && p.GetCustomAttribute<IgnoredMemberAttribute>() == null && p.GetIndexParameters().Length == 0))
                {
                    if (property.GetMethod.IsPublic && property.CanWrite)
                    {
                        empty = false;
                        CheckType(property.PropertyType);
                    }

                    // Warnings for potiential errors
                    if (ModelTypes.Contains(t))
                    {
                        if (property.GetMethod.GetCustomAttribute<CompilerGeneratedAttribute>() != null)
                        {
                            if (property.CanWrite)
                            {
                                if (!property.SetMethod.IsPublic)
                                {
                                    Console.WriteLine($"Auto-implemented property {property.Name} of type {t} is writable but not public.");
                                }
                            }
                            else if (property.GetMethod.IsPublic)
                            {
                                Console.WriteLine($"Auto-implemented property {property.Name} of type {t} is public but read-only.");
                            }
                        }
                        else if (property.CanWrite)
                        {
                            Console.WriteLine($"Property {property.Name} of type {t} is not auto-generated but will be serialized.");
                        }
                    }
                }

                foreach (FieldInfo field in t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(f => f.GetCustomAttribute<IgnoredMemberAttribute>() == null
                    && f.GetCustomAttribute<CompilerGeneratedAttribute>() == null))
                {
                    if (field.IsPublic)
                    {
                        empty = false;
                        CheckType(field.FieldType);
                    }

                    if (ModelTypes.Contains(t))
                    {
                        if (field.IsInitOnly)
                        {
                            if (field.IsPublic)
                            {
                                Console.WriteLine($"Field {field.Name} of type {t} is public but read-only.");
                            }
                        }
                        else
                        {
                            if (!field.IsPublic)
                            {
                                Console.WriteLine($"Field {field.Name} of type {t} is writable but not public.");
                            }
                        }
                    }
                }

                if (empty)
                {
                    Console.WriteLine($"Type {t} has no serializable members.");
                }
            }

            foreach (Type type in PacketTypes)
            {
                CheckType(type);
            }
        }

        [TestMethod]
        public void PacketSerializationTest()
        {
            Type testedType = null;
            List<Packet> packets = new();
            foreach (Type type in PacketTypes)
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
