using System.Threading.Tasks;
using Godot;

public partial class AbilityAnimationRunner : AbilityComponent
{
    protected override void ActivateExec(AbilityContext context)
    {
        context.Caster.PlayAnimation(new AnimationWrapper("Slash", true));
    }
}
