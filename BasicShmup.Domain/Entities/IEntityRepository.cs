namespace BasicShmup.Domain.Entities;

public interface IEntityRepository
{
    void Add(IEntity entity);
    IEnumerable<IEntity> GetAll();
}
