using System;
using BasicShmup.Dynamics;
using BasicShmup.Entities.Ships.Controllers;

namespace BasicShmup.Entities.Ships;

public interface IShip
{
    Position Position { get; set; }
    bool IsDead { get; }

    void Update(TimeSpan deltaTime);
    void FireProjectile(IController controller);
    void TakeDamage(Damage damage);
}
