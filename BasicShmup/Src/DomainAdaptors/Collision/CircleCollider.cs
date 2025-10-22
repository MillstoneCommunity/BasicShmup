using BasicShmup.Domain.Dynamics;
using BasicShmup.Domain.Movements;
using BasicShmup.Extensions;
using BasicShmup.ServiceProviders;
using Godot;

namespace BasicShmup.DomainAdaptors.Collision;

[NodeService]
public partial class CircleCollider : ShapeCast2D
{
    private readonly CircleShape2D _circle = new()
    {
        Radius = 0
    };

    public CircleCollider()
    {
        Enabled = false;
        Shape = _circle;
    }

    public MovementCollision? CastCircle(Position position, Movement movement, float radius)
    {
        Position = position.ToGodotVector();
        TargetPosition = movement.ToGodotVector();
        _circle.Radius = radius;

        ForceShapecastUpdate();

        if (GetCollisionCount() == 0)
            return null;

        var safeProgress = GetClosestCollisionSafeFraction();
        var normal = GetCollisionNormal(0);

        return new MovementCollision(safeProgress, normal.ToDomainVector());
    }
}
