using System;
using NitroxModel.DataStructures.Unity;
using NitroxModel.Networking;

namespace NitroxModel.Packets
{
    [Serializable]
    public class Movement : Packet
    {
        public ushort PlayerId { get; set; }
        public NitroxVector3 Position { get; set; }
        public NitroxVector3 Velocity { get; set; }
        public NitroxQuaternion BodyRotation { get; set; }
        public NitroxQuaternion AimingRotation { get; set; }

        public Movement() { }

        public Movement(ushort playerId, NitroxVector3 position, NitroxVector3 velocity, NitroxQuaternion bodyRotation, NitroxQuaternion aimingRotation)
        {
            PlayerId = playerId;
            Position = position;
            Velocity = velocity;
            BodyRotation = bodyRotation;
            AimingRotation = aimingRotation;
            DeliveryMethod = NitroxDeliveryMethod.DeliveryMethod.UNRELIABLE_SEQUENCED;
            UdpChannel = UdpChannelId.PLAYER_MOVEMENT;
        }
    }
}
