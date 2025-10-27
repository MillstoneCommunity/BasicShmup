using BasicShmup.Dynamics;
using Godot;

namespace BasicShmup.Entities.Ships;

public interface IShipConfiguration
{
    Texture2D Texture { get; }
    float TextureScaling { get; }
    Speed Speed { get; }
    float ColliderRadius { get; }
    Health Health { get; }
}
