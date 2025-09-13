using BasicShmup.Domain.Dynamics;

namespace BasicShmup.Domain.Movements;

public interface IMovementStrategy
{
    Position Move(Position position, Movement movement);
}
