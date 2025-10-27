using BasicShmup.Dynamics;
using BasicShmup.Events;
using BasicShmup.Input;
using Godot;

namespace BasicShmup.Entities.Ships.Controllers;

public partial class PlayerController : Node2D, IController, IEventHandler<ShipCollisionEvent>
{
    private CharacterBody2D _body = new()
    {
        MotionMode = CharacterBody2D.MotionModeEnum.Floating
    };

    private CollisionShape2D _collisionShape = new();

    public required IShip Ship { get; init; }
    public required IShipConfiguration ShipConfiguration { get; init; }

    public PlayerController()
    {
        _body.AddChild(_collisionShape);
        AddChild(_body);
    }

    public override void _Ready()
    {
        _collisionShape.Shape = new CircleShape2D
        {
            Radius = ShipConfiguration.ColliderRadius
        };
    }

    public override void _Process(double delta)
    {
        if (InputActions.IsFiring)
            Ship.FireProjectile();
    }

    public override void _PhysicsProcess(double delta)
    {
        _collisionShape.Disabled = false;
        _body.Position = Vector2.Zero;

        var movementDirection = InputActions.GetMovementDirection();
        _body.Velocity = (ShipConfiguration.Speed * movementDirection).VectorValue;
        _body.MoveAndSlide();

        _collisionShape.Disabled = true;

        Ship.Position += new Displacement(_body.Position);
    }

    public void Handle(ShipCollisionEvent shipCollisionEvent)
    {
        Ship.TakeDamage(shipCollisionEvent.Damage);
    }
}
