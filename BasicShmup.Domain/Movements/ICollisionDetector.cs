using BasicShmup.Domain.Dynamics;

namespace BasicShmup.Domain.Movements;

public interface ICollisionDetector
{
    MovementCollision? CastCircle(Position position, Movement movement, float radius);
}
