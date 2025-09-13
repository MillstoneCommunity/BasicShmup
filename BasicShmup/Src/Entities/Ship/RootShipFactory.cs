using BasicShmup.Domain.Entities;
using BasicShmup.Domain.Entities.Ship;
using BasicShmup.Domain.Movements;
using BasicShmup.Scenes;
using BasicShmup.Scenes.SceneConfigurations;

namespace BasicShmup.Entities.Ship;

public class RootShipFactory(
    INodeReference<Battle> battle,
    IShipConfiguration shipConfiguration,
    IShipViewFactory shipViewFactory,
    ICollisionDetector collisionDetector,
    IEntityRepository repository) : IShipFactory
{
    public IShip Create()
    {
        var movementStrategy = new MoveAndSlideMovementStrategy(collisionDetector)
        {
            Radius = shipConfiguration.Radius
        };
        var ship = new Domain.Entities.Ship.Ship(movementStrategy)
        {
            Speed = shipConfiguration.Speed
        };
        repository.Add(ship);

        var view = shipViewFactory.Create(ship);

        battle
            .GetNode()
            .AddChild(view);

        return ship;
    }
}
