using System.Linq;
using BasicShmup.Dynamics;
using BasicShmup.Entities.Battle;
using BasicShmup.Entities.Projectiles;
using BasicShmup.Entities.Ships.Controllers;
using BasicShmup.Entities.Ships.States;
using BasicShmup.Events;
using BasicShmup.Extensions;
using BasicShmup.ServiceProviders;
using Godot;

namespace BasicShmup.Entities.Ships;

[GlobalClass]
public partial class Ship : Node2D, IShip
{
    [Inject]
    private readonly IEventSender _eventSender = null!;

    [Inject]
    private readonly IShipConfiguration _shipConfiguration = null!;

    [Export]
    private ControllerType _controllerType;

    private IShipState _state = null!;
    private IController _controller = null!;

    public new Position Position
    {
        get => base.Position;
        set => base.Position = value.VectorValue;
    }

    public override void _Ready()
    {
        var controllerNode = CreateController(_shipConfiguration, _controllerType, out _controller);
        AddChild(controllerNode);

        var shipStateNode = CreateShipState(_shipConfiguration, out _state);
        AddChild(shipStateNode);

        var collider = CreateCollider(_shipConfiguration, _controller);
        AddChild(collider);

        var sprite = CreateSprite(_shipConfiguration);
        AddChild(sprite);
    }

    private static Node CreateShipState(IShipConfiguration shipConfiguration, out IShipState state)
    {
        var shipState = new ShipState
        {
            Health = shipConfiguration.Health
        };
        state = shipState;

        return shipState;
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

    private Node CreateController(
        IShipConfiguration shipConfiguration,
        ControllerType controllerType,
        out IController controller)
    {
        if (controllerType == ControllerType.Player)
        {
            var playerController = new PlayerController
            {
                Ship = this,
                ShipConfiguration = shipConfiguration
            };
            controller = playerController;
            return playerController;
        }

        var enemyController = new EnemyController
        {
            Ship = this,
            ShipConfiguration = shipConfiguration
        };
        controller = enemyController;
        return enemyController;
    }

    #region IShip

    public void FireProjectile()
    {
        if (!_state.CanFire)
            return;

        _state.SetFireCooldown();

        var projectile = new Projectile
        {
            SourceController = _controller,
            Position = GlobalPosition,
            MovementDirection = Direction.Right
        };

        _eventSender.Send(new SpawnBattleNodeEvent(projectile));
    }

    public void TakeDamage(Damage damage)
    {
        _state.TakeDamage(damage);

        if (_state.IsDead)
            QueueFree();
    }

    #endregion
}
