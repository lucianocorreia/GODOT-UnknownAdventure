using Godot;

public partial class AbilityContext : RefCounted
{
    public Entity Caster { get; private set; }
    public Ability Ability { get; private set; }
    public Node2D Target { get; set; } = null;

    public AbilityContext(Entity caster, Ability ability)
    {
        Caster = caster;
        Ability = ability;
    }

    public Vector2 GetTargetPosition()
    {
        return Target != null && Target is Entity ? Target.GlobalPosition : Vector2.Zero;
    }

}
