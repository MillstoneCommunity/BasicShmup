using Godot;

namespace BasicShmup.Dynamics;

public readonly record struct Position(Vector2 VectorValue)
{
    public static implicit operator Position(Vector2 position) => new(position);
}