using System;

namespace NitroxModel.Packets;

[Serializable]
public class GoalCompleted : Packet
{
    public string CompletedGoal { get; set; }

    public GoalCompleted() { }

    public GoalCompleted(string completedGoal)
    {
        CompletedGoal = completedGoal;
    }
}
