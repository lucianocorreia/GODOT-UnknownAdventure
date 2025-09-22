using System;
using Godot;

public partial class Entity : Node2D
{
    [Export]
    public AnimatedSprite2D AnimatedSprite { get; private set; }

    private AnimationWrapper currentAnimation;

    public override void _Ready()
    {
        base._Ready();
        AnimatedSprite.AnimationFinished += OnAnimationFinished;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        AnimatedSprite.AnimationFinished -= OnAnimationFinished;
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

}
