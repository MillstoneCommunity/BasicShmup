using BasicShmup.Domain.Dynamics;

namespace BasicShmup.Domain.Entities.Ship;

public interface IShip
{
    Direction MovementDirection { set; }
    Position Position { get; }
}
