using System;

namespace NitroxModel.Packets
{
    [Serializable]
    public class ChatMessage : Packet
    {
        public ushort PlayerId { get; set; }
        public string Text { get; set; }
        public const ushort SERVER_ID = ushort.MaxValue;

        public ChatMessage() { }

        public ChatMessage(ushort playerId, string text)
        {
            PlayerId = playerId;
            Text = text;
        }
    }
}
