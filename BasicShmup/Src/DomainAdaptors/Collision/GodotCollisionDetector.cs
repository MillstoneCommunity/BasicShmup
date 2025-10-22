using BasicShmup.Domain.Dynamics;
using BasicShmup.Domain.Movements;
using BasicShmup.ServiceProviders;

namespace BasicShmup.DomainAdaptors.Collision;

public class GodotCollisionDetector(INodeReference<CircleCollider> circleCollider) : ICollisionDetector
{
    public MovementCollision? CastCircle(Position position, Movement movement, float radius)
    {
        return circleCollider.GetNode().CastCircle(position, movement, radius);
    }
}
