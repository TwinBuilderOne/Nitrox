using System;
using NitroxModel.DataStructures.Util;

namespace NitroxModel.MultiplayerSession
{
    [Serializable]
    public class AuthenticationContext
    {
        public string Username { get; set; }
        public Optional<string> ServerPassword { get; set; }

        public AuthenticationContext() : this("") { }

        public AuthenticationContext(string username) : this(username, null)
        {
        }

        public AuthenticationContext(string username, string serverPassword)
        {
            Username = username;
            ServerPassword = Optional.OfNullable(serverPassword);
        }
    }
}
