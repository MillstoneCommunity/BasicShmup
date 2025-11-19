using System.Linq;
using BasicShmup.Entities.Ships.Controllers;
using BasicShmup.Events;
using BasicShmup.Extensions;
using BasicShmup.ServiceProviders;
using Godot;

namespace BasicShmup.Entities.Ships;

[GlobalClass]
public partial class ShipView : Node2D
{
    [Inject]
    private readonly IEventSender _eventSender = null!;

    [Inject]
    private readonly IShipConfiguration _shipConfiguration = null!;

    [Export]
    private ControllerType _controllerType;

    private IShip _shipModel = null!;
    private IController _controller = null!;

    public override void _Ready()
    {
        _shipModel = new Ship(_shipConfiguration, _eventSender)
        {
            Position = Position
        };

        var controllerNode = CreateController(_shipConfiguration, _shipModel, _controllerType, out _controller);
        AddChild(controllerNode);

        var collider = CreateCollider(_shipConfiguration, _controller);
        AddChild(collider);

        var sprite = CreateSprite(_shipConfiguration);
        AddChild(sprite);
    }

    private Node CreateCollider(IShipConfiguration shipConfiguration, IController controller)
    {
        var colliderArea = new Area2D();
        colliderArea.AreaEntered += CollideWith;

        var colliderShape = new CircleShape2D
        {
            Radius = shipConfiguration.ColliderRadius
        };
        var collisionShape = new CollisionShape2D { Shape = colliderShape };
        colliderArea.AddChild(collisionShape);

        var entityReference = new ControllerReference { Controller = controller };
        colliderArea.AddChild(entityReference);

        return colliderArea;
    }

    private void CollideWith(Node2D hitNode)
    {
        var hitController = hitNode
            .GetChildren<ControllerReference>()
            .FirstOrDefault()
            ?.Controller;

        if (hitController == _controller)
            return;

        if (hitController is not IEventHandler<ShipCollisionEvent> eventHandler)
            return;

        eventHandler.Handle(new ShipCollisionEvent(_shipConfiguration.DamageOnCollision));
    }

    private static Node CreateSprite(IShipConfiguration shipConfiguration)
    {
        return new Sprite2D
        {
            Texture = shipConfiguration.Texture,
            Scale = shipConfiguration.TextureScaling.AsUniformVector(),
            TextureFilter = TextureFilterEnum.Nearest
        };
    }

    private static Node CreateController(
        IShipConfiguration shipConfiguration,
        IShip ship,
        ControllerType controllerType,
        out IController controller)
    {
        if (controllerType == ControllerType.Player)
        {
            var playerController = new PlayerController
            {
                Ship = ship,
                ShipConfiguration = shipConfiguration
            };
            controller = playerController;
            return playerController;
        }

        var enemyController = new EnemyController
        {
            Ship = ship,
            ShipConfiguration = shipConfiguration
        };
        controller = enemyController;
        return enemyController;
    }

    public override void _Process(double delta)
    {
        if (_shipModel.IsDead)
            QueueFree();

        Position = _shipModel.Position.VectorValue;
    }
}
