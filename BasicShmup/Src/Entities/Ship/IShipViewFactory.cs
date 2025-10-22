using BasicShmup.Domain.Entities.Ship;

namespace BasicShmup.Entities.Ship;

public interface IShipViewFactory // todo remove
{
    ShipView Create(IShip ship);
}
