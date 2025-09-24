using Godot;

public partial class FloatText : Node
{
    public static FloatText Instance { get; private set; }

    private Font damageFont = (Font)GD.Load("res://Resources/pixel_font.tres");

    public override void _Ready()
    {
        base._Ready();
        Instance = this;
    }

    public void ShowDamageText(string damage, Vector2 position, Color color)
    {
        var label = new Label
        {
            ZIndex = 1000,
            Text = damage,

            LabelSettings = new LabelSettings()
            {
                Font = damageFont,
                FontSize = 10,
                FontColor = color,
                OutlineSize = 2,
                OutlineColor = Colors.Black
            },
            // Scale = new Vector2(0.12f, 0.12f),

            Position = position
        };

        var xOffset = GD.RandRange(-10, 10);
        var spawnOffset = label.Size * 0.5f;
        label.Position -= spawnOffset;
        label.Position = new Vector2(label.Position.X + xOffset, label.Position.Y);

        AddChild(label);

        var tween = CreateTween();

        // Randomly choose left or right arc
        var arcDirection = GD.Randf() < 0.5f ? -1 : 1;
        var arcDistance = GD.RandRange(20, 40) * arcDirection;

        // Move up and arc horizontally
        var targetPosition = label.Position + new Vector2(arcDistance, -30);

        tween.TweenProperty(label, "position", targetPosition, 0.4f)
            .SetTrans(Tween.TransitionType.Sine)
            .SetEase(Tween.EaseType.Out);

        // Fade out while moving
        tween.Parallel();
        tween.TweenProperty(label, "modulate:a", 0.0f, 0.35f)
            .SetDelay(0.3f)
            .SetTrans(Tween.TransitionType.Linear)
            .SetEase(Tween.EaseType.In);

        tween.TweenCallback(Callable.From(() => label.QueueFree()));
    }

}
