using Godot;

namespace BasicShmup.Entities.Ships.Controllers;

public partial class ControllerReference : Node
{
    public required IController Entity { get; init; }
}
