using Godot;
using System;
using System.Threading.Tasks;

namespace Scenes;

public partial class Player : Entity
{
    const float SPEED = 100.0f;

    [Export]
    public AbilityController AbilityController { get; private set; }

    [Export]
    public Sprite2D WeaponSprite { get; private set; }

    private bool isMoving = false;
    private Vector2 weaponRightPosition;
    private Vector2 weaponLeftPosition;

    public override void _Ready()
    {
        base._Ready();
        // weaponRightPosition = WeaponSprite.Position;
        // weaponLeftPosition = Position + (Position - WeaponSprite.Position);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        HandleMovement(delta);
        HandleAbilities();
        HandleAnimation();
    }

    public override async void ShowDamageTakenEffectAsync()
    {
        if (AnimatedSprite.Material == null) { return; }

        GD.Print("Player took damage effect");
        for (int i = 0; i < 2; i++)
        {
            AnimatedSprite.Material.Set("shader_parameter/is_hurt", true);
            await ToSignal(GetTree().CreateTimer(0.05f), "timeout");
            AnimatedSprite.Material.Set("shader_parameter/is_hurt", false);
            await ToSignal(GetTree().CreateTimer(0.05f), "timeout");
        }
    }

    private void HandleAbilities()
    {
        if (Input.IsActionJustPressed("Ability1"))
        {
            AbilityController.TriggerAbilityByIndex(0);
        }
        if (Input.IsActionJustPressed("Ability2"))
        {
            AbilityController.TriggerAbilityByIndex(1);
        }
        if (Input.IsActionJustPressed("Ability3"))
        {
            AbilityController.TriggerAbilityByIndex(2);
        }
    }

    private void HandleMovement(double delta)
    {
        isMoving = false;

        TurningCooldown = Mathf.Max(0, TurningCooldown - (float)delta);

        var horizontal = Input.GetAxis("Left", "Right");
        var vertical = Input.GetAxis("Up", "Down");

        var moviment = new Vector2((float)horizontal, (float)vertical);
        var normalized = moviment.Normalized();

        Position += normalized * SPEED * (float)delta;

        if (normalized.Length() > 0)
        {
            isMoving = true;

            if (TurningCooldown == 0)
            {
                if (horizontal > 0)
                {
                    AnimatedSprite.FlipH = false;
                }
                else if (horizontal < 0)
                {
                    AnimatedSprite.FlipH = true;
                }
            }

        }
    }

    private void HandleAnimation()
    {
        if (isMoving)
        {
            PlayAnimation(new AnimationWrapper("Run"));
        }
        else
        {
            PlayAnimation(new AnimationWrapper("Idle"));
        }
    }

}
