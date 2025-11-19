using BasicShmup.Dynamics;
using BasicShmup.Entities.Ships.Controllers;

namespace BasicShmup.Entities.Ships;

public interface ICannon
{
    void FireProjectile(IController controller, Position projectilePosition);
}
