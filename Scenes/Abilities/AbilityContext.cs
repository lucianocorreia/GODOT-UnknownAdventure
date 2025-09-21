using Godot;

public partial class AbilityContext : RefCounted
{
    public Entity Caster { get; private set; }
    public Ability Ability { get; private set; }

    public AbilityContext(Entity caster, Ability ability)
    {
        Caster = caster;
        Ability = ability;
    }

}
