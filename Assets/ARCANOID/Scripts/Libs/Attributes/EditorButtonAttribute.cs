using System;

[AttributeUsage(AttributeTargets.Method)]
public class EditorButtonAttribute : Attribute
{
    public readonly string Name;

    public EditorButtonAttribute(string name)
    {
        Name = name;
    }
}