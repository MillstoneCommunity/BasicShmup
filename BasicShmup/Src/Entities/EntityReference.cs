using Godot;

namespace BasicShmup.Entities;

public partial class EntityReference : Node
{
    public required IEntity Entity { get; init; }
}
