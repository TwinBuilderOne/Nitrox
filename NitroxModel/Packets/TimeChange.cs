using System;

namespace NitroxModel.Packets
{
    [Serializable]
    public class TimeChange : Packet
    {
        public double CurrentTime { get; set; }
        public bool InitialSync { get; set; }

        public TimeChange() { }

        public TimeChange(double currentTime, bool initialSync)
        {
            CurrentTime = currentTime;
            InitialSync = initialSync;
        }
    }
}
