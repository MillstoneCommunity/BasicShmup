using BasicShmup.Dynamics;
using BasicShmup.Entities.Ships.States;

namespace BasicShmup.Entities.Ships;

public interface IShip
{
    void Move(Direction movementDirection);
    void FireProjectile();
    void TakeDamage(Damage damage);
}
