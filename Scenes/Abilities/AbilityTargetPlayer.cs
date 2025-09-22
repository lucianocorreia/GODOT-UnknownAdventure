using Godot;
using System;

public partial class AbilityTargetPlayer : AbilityComponent
{
    public override void Activate(AbilityContext context)
    {
        base.Activate(context);
        var player = GetTree().GetFirstNodeInGroup("Player") as Node2D;
        context.Target = player;
    }
}
