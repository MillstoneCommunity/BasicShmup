using BasicShmup.Domain.Dynamics;
using Godot;

namespace BasicShmup.Extensions;

public static class VectorExtensions
{
    public static Direction ToMovement(this Vector2 vector) => new(vector.X, vector.Y);
    public static System.Numerics.Vector2 ToDomainVector(this Vector2 vector) => new(vector.X, vector.Y);

    public static Vector2 ToGodotVector(this Position position) => new(position.X, position.Y);
    public static Vector2 ToGodotVector(this Movement movement) => new(movement.DeltaX, movement.DeltaY);
}
