using System;

namespace BasicShmup.Entities.Ships;

public readonly record struct Health(int Amount)
{
    public static implicit operator Health(int amount) => new(amount);

    public static Health operator -(Health health, Damage damage)
    {
        var newUnboundedHealth = health.Amount - damage.Amount;
        return Math.Max(newUnboundedHealth, 0);
    }
}