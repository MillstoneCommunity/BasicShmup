using System.Numerics;

namespace BasicShmup.Domain.Movements;

public record MovementCollision(float MovementFraction, Vector2 Normal);
