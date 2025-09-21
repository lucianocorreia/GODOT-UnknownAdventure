using Godot;
using System;

public partial class SpawnComponent : AbilityComponent
{
    [Export]
    public PackedScene SceneToSpawn { get; private set; }

    [Export]
    public bool IsChild { get; private set; } = false;

    [Export]
    public Vector2 Offset { get; private set; } = Vector2.Zero;

    public override void Activate(AbilityContext context)
    {
        if (SceneToSpawn == null) { return; }

        var sceneInstance = SceneToSpawn.Instantiate<AbilityManifest>();

        if (IsChild)
        {
            context.Caster.AddChild(sceneInstance);
        }
        else
        {
            var root = GetTree().Root;
            sceneInstance.Position = context.Caster.Position;
            root.AddChild(sceneInstance);
        }

        sceneInstance.Position += Offset;
        sceneInstance.Activate(context);

    }

}
