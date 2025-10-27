namespace BasicShmup.Entities.Ships;

public interface IShipState
{
    bool CanFire { get; }
    public bool IsDead { get; }
    void SetFireCooldown();
    void TakeDamage(Damage damage);
}
