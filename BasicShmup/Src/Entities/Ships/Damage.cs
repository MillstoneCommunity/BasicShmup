namespace BasicShmup.Entities.Ships;

public readonly record struct Damage(int Amount)
{
    public static implicit operator Damage(int amount) => new(amount);
}
