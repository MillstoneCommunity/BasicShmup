using System.Numerics;

namespace BasicShmup.Domain.Dynamics;

public static class VectorExtensions
{
    public static Vector2 ToVector(this Movement movement) => new(movement.DeltaX, movement.DeltaY);

    public static Vector2 Normalized(this Vector2 vector) => Vector2.Normalize(vector);
}
