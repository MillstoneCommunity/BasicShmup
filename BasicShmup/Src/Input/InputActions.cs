using BasicShmup.Dynamics;
using Godot;

namespace BasicShmup.Input;

public static class InputActions
{
    private const string MoveUpActionName = "Move.Up";
    private const string MoveDownActionName = "Move.Down";
    private const string MoveLeftActionName = "Move.Left";
    private const string MoveRightActionName = "Move.Right";

    public static Direction GetMovementDirection()
    {
        var horizontalMovement = Godot.Input.GetAxis(MoveLeftActionName, MoveRightActionName);
        var verticalMovement = Godot.Input.GetAxis(MoveUpActionName, MoveDownActionName);

        var movement = new Vector2(horizontalMovement, verticalMovement);

        return new Direction(movement);
    }
}
