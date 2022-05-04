using System;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets
{
    [Serializable]
    public class MedicalCabinetClicked : Packet
    {
        public NitroxId Id { get; set; }
        public bool DoorOpen { get; set; }
        public bool HasMedKit { get; set; }
        public float NextSpawnTime { get; set; }

        public MedicalCabinetClicked() { }

        public MedicalCabinetClicked(NitroxId id, bool doorOpen, bool hasMedKit, float nextSpawnTime)
        {
            Id = id;
            DoorOpen = doorOpen;
            HasMedKit = hasMedKit;
            NextSpawnTime = nextSpawnTime;
        }
    }
}
