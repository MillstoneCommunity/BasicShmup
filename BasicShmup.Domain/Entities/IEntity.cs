using BasicShmup.Domain.Dynamics;

namespace BasicShmup.Domain.Entities;

public interface IEntity
{
    void Move(DeltaTime deltaTime);
}
