using BasicShmup.Entities.Controllers;
using Godot;

namespace BasicShmup.Entities.Ships;

[GlobalClass]
public partial class Player : Node
{
    private readonly ICharacterBodyControllerStrategy _controllerStrategy = new CharacterBodyControllerStrategy();
    private readonly CharacterBody2D _body;

    [Export]
    private float _speed = 750;

    [Export]
    private float _radius = 75;

    [Export]
    // todo use error texture
    private Texture2D _texture = ResourceLoader.Load<Texture2D>("res://icon.svg");

    public Player()
    {
        _body = CreateBody();

        _body.AddChild(new Sprite2D
        {
            Texture = _texture
        });

        AddChild(_body);
    }

    private CharacterBody2D CreateBody()
    {
        var collisionShape = new CollisionShape2D
        {
            Shape = new CircleShape2D
            {
                Radius = _radius
            }
        };

        var body = new CharacterBody2D
        {
            MotionMode = CharacterBody2D.MotionModeEnum.Floating
        };
        body.AddChild(collisionShape);
        return body;
    }

    public override void _PhysicsProcess(double delta)
    {
        _controllerStrategy.Move(_body, _speed);
    }
}

// todo update documentation
