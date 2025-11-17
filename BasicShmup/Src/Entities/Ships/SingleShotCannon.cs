using BasicShmup.Dynamics;
using BasicShmup.Entities.Battle;
using BasicShmup.Entities.Projectiles;
using BasicShmup.Entities.Ships.Controllers;
using BasicShmup.Events;

namespace BasicShmup.Entities.Ships;

public class SingleShotCannon(IEventSender eventSender) : ICannon
{
    public void FireProjectile(IController controller, Position projectilePosition)
    {
        var projectile = new Projectile
        {
            SourceController = controller,
            Position = projectilePosition.VectorValue,
            MovementDirection = Direction.Right
        };

        eventSender.Send(new SpawnBattleNodeEvent(projectile));
    }
}