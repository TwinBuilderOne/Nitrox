using System;

namespace NitroxModel.Packets
{
    [Serializable]
    public class PDALogEntryAdd : Packet
    {
        public string Key { get; set; }
        public float Timestamp { get; set; }

        public PDALogEntryAdd() { }

        public PDALogEntryAdd(string key, float timestamp)
        {
            Key = key;
            Timestamp = timestamp;
        }
    }
}
