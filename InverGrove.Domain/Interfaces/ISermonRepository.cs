namespace InverGrove.Domain.Interfaces
{
    public interface ISermonRepository : IEntityRepository<Data.Entities.Sermon, int>
    {
        /// <summary>
        /// Adds the specified new sermon.
        /// </summary>
        /// <param name="newSermon">The new sermon.</param>
        /// <returns></returns>
        int Add(ISermon newSermon);

        /// <summary>
        /// Updates the specified sermon.
        /// </summary>
        /// <param name="sermon">The sermon.</param>
        void Update(ISermon sermon);
    }
}