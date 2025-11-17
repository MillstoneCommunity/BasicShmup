using System;
using BasicShmup.Dynamics;
using BasicShmup.Entities.Battle;
using BasicShmup.Entities.Projectiles;
using BasicShmup.Entities.Ships.Controllers;
using BasicShmup.Events;

namespace BasicShmup.Entities.Ships;

public class Ship(IShipConfiguration shipConfiguration, IEventSender eventSender) : IShip
{
    private static readonly TimeSpan FiringCooldown = TimeSpan.FromSeconds(0.1f);

    private TimeSpan _remainingFiringCooldown = TimeSpan.Zero;
    private Health _health = shipConfiguration.Health;

    private bool CanFire => _remainingFiringCooldown == TimeSpan.Zero;

    public bool IsDead => _health == 0;
    public Position Position { get; set; }

    public void Update(TimeSpan deltaTime)
    {
        if (_remainingFiringCooldown < deltaTime)
            _remainingFiringCooldown = TimeSpan.Zero;
        else
            _remainingFiringCooldown -= deltaTime;
    }

    public void FireProjectile(IController controller)
    {
        if (!CanFire)
            return;

        SetFireCooldown();

        var projectile = new Projectile
        {
            SourceController = controller,
            Position = Position.VectorValue,
            MovementDirection = Direction.Right
        };

        eventSender.Send(new SpawnBattleNodeEvent(projectile));
    }

    private void SetFireCooldown()
    {
        _remainingFiringCooldown = FiringCooldown;
    }

    public void TakeDamage(Damage damage)
    {
        _health -= damage;
    }
}
