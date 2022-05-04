using System;
using NitroxModel.DataStructures;
using NitroxModel.MultiplayerSession;

namespace NitroxModel.Packets
{
    [Serializable]
    public class MultiplayerSessionPolicy : CorrelatedPacket
    {
        public bool RequiresServerPassword { get; set; }
        public bool DisableConsole { get; set; }
        public int MaxConnections { get; set; }

        public MultiplayerSessionAuthenticationAuthority AuthenticationAuthority { get; set; }
        public NitroxVersion NitroxVersionAllowed { get; set; }

        public MultiplayerSessionPolicy() : base("") { }

        public MultiplayerSessionPolicy(string correlationId, bool disableConsole, int maxConnections, bool requiresServerPassword) : base(correlationId)
        {
            // This is done intentionally. It is only a stub for future extension.
            RequiresServerPassword = requiresServerPassword;
            AuthenticationAuthority = MultiplayerSessionAuthenticationAuthority.SERVER;
            DisableConsole = disableConsole;
            MaxConnections = maxConnections;
            // get the full version name
            Version ver = typeof(MultiplayerSessionPolicy).Assembly.GetName().Version;
            // only the major and minor version number is required
            NitroxVersionAllowed = new NitroxVersion(ver.Major, ver.Minor);
        }
    }
}
