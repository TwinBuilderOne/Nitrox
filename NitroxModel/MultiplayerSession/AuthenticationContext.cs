using System;
using NitroxModel.DataStructures.Util;

namespace NitroxModel.MultiplayerSession
{
    [Serializable]
    public class AuthenticationContext
    {
        public string Username { get; set; }
        public Optional<string> ServerPassword { get; set; }

        public AuthenticationContext(string username = "", string serverPassword = null)
        {
            Username = username;
            ServerPassword = Optional.OfNullable(serverPassword);
        }
    }
}
