using Godot;
using System;
using System.Threading.Tasks;

public partial class SlashEffect : AbilityManifest
{
    public static bool FlipVSlash = false;

    [Export]
    public AnimatedSprite2D AnimatedSprite { get; private set; }

    [Export]
    public float RotationOffset { get; private set; } = 45.0f;

    private Node2D cloneWeapon;

    public override void _Ready()
    {
        base._Ready();

        if (AnimatedSprite != null)
        {
            AnimatedSprite.AnimationFinished += OnAnimationFinished;
            FlipVSlash = !FlipVSlash;
            AnimatedSprite.FlipV = FlipVSlash;
        }
    }

    public override void Activate(AbilityContext context)
    {
        base.Activate(context);
        var mousePosition = GetViewport().GetCamera2D().GetGlobalMousePosition();
        LookAt(mousePosition);

        var weapon = context.Caster.GetNodeOrNull<Sprite2D>("Weapon");

        if (weapon != null)
        {
            weapon.Hide();
            var baseAngle = (mousePosition - context.Caster.GlobalPosition).Angle();
            var offsetRad = Mathf.DegToRad(RotationOffset);

            if (FlipVSlash) { offsetRad = -offsetRad; }

            var weaponAngle = baseAngle + offsetRad;
            var weaponDirection = new Vector2(Mathf.Cos(weaponAngle), Mathf.Sin(weaponAngle));

            cloneWeapon = weapon.Duplicate() as Node2D;
            cloneWeapon.Show();
            context.Caster.AddChild(cloneWeapon);

            weapon.GlobalPosition = context.Caster.GlobalPosition + weaponDirection * 15.0f;

            weapon.Rotation = weaponAngle + (Mathf.Pi / 2);
        }
    }

    private void OnAnimationFinished()
    {
        Hide();
        if (cloneWeapon != null)
        {
            // await ToSignal(GetTree().CreateTimer(0.1f), "timeout");
            cloneWeapon.QueueFree();
        }
        QueueFree();
    }

}
