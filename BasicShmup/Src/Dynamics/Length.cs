using Godot;

namespace BasicShmup.Dynamics;

public record struct Length(float FloatValue)
{
    public static implicit operator Length(float length) => new(length);

    public static Displacement operator *(Length length, Direction direction) => length.FloatValue * direction.VectorValue;
}

public record struct Displacement(Vector2 VectorValue)
{
    public static implicit operator Displacement(Vector2 displacement) => new(displacement);
}
