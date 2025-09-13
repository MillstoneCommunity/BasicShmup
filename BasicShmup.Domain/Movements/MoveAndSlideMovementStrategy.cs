using System.Numerics;
using BasicShmup.Domain.Dynamics;

namespace BasicShmup.Domain.Movements;

public class MoveAndSlideMovementStrategy(ICollisionDetector collisionDetector) : IMovementStrategy
{
    private const int MaxCollisions = 32;

    public required float Radius { get; init; }

    public Position Move(Position position, Movement movement)
    {
        var collisionCount = 0;

        while (collisionCount < MaxCollisions)
        {
            var collision = collisionDetector.CastCircle(position, movement, Radius);
            if (collision == null)
                return position + movement;

            var movementWithBuffer = GetMovementCloseToCollision(movement, collision.MovementFraction);
            position += movementWithBuffer;

            var hasMoveAlmostFullMovement = collision.MovementFraction >= 1 - FloatConstants.Tolerance;
            if (hasMoveAlmostFullMovement)
                return position;

            movement = GetMovementAfterCollision(movement, collision);
            if (movement == Movement.Zero)
                return position;

            collisionCount++;
        }

        return position;
    }

    private static Movement GetMovementCloseToCollision(Movement movement, float progressToCollision)
    {
        var movementToCollisionVector = (movement * progressToCollision).ToVector();
        var distanceToCollision = movementToCollisionVector.Length();
        if (distanceToCollision <= FloatConstants.Tolerance)
            return Movement.Zero;

        var movementBuffer = movementToCollisionVector / distanceToCollision * FloatConstants.Tolerance;
        var movementWithBuffer = movementToCollisionVector - movementBuffer;

        return new Movement(movementWithBuffer);
    }

    private static Movement GetMovementAfterCollision(Movement movement, MovementCollision collision)
    {
        var movementVector = movement.ToVector();
        var movementAlongNormal = Vector2.Dot(movementVector, collision.Normal) * collision.Normal;
        var movementPerpendicularToNormal = movementVector - movementAlongNormal;

        var perpendicularLength = movementPerpendicularToNormal.Length();
        if (perpendicularLength <= FloatConstants.Tolerance)
            return Movement.Zero;

        var newMovementDirection = Vector2.Normalize(movementPerpendicularToNormal);
        var remainingMovement = movementVector.Length() * (1 - collision.MovementFraction);

        return new Movement(newMovementDirection * remainingMovement);
    }
}
