namespace BasicShmup.Domain.Entities;

public class EntityRepository : IEntityRepository
{
    private readonly List<IEntity> _entities = [];

    public void Add(IEntity entity)
    {
        _entities.Add(entity);
    }

    public IEnumerable<IEntity> GetAll()
    {
        return _entities;
    }
}
