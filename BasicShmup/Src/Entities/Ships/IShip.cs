using BasicShmup.Dynamics;

namespace BasicShmup.Entities.Ships;

public interface IShip
{
    void Move(Direction movementDirection);
    void FireProjectile();
    void TakeDamage(Damage damage);
}
