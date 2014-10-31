namespace InverGrove.Domain.Interfaces
{
    public interface ISermonRepository : IEntityRepository<Data.Entities.Sermon, int>
    {
        int Add(ISermon newSermon);
    }
}