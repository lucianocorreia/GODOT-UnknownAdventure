using Godot;
using Scenes;

public partial class Enemy : Entity
{
    [Export]
    public float Speed { get; private set; } = 20f;
    [Export]
    public float StopDistance { get; private set; } = 15f;

    private Player player;
    private Vector2 velocity = Vector2.Zero;
    private float currentSpeed;
    private Vector2 lastPosition;


    public override void _Ready()
    {
        base._Ready();
        lastPosition = Position;
        player = GetTree().GetFirstNodeInGroup("Player") as Player;

    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (player != null)
        {
            var direction = (player.Position - Position).Normalized();

            if (Position.DistanceTo(player.Position) > StopDistance)
            {
                Position += direction * Speed * (float)delta;
            }

            velocity = (Position - lastPosition) / (float)delta;
            currentSpeed = velocity.Length();
            FaceTarget(direction);
        }

        lastPosition = Position;
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        if (currentSpeed <= 0)
        {
            AnimatedSprite.Play("Idle");
        }
        else
        {
            AnimatedSprite.Play("Run");
        }
    }

    private void FaceTarget(Vector2 direction)
    {
        if (!AnimatedSprite.FlipH && direction.X < 0)
        {
            AnimatedSprite.FlipH = true;
        }
        else if (AnimatedSprite.FlipH && direction.X > 0)
        {
            AnimatedSprite.FlipH = false;
        }
    }
}
