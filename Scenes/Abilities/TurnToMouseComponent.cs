using Godot;
using System;

public partial class TurnToMouseComponent : AbilityComponent
{
    protected override void ActivateExec(AbilityContext context)
    {
        base.ActivateExec(context);

        var mousePositon = GetWindow().GetCamera2D().GetGlobalMousePosition();
        context.Caster.TurnToPosition(mousePositon);

        context.Caster.TurningCooldown = 0.1f;
    }
}
