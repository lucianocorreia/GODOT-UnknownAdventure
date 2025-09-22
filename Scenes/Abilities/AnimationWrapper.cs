using Godot;

public partial class AnimationWrapper : RefCounted
{
    public string Name { get; set; } = string.Empty;
    public bool IsHightPriority { get; set; } = false;

    public AnimationWrapper(string name, bool isHightPriority = false)
    {
        Name = name;
        IsHightPriority = isHightPriority;
    }

}
