using System;
using NitroxModel.DataStructures.Util;

namespace NitroxModel.MultiplayerSession
{
    [Serializable]
    public class AuthenticationContext
    {
        public string Username { get; set; }
        public Optional<string> ServerPassword { get; set; }

        public AuthenticationContext(string username, Optional<string> serverPassword)
        {
            Username = username;
            ServerPassword = serverPassword;
        }
    }
}
