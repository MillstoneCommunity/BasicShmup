using Godot;

namespace BasicShmup.Input;

public static class InputActions
{
    private const float Tolerance = 0.01f;

    private const string MoveUpActionName = "Move.Up";
    private const string MoveDownActionName = "Move.Down";
    private const string MoveLeftActionName = "Move.Left";
    private const string MoveRightActionName = "Move.Right";

    public static Vector2 GetClampedMovement()
    {
        var horizontalMovement = Godot.Input.GetAxis(MoveLeftActionName, MoveRightActionName);
        var verticalMovement = Godot.Input.GetAxis(MoveUpActionName, MoveDownActionName);

        var movement = new Vector2(horizontalMovement, verticalMovement);

        var movementLength = movement.Length();
        if (movementLength < Tolerance)
            return Vector2.Zero;

        if (movementLength <= 1)
            return movement;

        return movement / movementLength;
    }
}
