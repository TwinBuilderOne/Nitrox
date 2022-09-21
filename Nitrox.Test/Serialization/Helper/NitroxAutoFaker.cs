using System;
using System.Collections.Generic;
using System.Linq;
using AutoBogus;
using NitroxModel.DataStructures.GameLogic;

namespace Nitrox.Test.Serialization.Helper;

public class NitroxAutoFaker<TType> : AutoFaker<TType> where TType : class
{
    public NitroxAutoFaker() : this(new Dictionary<Type, Type[]>()) { }

    public NitroxAutoFaker(Dictionary<Type, Type[]> subtypesByBaseType)
    {
        Configure(newBinder =>
        {
            newBinder.WithBinder(new PacketAutoBinder(subtypesByBaseType))
                     .WithOverride(new NitroxTechTypeOverride());

            if (subtypesByBaseType.Values.Count != 0)
            {
                int highestAbstractObjectCount = subtypesByBaseType.Values.Max(objects => objects.Length);
                newBinder.WithRepeatCount(Math.Max(2, highestAbstractObjectCount));
            }
        });
        Binder = new PacketAutoBinder(subtypesByBaseType);
    }

    private class NitroxTechTypeOverride : AutoGeneratorOverride
    {
        public override bool CanOverride(AutoGenerateContext context)
        {
            return context.GenerateType == typeof(NitroxTechType);
        }

        public override void Generate(AutoGenerateOverrideContext context)
        {
            context.Instance = new NitroxTechType(context.Faker.PickRandom<TechType>().ToString());
        }
    }
}
