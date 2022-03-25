using NitroxModel.Logger;

namespace Plugin
{
    public class Plugin
    {
        public static void Initialize()
        {
            // sample
            Log.InGame("Initialized!");
        }

        public static void Destroy()
        {
            // also sample
            Log.InGame("Destroyed.");
        }
    }
}
