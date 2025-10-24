using Godot;

namespace BasicShmup.Dynamics;

public record struct Distance(Vector2 VectorValue)
{
    public static implicit operator Distance(Vector2 vectorValue) => new(vectorValue);
}