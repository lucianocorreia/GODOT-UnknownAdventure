using Godot;
using System;

namespace Scenes;

public partial class Player : Entity
{
    const float SPEED = 100.0f;

    [Export]
    public AnimatedSprite2D AnimatedSprite { get; private set; }

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

        var horizontal = Input.GetAxis("Left", "Right");
        var vertical = Input.GetAxis("Up", "Down");

        var moviment = new Vector2((float)horizontal, (float)vertical);
        var normalized = moviment.Normalized();

        Position += normalized * SPEED * (float)delta;

        if (normalized.Length() > 0)
        {
            isMoving = true;
            if (horizontal > 0)
            {
                AnimatedSprite.FlipH = false;
                // WeaponSprite.Position = weaponRightPosition;
                // WeaponSprite.Rotation = Mathf.DegToRad(35);
            }
            else if (horizontal < 0)
            {
                AnimatedSprite.FlipH = true;
                // WeaponSprite.Position = weaponLeftPosition;
                // WeaponSprite.Rotation = Mathf.DegToRad(-35);
            }

        }
    }

    private void HandleAnimation()
    {
        if (isMoving)
        {
            AnimatedSprite.Play("Run");
        }
        else
        {
            AnimatedSprite.Play("Idle");
        }
    }

}
