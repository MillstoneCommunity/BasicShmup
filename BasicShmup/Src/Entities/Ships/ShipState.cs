using System;
using Godot;

namespace BasicShmup.Entities.Ships;

public partial class ShipState : Node, IShipState
{
    private static readonly TimeSpan FiringCooldown = TimeSpan.FromSeconds(0.1f);

    private TimeSpan _remainingFiringCooldown = TimeSpan.Zero;
    private Health _health;

    public required Health Health
    {
        init => _health = value;
    }

    #region IShipState

    public bool CanFire => _remainingFiringCooldown == TimeSpan.Zero;
    public bool IsDead => _health == 0;

    public void SetFireCooldown()
    {
        _remainingFiringCooldown = FiringCooldown;
    }

    public void TakeDamage(Damage damage)
    {
        _health -= damage;
    }

    #endregion

    public override void _Process(double delta)
    {
        var deltaTime = TimeSpan.FromSeconds(delta);

        UpdateRemainingFiringCooldown(deltaTime);
    }

    private void UpdateRemainingFiringCooldown(TimeSpan deltaTime)
    {
        if (_remainingFiringCooldown < deltaTime)
            _remainingFiringCooldown = TimeSpan.Zero;
        else
            _remainingFiringCooldown -= deltaTime;
    }
}
