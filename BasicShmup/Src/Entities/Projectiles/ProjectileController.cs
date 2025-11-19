using System;
using BasicShmup.Dynamics;
using Godot;

namespace BasicShmup.Entities.Projectiles;

public partial class ProjectileController : Node
{
    public required Node2D MovementRoot { get; init; }
    public required Direction MovementDirection { get; init; }
    public required Speed Speed { get; init; }

    public override void _PhysicsProcess(double delta)
    {
        var deltaTime = TimeSpan.FromSeconds(delta);
        var deltaMovement = Speed * deltaTime * MovementDirection;

        MovementRoot.Position += deltaMovement.VectorValue;
    }
}
