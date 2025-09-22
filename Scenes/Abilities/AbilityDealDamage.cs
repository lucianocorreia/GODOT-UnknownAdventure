using Godot;
using System;

public partial class AbilityDealDamage : AbilityComponent
{
    public override void Activate(AbilityContext context)
    {
        base.Activate(context);
        if (context.Target == null) { return; }

        GD.Print($"{context.Target.Name} deals damage with {context.Ability.Name}");
    }
}
