using Godot;

namespace BasicShmup.Extensions;

public static class VectorExtensions
{
    public static Vector2 AsUniformVector(this float f) => new(f, f);
}
