using Godot;
using System;

public partial class FollowCamera : Camera2D
{
    [Export]
    public Node2D Target { get; private set; }

    public override void _Process(double delta)
    {
        base._Process(delta);

        Position = Target.Position;
    }
}
