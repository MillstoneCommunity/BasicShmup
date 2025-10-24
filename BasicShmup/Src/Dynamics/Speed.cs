namespace BasicShmup.Dynamics;

public record struct Speed(float FloatValue)
{
    public static implicit operator Speed(float speedValue) => new(speedValue);

    public static Distance operator *(Speed speed, Direction direction) => speed.FloatValue * direction.VectorValue;
}
