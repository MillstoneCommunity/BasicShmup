using BasicShmup.Dynamics;
using Godot;

namespace BasicShmup.Entities.Ships;

public interface IShipConfiguration
{
    public Speed Speed { get; }
    public float ColliderRadius { get; }
    public Texture2D Texture { get; }
    public float TextureScaling { get; }
}
