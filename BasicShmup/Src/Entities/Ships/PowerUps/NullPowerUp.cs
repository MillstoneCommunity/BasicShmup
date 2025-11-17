namespace BasicShmup.Entities.Ships.PowerUps;

public class NullPowerUp : IPowerUp
{
    public static NullPowerUp Instance { get; } = new();

    private NullPowerUp()
    {
    }

    public void Apply(IPowerUpShip ship)
    {
    }
}
