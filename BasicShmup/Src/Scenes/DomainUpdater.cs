using BasicShmup.Domain.Dynamics;
using BasicShmup.Domain.Entities;
using BasicShmup.Scenes.SceneConfigurations;
using Godot;

namespace BasicShmup.Scenes;

public partial class DomainUpdater : Node
{
    [Inject]
    private readonly IEntityRepository _entityRepository = null!;

    public override void _PhysicsProcess(double delta)
    {
        var deltaTime = DeltaTime.FromSeconds(delta);
        var entities = _entityRepository.GetAll();

        foreach (var entity in entities)
            entity.Move(deltaTime);
    }
}
