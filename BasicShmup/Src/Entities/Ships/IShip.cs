using BasicShmup.Dynamics;
using BasicShmup.Entities.Ships.States;

namespace BasicShmup.Entities.Ships;

public interface IShip
{
    Position Position { get; set; }

    void FireProjectile();
    void TakeDamage(Damage damage);
}
