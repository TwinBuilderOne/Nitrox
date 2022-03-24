
namespace NitroxModel.Packets;

public class ServerPlugin : CorrelatedPacket
{
    public byte[] SerializedPlugin { get; }

    public ServerPlugin(string correlationId, byte[] serializedPlugin) : base(correlationId)
    {
        SerializedPlugin = serializedPlugin;
    }
}
