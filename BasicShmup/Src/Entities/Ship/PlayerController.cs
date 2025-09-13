using BasicShmup.Domain.Entities.Ship;
using BasicShmup.Extensions;
using BasicShmup.Input;
using Godot;

namespace BasicShmup.Entities.Ship;

public partial class PlayerController : Node
{
    public required IShip Ship { get; init; }

    public override void _Process(double _)
    {
        var movement = InputActions.GetClampedMovement();
        Ship.MovementDirection = movement.ToMovement();
    }
}
