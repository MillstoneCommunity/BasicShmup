using BasicShmup.Domain.Entities;
using BasicShmup.Domain.Entities.Ship;
using BasicShmup.Domain.Movements;
using BasicShmup.Scenes;
using BasicShmup.ServiceProviders;

namespace BasicShmup.Entities.Ships;

public class ShipFactory(
    INodeReference<Battle> battle,
    IShipConfiguration shipConfiguration,
    ICollisionDetector collisionDetector,
    IEntityRepository repository) : IShipFactory
{
    public IShip Create()
    {
        var movementStrategy = new MoveAndSlideMovementStrategy(collisionDetector)
        {
            Radius = shipConfiguration.Radius
        };
        var ship = new Ship(movementStrategy)
        {
            Speed = shipConfiguration.Speed
        };
        repository.Add(ship);

        var view = new ShipView
        {
            Ship = ship,
            Texture = shipConfiguration.Texture
        };

        battle
            .GetNode()
            .AddChild(view);

        return ship;
    }
}
