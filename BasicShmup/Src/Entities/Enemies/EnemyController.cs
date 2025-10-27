using System;
using BasicShmup.Dynamics;
using BasicShmup.Entities.Ships;
using BasicShmup.ServiceProviders;
using Godot;

namespace BasicShmup.Entities.Enemies;

public partial class EnemyController : Node
{
    // todo make public required property
    [Inject]
    private readonly IShipConfiguration _shipConfiguration = null!;

    public required IShip Ship { get; init; }

    public override void _PhysicsProcess(double delta)
    {
        var deltaTime = TimeSpan.FromSeconds(delta);
        Ship.Position += _shipConfiguration.Speed * deltaTime * Direction.Left;
    }
}
