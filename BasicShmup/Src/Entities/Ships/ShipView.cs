using BasicShmup.Domain.Entities.Ship;
using BasicShmup.Extensions;
using Godot;

namespace BasicShmup.Entities.Ships;

public partial class ShipView : Node2D
{
    private Sprite2D _sprite = new();

    public required IShip Ship { get; init; }

    public required Texture2D Texture
    {
        init => _sprite.Texture = value;
    }

    public override void _EnterTree()
    {
        AddChild(new PlayerController
        {
            Ship = Ship
        });

        AddChild(_sprite);
    }

    public override void _Process(double _)
    {
        Position = Ship.Position.ToGodotVector();
    }
}
