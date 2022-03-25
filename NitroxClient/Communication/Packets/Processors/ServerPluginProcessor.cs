using System.Reflection;
using NitroxClient.Communication.Packets.Processors.Abstract;
using NitroxModel.Packets;

namespace NitroxClient.Communication.Packets.Processors;

class ServerPluginProcessor : ClientPacketProcessor<ServerPlugin>
{
    public override void Process(ServerPlugin packet)
    {
        Log.InGame("Installing plugin...");
        Assembly pluginAssembly = Assembly.Load(packet.SerializedPlugin);
        pluginAssembly.GetType("Plugin.Plugin").GetMethod("Initialize", BindingFlags.Static | BindingFlags.Public).Invoke(null, null);
    }
}
