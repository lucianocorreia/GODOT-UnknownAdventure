using Godot;

public partial class AbilityAnimationRunner : AbilityComponent
{
    public override void Activate(AbilityContext context)
    {
        base.Activate(context);
        context.Caster.PlayAnimation(new AnimationWrapper("Slash", true));
    }
}
