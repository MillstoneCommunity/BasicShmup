using System;
using BasicShmup.Dynamics;
using BasicShmup.Entities.Ships.Controllers;
using BasicShmup.Entities.Ships.PowerUps;
using BasicShmup.Events;

namespace BasicShmup.Entities.Ships;

public class Ship(IShipConfiguration shipConfiguration, IEventSender eventSender) : IShip, IPowerUpShip
{
    private TimeSpan _remainingFiringCooldown = TimeSpan.Zero;
    private Health _health = shipConfiguration.Health;

    private ICannon _cannon = new SingleShotCannon(eventSender);

    private bool CanFire => _remainingFiringCooldown == TimeSpan.Zero;

    #region IShip

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
        _cannon.FireProjectile(controller, Position);
    }

    private void SetFireCooldown()
    {
        _remainingFiringCooldown = shipConfiguration.FiringCooldown;
    }

    public void TakeDamage(Damage damage)
    {
        _health -= damage;
    }

    public void AddPowerUp(IPowerUp powerUp)
    {
        powerUp.Apply(this);
    }

    #endregion

    #region IPowerUpShip

    public void SetCannon(ICannon cannon)
    {
        _cannon = cannon;
    }

    #endregion
}
