using System.Reflection;
using NitroxClient.Communication.Packets.Processors.Abstract;
using NitroxClient.GameLogic;
using NitroxModel.Packets;

namespace NitroxClient.Communication.Packets.Processors;

class ServerPluginProcessor : ClientPacketProcessor<ServerPlugin>
{
    private PluginManager pluginManager;

    public ServerPluginProcessor(PluginManager pluginManager)
    {
        this.pluginManager = pluginManager;
    }

    public override void Process(ServerPlugin packet)
    {
        pluginManager.LoadPlugin(packet.SerializedPlugin);
    }
}
