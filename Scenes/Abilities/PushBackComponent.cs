using Godot;
using System;

public partial class PushBackComponent : AbilityComponent
{

    [Export]
    public float PushDistance = 30;
    [Export]
    public float PushDuration = 1;
    [Export]
    public int Frequency = 3;

    private int PushBackCounter = 0;
    private float lastActivationTime = -1;

    protected override void ActivateExec(AbilityContext context)
    {
        base.ActivateExec(context);

        if (Time.GetTicksMsec() - lastActivationTime < 1000)
        {
            PushBackCounter = 0;
        }


        PushBackCounter++;
        if (PushBackCounter == Frequency)
        {
            PushBackCounter = 0;

            var casterPositon = context.Caster.Position;
            var mousePositon = GetWindow().GetCamera2D().GetGlobalMousePosition();

            var pushPosition = (casterPositon - mousePositon).Normalized();
            var targetPosition = casterPositon + pushPosition * PushDistance;

            var tween = CreateTween();
            tween.TweenProperty(context.Caster, "position", targetPosition, PushDuration).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.Out);

        }

        lastActivationTime = Time.GetTicksMsec();

    }


}
