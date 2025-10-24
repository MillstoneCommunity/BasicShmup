using System;
using Godot;

namespace BasicShmup.Entities.Ships;

public partial class ShipState : Node, IShipState
{
    private static readonly TimeSpan FiringCooldown = TimeSpan.FromSeconds(0.5f);

    private TimeSpan _remainingFiringCooldown = TimeSpan.Zero;

    public bool CanFire => _remainingFiringCooldown == TimeSpan.Zero;

    public void SetFireCooldown()
    {
        _remainingFiringCooldown = FiringCooldown;
    }

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
