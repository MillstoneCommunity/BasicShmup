using BasicShmup.Dynamics;
using Godot;

namespace BasicShmup.Inputs;

public static class InputActions
{
    private const string MoveUpActionName = "Move.Up";
    private const string MoveDownActionName = "Move.Down";
    private const string MoveLeftActionName = "Move.Left";
    private const string MoveRightActionName = "Move.Right";
    private const string FireActionName = "Fire";


    public static bool IsFiring => Input.IsActionPressed(FireActionName);
    public static bool AddPowerUp => Input.IsKeyPressed(Key.Key1);

    public static Direction GetMovementDirection()
    {
        var horizontalMovement = Input.GetAxis(MoveLeftActionName, MoveRightActionName);
        var verticalMovement = Input.GetAxis(MoveUpActionName, MoveDownActionName);

        var movement = new Vector2(horizontalMovement, verticalMovement);

        return new Direction(movement);
    }
}
