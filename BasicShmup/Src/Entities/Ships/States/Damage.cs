namespace BasicShmup.Entities.Ships.States;

public readonly record struct Damage(int Amount)
{
    public static implicit operator Damage(int amount) => new(amount);
}
