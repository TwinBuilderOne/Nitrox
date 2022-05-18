using System;
using NitroxModel.Helper;
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

        public MultiplayerSessionPolicy() : base(string.Empty) { }

        public MultiplayerSessionPolicy(string correlationId, bool disableConsole, int maxConnections, bool requiresServerPassword) : base(correlationId)
        {
            RequiresServerPassword = requiresServerPassword;
            AuthenticationAuthority = MultiplayerSessionAuthenticationAuthority.SERVER;
            DisableConsole = disableConsole;
            MaxConnections = maxConnections;

            Version ver = NitroxEnvironment.Version;
            // only the major and minor version number is required
            NitroxVersionAllowed = new NitroxVersion(ver.Major, ver.Minor);
        }
    }
}
