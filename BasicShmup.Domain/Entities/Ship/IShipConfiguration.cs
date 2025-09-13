using BasicShmup.Domain.Dynamics;

namespace BasicShmup.Domain.Entities.Ship;

public interface IShipConfiguration
{
    Speed Speed { get; }
    float Radius { get; }
}
