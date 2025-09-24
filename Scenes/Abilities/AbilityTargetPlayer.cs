using Godot;
using System;

public partial class AbilityTargetPlayer : AbilityComponent
{
    protected override void ActivateExec(AbilityContext context)
    {
        var player = GetTree().GetFirstNodeInGroup("Player") as Node2D;
        context.Target = player;
    }
}
