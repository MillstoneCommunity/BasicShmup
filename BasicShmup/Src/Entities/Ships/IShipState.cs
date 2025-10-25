namespace BasicShmup.Entities.Ships;

public interface IShipState
{
    public bool CanFire { get; }
    public void SetFireCooldown();
}