using System;

namespace NitroxModel.Packets
{
    [Serializable]
    public class StoryEventSend : Packet
    {
        public EventType Type { get; set; }
        public string Key { get; set; }

        public StoryEventSend() { }

        public StoryEventSend(EventType type, string key = "")
        {
            Type = type;
            Key = key;
        }

        public enum EventType
        {
            PDA,
            RADIO,
            ENCYCLOPEDIA,
            STORY,
            GOAL_UNLOCK,
            EXTRA,
            PDA_EXTRA,
        }
    }
}
