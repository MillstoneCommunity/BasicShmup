using Godot;

namespace BasicShmup.Dynamics;

public record struct Speed(float Value)
{
    public static implicit operator Speed(float speedValue) => new(speedValue);

    public static Vector2 operator *(Speed speed, Vector2 vector) => speed.Value * vector;
}
