using BasicShmup.Entities.Projectiles;
using BasicShmup.Entities.Ships;
using BasicShmup.Events;
using Godot;

namespace BasicShmup.Entities.Enemies;

[GlobalClass]
public partial class Enemy : Node, IEntity, IEventHandler<ProjectileHitEvent>
{
    private readonly Ship _ship;

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
        _ship.TakeDamage(projectileHitEvent.Damage);

        if (_ship.IsDead)
            QueueFree();
    }
}
