using System;

namespace BasicShmup.Dynamics;

public record struct Speed(float FloatValue)
{
    public static implicit operator Speed(float speed) => new(speed);

    public static Velocity operator *(Speed speed, Direction direction) => speed.FloatValue * direction.VectorValue;
    public static Length operator *(Speed speed, TimeSpan timeSpan) => speed.FloatValue * (float)timeSpan.TotalSeconds;
}
