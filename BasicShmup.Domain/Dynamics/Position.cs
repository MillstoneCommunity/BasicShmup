namespace BasicShmup.Domain.Dynamics;

public record struct Position(float X, float Y)
{
    public static readonly Position Zero = new(0, 0);

    public static Position operator +(Position position, Movement movement) =>
        new(position.X + movement.DeltaX, position.Y + movement.DeltaY);
}
