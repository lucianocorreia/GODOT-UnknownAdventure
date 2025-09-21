using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class AbilityController : Node
{
    private Ability[] abilities;
    private Dictionary<Ability, float> cooldowns = [];
    private Entity entity;

    public override void _Ready()
    {
        base._Ready();

        entity = GetParent<Entity>();

        abilities = [.. GetChildren().OfType<Ability>()];
        GD.Print("Abilities found: " + abilities.Length);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        foreach (var ability in cooldowns.Keys.ToList())
        {
            if (cooldowns[ability] > 0)
            {
                cooldowns[ability] = Mathf.Max(0, cooldowns[ability] - (float)delta);
            }
        }
    }

    public void TriggerAbilityByIndex(int index)
    {
        if (index < 0 || index >= abilities.Length)
        {
            GD.Print("Invalid ability index: " + index);
            return;
        }

        var ability = abilities[index];
        if (ability != null)
        {
            TriggerAbility(ability);
        }
    }

    public void TriggerAbility(Ability ability)
    {
        if (cooldowns.TryGetValue(ability, out float value) && value > 0)
        {
            GD.Print("Ability on cooldown: " + ability.Name);
            return;
        }

        ability.Activate(entity);

        cooldowns[ability] = ability.Cooldown;
    }
}

