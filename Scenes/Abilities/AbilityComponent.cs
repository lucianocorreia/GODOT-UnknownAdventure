using Godot;

public partial class AbilityComponent : Node
{
    [Export]
    public float ExecDelay { get; private set; } = 0f;

    public async void Activate(AbilityContext context)
    {
        if (ExecDelay > 0)
        {
            await ToSignal(GetTree().CreateTimer(ExecDelay), "timeout");
        }

        ActivateExec(context);
    }

    protected virtual void ActivateExec(AbilityContext context) { }
}
