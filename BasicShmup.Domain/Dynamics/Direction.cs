namespace BasicShmup.Domain.Dynamics;

public record struct Direction(float X, float Y)
{
    public static Movement operator *(Direction direction, Distance distance) =>
        new(direction.X * distance.Value, direction.Y * distance.Value);
}
