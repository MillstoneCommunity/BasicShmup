using BasicShmup.Domain.Dynamics;
using BasicShmup.Domain.Movements;

namespace BasicShmup.Domain.Entities.Ship;

public class Ship(IMovementStrategy movementStrategy) : IShip, IEntity
{
    public required Speed Speed { get; init; }
    public Direction MovementDirection { get; set; }
    public Position Position { get; private set; }

    public void Move(DeltaTime deltaTime)
    {
        var movement = MovementDirection * (Speed * deltaTime);
        Position = movementStrategy.Move(Position, movement);
    }
}
