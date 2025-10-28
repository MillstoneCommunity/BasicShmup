namespace BasicShmup.Dynamics;

public record struct Length(float FloatValue)
{
    public static implicit operator Length(float length) => new(length);

    public static Displacement operator *(Length length, Direction direction) => length.FloatValue * direction.VectorValue;
}
