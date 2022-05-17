using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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

        public IEnumerable<FieldInfo> GetSerializedFields(Type type, bool nonPublic) =>
            type.GetFields(BindingFlags.Instance | BindingFlags.Public | (nonPublic ? BindingFlags.NonPublic : BindingFlags.Default))
            .Where(f => f.GetCustomAttribute<IgnoredMemberAttribute>() == null
                && f.GetCustomAttribute<CompilerGeneratedAttribute>() == null);

        public IEnumerable<PropertyInfo> GetSerializedProperties(Type type, bool nonPublic) =>
            type.GetProperties(BindingFlags.Instance | BindingFlags.Public
                | (nonPublic ? BindingFlags.NonPublic : BindingFlags.Default))
            .Where(p => p.CanRead && p.CanWrite && p.GetCustomAttribute<IgnoredMemberAttribute>() == null
                && p.GetIndexParameters().Length == 0);

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

                foreach (PropertyInfo property in GetSerializedProperties(t, true))
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

                foreach (FieldInfo field in GetSerializedFields(t, false))
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
            IEnumerable<object> DefaultObjects(Type type, int[] abstractIndices = null)
            {
                IEnumerable<Type> Subclasses(Type type) =>
                    ModelTypes.Where(t => t.IsSubclassOf(type) && t.IsClass && !t.IsAbstract);

                if (abstractIndices == null)
                {
                    abstractIndices = Array.Empty<int>();
                }

                if (type.IsArray)
                {
                    object[] defaultObjects = DefaultObjects(type.GetElementType()).ToArray();
                    Array array = Array.CreateInstance(type.GetElementType(), defaultObjects.Length);

                    for (int i = 0; i < defaultObjects.Length; i++)
                    {
                        array.SetValue(defaultObjects[i], i);
                    }

                    yield return array;
                    yield break;
                }

                if (type == typeof(string))
                {
                    yield return string.Empty;
                    yield break;
                }

                object result = Activator.CreateInstance(type);

                int abstractTypes = 0;

                foreach (FieldInfo field in GetSerializedFields(type, false))
                {
                    if (field.FieldType == type)
                    {
                        continue;
                    }

                    if (field.FieldType.IsAbstract)
                    {
                        Type[] subclasses = Subclasses(type).ToArray();

                        if (abstractIndices.Length > abstractTypes)
                        {
                            field.SetValue(result, subclasses[abstractIndices[abstractTypes]]);
                        }
                        else
                        {
                            for (int i = 0; i < subclasses.Length; i++)
                            {
                                yield return DefaultObjects(type, abstractIndices.Append(i).ToArray());
                            }
                        }

                        abstractTypes++;
                    }
                    else
                    {
                        field.SetValue(result, DefaultObjects(field.FieldType).First());
                    }
                }

                foreach (PropertyInfo property in GetSerializedProperties(type, false))
                {
                    if (property.PropertyType == type)
                    {
                        continue;
                    }

                    if (property.PropertyType.IsAbstract)
                    {
                        Type[] subclasses = Subclasses(type).ToArray();

                        if (abstractIndices.Length > abstractTypes)
                        {
                            property.SetValue(result, subclasses[abstractIndices[abstractTypes]]);
                        }
                        else
                        {
                            for (int i = 0; i < subclasses.Length; i++)
                            {
                                yield return DefaultObjects(type, abstractIndices.Append(i).ToArray());
                            }
                        }

                        abstractTypes++;
                    }
                    else
                    {
                        property.SetValue(result, DefaultObjects(property.PropertyType).First());
                    }
                }

                yield return result;
            }

            void TestEqual(object first, object second)
            {
                if (first == null && second == null)
                {
                    return;
                }

                if (first == null || second == null)
                {
                    Assert.Fail("One value is null and the other is not");
                }

                if (first.GetType() != second.GetType())
                {
                    Assert.Fail($"{first} and {second} do not have the same type");
                }

                Type t = first.GetType();

                switch (t)
                {
                    case Type when t.IsPrimitive || t.IsEnum || t == typeof(decimal) || t == typeof(string):
                        if (!(bool)(t.GetMethod("op_Equality")?.Invoke(null, new object[] { first, second })
                            ?? t.GetMethod(nameof(object.Equals), new Type[] { typeof(object) })
                            .Invoke(first, new object[] { second })))
                        {
                            Assert.Fail($"The equality operator returned false for {first} and {second}");
                        }

                        break;

                    case Type when t.GetInterfaces().Contains(typeof(IEnumerable<>)):
                        MethodInfo toArray = typeof(Enumerable).GetMethod(nameof(Enumerable.ToArray));

                        IEnumerator firstEnumerator = ((Array)toArray.Invoke(first, null)).GetEnumerator();
                        IEnumerator secondEnumerator = ((Array)toArray.Invoke(second, null)).GetEnumerator();

                        while (firstEnumerator.MoveNext())
                        {
                            if (!secondEnumerator.MoveNext())
                            {
                                Assert.Fail($"{second} has fewer items than {first}");
                            }

                            if (firstEnumerator.Current != secondEnumerator.Current)
                            {
                                Assert.Fail($"Item {firstEnumerator.Current} of {first} is not equal to item {secondEnumerator.Current} of {second}");
                            }
                        }

                        if (secondEnumerator.MoveNext())
                        {
                            Assert.Fail($"{first} has fewer items than {second}");
                        }

                        break;

                    default:
                        foreach (FieldInfo field in GetSerializedFields(t, false))
                        {
                            TestEqual(field.GetValue(first), field.GetValue(second));
                        }

                        foreach (PropertyInfo property in GetSerializedProperties(t, false))
                        {
                            TestEqual(property.GetValue(first), property.GetValue(second));
                        }

                        break;
                }
            }

            Type testedType = null;
            List<Packet> packets = new();
            foreach (Type type in PacketTypes)
            {
                testedType = type;
                packets.AddRange(DefaultObjects(type).Select(obj => (Packet)obj));
            }

            foreach (Packet packet in packets)
            {
                testedType = packet.GetType();

                TestEqual(packet, Packet.Deserialize(packet.Serialize()));
            }
        }
    }
}
