using BasicShmup.Dynamics;
using BasicShmup.Entities.Battle;
using BasicShmup.Entities.Projectiles;
using BasicShmup.Entities.Ships.Controllers;
using BasicShmup.Events;
using Godot;

namespace BasicShmup.Entities.Ships.PowerUps;

public class DoubleShotPowerUp(IEventSender eventSender) : IPowerUp, ICannon
{
    private const float AbsoluteProjectileOffset = 50;
    private static readonly Displacement ProjectileOffset1 = new Vector2(0, AbsoluteProjectileOffset / 2);
    private static readonly Displacement ProjectileOffset2 = new Vector2(0, -AbsoluteProjectileOffset / 2);

    public void Apply(IPowerUpShip ship)
    {
        ship.SetCannon(this);
    }

    public void FireProjectile(IController controller, Position projectilePosition)
    {
        var projectile1 = CreateProjectile(controller, projectilePosition + ProjectileOffset1);
        eventSender.Send(new SpawnBattleNodeEvent(projectile1));

        var projectile2 = CreateProjectile(controller, projectilePosition + ProjectileOffset2);
        eventSender.Send(new SpawnBattleNodeEvent(projectile2));
    }

    private static Projectile CreateProjectile(IController controller, Position projectilePosition)
    {
        return new Projectile
        {
            SourceController = controller,
            Position = projectilePosition.VectorValue,
            MovementDirection = Direction.Right
        };
    }
}
