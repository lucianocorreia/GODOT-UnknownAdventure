using Godot;

public partial class AbilityComponent : Node
{
    public virtual void Activate(AbilityContext context)
    {
        GD.Print("AbilityComponent activated: " + Name);
    }
}
