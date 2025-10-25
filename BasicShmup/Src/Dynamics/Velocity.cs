using Godot;

namespace BasicShmup.Dynamics;

public record struct Velocity(Vector2 VectorValue)
{
    public static implicit operator Velocity(Vector2 vectorValue) => new(vectorValue);
}