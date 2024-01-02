using System.Reflection;
using NitroxClient.Communication.Abstract;
using NitroxModel.DataStructures;
using NitroxModel.Helper;
using NitroxModel.Packets;

namespace NitroxPatcher.Patches.Dynamic;

/// <summary>
/// When we place a power crystal into the teleporter terminal it becomes consumed.  Inform the server the entity was destroyed.
/// </summary>
public sealed partial class PrecursorTeleporterActivationTerminal_OnPlayerCinematicModeEnd_Patch : NitroxPatch, IDynamicPatch
{
    private static readonly MethodInfo TARGET_METHOD = Reflect.Method((PrecursorTeleporterActivationTerminal t) => t.OnPlayerCinematicModeEnd(default(PlayerCinematicController)));

    public static void Prefix(PrecursorTeleporterActivationTerminal __instance)
    {
        if (__instance.crystalObject.TryGetIdOrWarn(out NitroxId id))
        {
            Resolve<IPacketSender>().Send(new EntityDestroyed(id));
        }
    }
}
