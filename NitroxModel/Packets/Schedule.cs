using System;

namespace NitroxModel.Packets
{
    [Serializable]
    public class Schedule : Packet
    {
        public float TimeExecute { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }

        public Schedule() { }

        public Schedule(float timeExecute, string key, string type)
        {
            TimeExecute = timeExecute;
            Key = key;
            Type = type;
        }
    }
}
