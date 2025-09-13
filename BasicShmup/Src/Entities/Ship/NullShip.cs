using BasicShmup.Domain.Dynamics;
using BasicShmup.Domain.Entities.Ship;

namespace BasicShmup.Entities.Ship;

public class NullShip : IShip
{
    public static IShip Instance { get; } = new NullShip();

    public Direction MovementDirection { set { } }
    public Position Position => Position.Zero;

    private NullShip() { }
}
