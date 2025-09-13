namespace BasicShmup.Domain.Dynamics;

public record struct Speed(float Value)
{
    public static implicit operator Speed(float speedValue) => new(speedValue);

    public static Distance operator *(Speed speed, DeltaTime deltaTime) => new(speed.Value * deltaTime.Seconds);
}
