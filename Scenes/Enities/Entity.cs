using System;
using Godot;

public partial class Entity : Node2D
{
    [Export]
    public AnimatedSprite2D AnimatedSprite { get; private set; }
    [Export]
    public float MaxHealth { get; private set; } = 50f;

    private AnimationWrapper currentAnimation;
    private bool isDead = false;

    public float TurningCooldown { get; set; } = 0.0f;
    public float Health { get; protected set; }

    public override void _Ready()
    {
        base._Ready();
        AnimatedSprite.AnimationFinished += OnAnimationFinished;
        Health = MaxHealth;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        AnimatedSprite.AnimationFinished -= OnAnimationFinished;
    }

    public void ApplyDamage(float damage)
    {
        if (isDead) { return; }

        Health -= damage;
        Health = Math.Max(0, Health);
        ShowDamageTakenEffectAsync();
        ShowDamageText(damage);

        if (Health <= 0)
        {

            isDead = true;
            GD.Print($"{Name} has died.");
        }
    }

    public virtual void PlayAnimation(AnimationWrapper animation)
    {
        if (AnimatedSprite.Animation == animation.Name)
            return;

        if (currentAnimation != null && currentAnimation.IsHightPriority && !animation.IsHightPriority)
            return;

        currentAnimation = animation;
        AnimatedSprite.Play(animation.Name);
    }

    private void OnAnimationFinished()
    {
        if (currentAnimation != null)
        {
            currentAnimation = null;
        }
    }

    private void ShowDamageText(float damage)
    {
        var animation = AnimatedSprite.Animation;
        var FrameTexture = AnimatedSprite.SpriteFrames.GetFrameTexture(animation, 0);
        var height = FrameTexture.GetHeight();
        var spawnPosition = new Vector2(Position.X, Position.Y - (height * 0.5f));

        FloatText.Instance.ShowDamageText(damage.ToString(), spawnPosition, Colors.White);
    }

    public void TurnToPosition(Vector2 targetPosition)
    {
        if (Position.X > targetPosition.X && !AnimatedSprite.FlipH)
        {
            AnimatedSprite.FlipH = true;
        }
        else if (Position.X < targetPosition.X && AnimatedSprite.FlipH)
        {
            AnimatedSprite.FlipH = false;
        }
    }

    public virtual void ShowDamageTakenEffectAsync() { }

}
