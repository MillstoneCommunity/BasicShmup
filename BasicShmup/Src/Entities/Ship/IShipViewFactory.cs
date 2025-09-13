using BasicShmup.Domain.Entities.Ship;

namespace BasicShmup.Entities.Ship;

public interface IShipViewFactory
{
    ShipView Create(IShip ship);
}
