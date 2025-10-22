using BasicShmup.Domain.Dynamics;
using Godot;

namespace BasicShmup.Entities.Ships;

public interface IShipConfiguration
{
    Texture2D Texture { get; }
    Speed Speed { get; }
    float Radius { get; }
}
