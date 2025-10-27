using System;
using BasicShmup.Dynamics;
using BasicShmup.Entities.Projectiles;
using BasicShmup.Events;
using Godot;

namespace BasicShmup.Entities.Ships.Controllers;

public partial class EnemyController : Node, IController, IEventHandler<ProjectileCollisionEvent>
{
    public required IShip Ship { get; init; }
    public required IShipConfiguration ShipConfiguration { get; init; }

    public override void _PhysicsProcess(double delta)
    {
        var deltaTime = TimeSpan.FromSeconds(delta);
        Ship.Position += ShipConfiguration.Speed * deltaTime * Direction.Left;
    }

    public void Handle(ProjectileCollisionEvent projectileCollisionEvent)
    {
        Ship.TakeDamage(projectileCollisionEvent.Damage);
    }
}
