using System.Numerics;
using BasicShmup.Domain.Dynamics;
using BasicShmup.Domain.Movements;
using BasicShmup.Test.Unit.Extensions;
using NSubstitute;

namespace BasicShmup.Test.Unit.Tests;

public class MoveAndSlideMovementStrategyTests
{
    private readonly ICollisionDetector _collisionDetector = Substitute.For<ICollisionDetector>();
    private readonly IMovementStrategy _movementStrategy;

    private readonly float _radius = 1;
    private readonly Position _position = Position.Zero;
    private readonly Movement _movement = new(1, 0);

    public MoveAndSlideMovementStrategyTests()
    {
        _movementStrategy = new MoveAndSlideMovementStrategy(_collisionDetector)
        {
            Radius = _radius
        };

        _collisionDetector
            .CastCircle(_position, _movement, _radius)
            .Returns((MovementCollision?)null);
    }

    [Fact]
    public void Move_WhenNoCollisionIsDetected_ShouldReturnPositionPlusMovement()
    {
        var actualPosition = _movementStrategy.Move(_position, _movement);

        var expectedPosition = _position + _movement;
        actualPosition.ShouldBeCloseTo(expectedPosition);
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(0.25f)]
    [InlineData(0.50f)]
    [InlineData(0.75f)]
    [InlineData(1f)]
    public void Move_WithCollisionAndNoSliding_ShouldMoveMovementFraction(float movementFraction)
    {
        var normalWithNoSliding = -_movement.ToVector().Normalized();
        _collisionDetector
            .CastCircle(_position, _movement, _radius)
            .Returns(new MovementCollision(movementFraction, normalWithNoSliding));

        var actualPosition = _movementStrategy.Move(_position, _movement);

        var expectedPosition = _position + _movement * movementFraction;
        actualPosition.ShouldBeCloseTo(expectedPosition);
    }

    [Fact]
    public void Move_WithCollisionAndSliding_ShouldMoveParallelToNormal()
    {
        const float movementFraction = 0.5f;
        var movement = new Movement(4, 0);
        var normalWithNoSliding = new Vector2(-1, 1).Normalized();
        _collisionDetector
            .CastCircle(_position, movement, _radius)
            .Returns(new MovementCollision(movementFraction, normalWithNoSliding), null);

        var actualPosition = _movementStrategy.Move(_position, movement);

        var expectedMovementUntilCollision = movement * movementFraction;
        var expectedMovementAfterCollisionVector = new Vector2(1, 1).Normalized() * movement.ToVector().Length() * (1 - movementFraction);
        var expectedMovementAfterCollision = new Movement(expectedMovementAfterCollisionVector);
        var expectedPosition = _position + expectedMovementUntilCollision + expectedMovementAfterCollision;
        actualPosition.ShouldBeCloseTo(expectedPosition);
    }

    [Fact]
    public void Move_WhenContinuedCollision_ShouldTerminateBeforeMovingFullLength()
    {
        _collisionDetector
            .CastCircle(Arg.Any<Position>(), Arg.Any<Movement>(), _radius)
            .Returns(new MovementCollision(0.1f, new Vector2(0, 1)));

        var actualPosition = _movementStrategy.Move(_position, _movement);

        actualPosition.ShouldNotBeCloseTo(_position + _movement);
    }
}
