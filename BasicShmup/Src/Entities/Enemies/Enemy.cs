using BasicShmup.Entities.Projectiles;
using BasicShmup.Entities.Ships;
using BasicShmup.Events;
using Godot;

namespace BasicShmup.Entities.Enemies;

[GlobalClass]
public partial class Enemy : Node, IEntity, IEventHandler<ProjectileHitEvent>
{
    private readonly Ship _ship;

    [Export]
    public Vector2 Position
    {
        get => _ship.Position;
        set => _ship.Position = value;
    }

    public Enemy()
    {
        _ship = new Ship
        {
            RootEntity = this
        };
        AddChild(_ship);

        var controller = new EnemyController
        {
            Ship = _ship
        };
        AddChild(controller);
    }

    public void Handle(ProjectileHitEvent projectileHitEvent)
    {
        _ship.TakeDamage(1);

        if (_ship.IsDead)
            QueueFree();
    }
}
