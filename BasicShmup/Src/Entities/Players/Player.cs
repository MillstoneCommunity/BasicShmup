using BasicShmup.Entities.Players.Controllers;
using BasicShmup.Entities.Projectiles;
using BasicShmup.Entities.Ships;
using BasicShmup.Events;
using Godot;

namespace BasicShmup.Entities.Players;

[GlobalClass]
public partial class Player : Node, IEntity, IEventHandler<ProjectileHitEvent>
{
    private readonly Ship _ship;

    [Export]
    private Vector2 Position
    {
        get => _ship.Position;
        set => _ship.Position = value;
    }

    public Player()
    {
        _ship = new Ship
        {
            RootEntity = this
        };
        AddChild(_ship);

        var playerController = new PlayerController
        {
            Ship = _ship
        };
        AddChild(playerController);
    }

    public void Handle(ProjectileHitEvent projectileHitEvent)
    {
        GD.Print("Player was hit");
    }
}
