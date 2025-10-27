using Godot;

namespace BasicShmup.Dynamics;

public record struct Displacement(Vector2 VectorValue)
{
    public static implicit operator Displacement(Vector2 displacement) => new(displacement);

    public static Position operator +(Position position, Displacement displacement) =>
        position.VectorValue + displacement.VectorValue;
}
