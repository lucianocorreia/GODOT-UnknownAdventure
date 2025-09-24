using Godot;
using System;

public partial class AbilityDealDamage : AbilityComponent
{
    [Export]
    public float DamageAmount { get; private set; } = 10f;

    protected override void ActivateExec(AbilityContext context)
    {
        if (context.Target == null) { return; }

        if (context.Target is Entity entity)
        {
            entity.ApplyDamage(DamageAmount);
        }

        GD.Print($"{context.Target.Name} deals damage with {context.Ability.Name}");
    }
}
