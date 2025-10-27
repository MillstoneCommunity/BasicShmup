namespace BasicShmup.Entities.Ships.States;

public interface IShipState
{
    bool CanFire { get; }
    public bool IsDead { get; }
    void SetFireCooldown();
    void TakeDamage(Damage damage);
}
