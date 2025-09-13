using System.Numerics;

namespace BasicShmup.Domain.Dynamics;

public record struct Movement(float DeltaX, float DeltaY)
{
    public static readonly Movement Zero = new(0, 0);

    public static Movement operator *(Movement movement, float scalar) =>
        new(movement.DeltaX * scalar, movement.DeltaY * scalar);

    public Movement(Vector2 movement) : this(movement.X, movement.Y) { }
}
