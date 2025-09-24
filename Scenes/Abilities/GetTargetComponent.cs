using Godot;
using System;

public partial class GetTargetComponent : AbilityComponent
{
    [Export]
    public float Radius = 30;
    protected override void ActivateExec(AbilityContext context)
    {
        base.ActivateExec(context);
    }

    public void CheckCollidders(Entity caster)
    {

    }
}
