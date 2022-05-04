using System;

namespace NitroxModel.Packets
{
    [Serializable]
    public class PDAEncyclopediaEntryAdd : Packet
    {
        public string Key { get; set; }

        public PDAEncyclopediaEntryAdd() { }

        public PDAEncyclopediaEntryAdd(string key)
        {
            Key = key;
        }
    }
}
