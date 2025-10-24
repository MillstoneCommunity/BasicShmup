using BasicShmup.Entities.Ships;
using BasicShmup.Input;
using Godot;

namespace BasicShmup.Entities.Players.Controllers;

public partial class PlayerController : Node
{
    public required IShip Ship { get; init; }

    public override void _PhysicsProcess(double delta)
    {
        var movementDirection = InputActions.GetMovementDirection();
        Ship.Move(movementDirection);
    }
}
