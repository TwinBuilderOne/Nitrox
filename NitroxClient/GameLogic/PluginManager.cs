using System.Reflection;

namespace NitroxClient.GameLogic;

public class PluginManager
{
    private Assembly pluginAssembly;

    public void LoadPlugin(byte[] serialized)
    {
        Log.InGame("Installing plugin...");
        pluginAssembly = Assembly.Load(serialized);
        pluginAssembly.GetType("Plugin.Plugin").GetMethod("Initialize", BindingFlags.Static | BindingFlags.Public).Invoke(null, null);
    }

    public void UnloadPlugin()
    {
        Log.InGame("Uninstalling plugin...");
        pluginAssembly?.GetType("Plugin.Plugin")?.GetMethod("Destroy", BindingFlags.Static | BindingFlags.Public)?.Invoke(null, null);
        pluginAssembly = null;
    }
}
