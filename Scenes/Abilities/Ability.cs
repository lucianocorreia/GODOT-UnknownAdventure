using Godot;
using System;
using System.Linq;

public partial class Ability : Node
{
    [Export]
    public float Cooldown { get; private set; } = 2.0f;

    public void Activate(Entity entity)
    {
        var context = new AbilityContext(entity, this);

        ActivateComponents(context);
    }

    private void ActivateComponents(AbilityContext context)
    {
        foreach (var component in GetChildren().OfType<AbilityComponent>())
        {
            component.Activate(context);
        }
    }
}
