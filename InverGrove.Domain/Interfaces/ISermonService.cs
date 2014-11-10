using System.Collections.Generic;

namespace InverGrove.Domain.Interfaces
{
    public interface ISermonService
    {
        /// <summary>
        /// Adds the sermon.
        /// </summary>
        /// <param name="newSermon">The new sermon.</param>
        /// <returns></returns>
        int AddSermon(ISermon newSermon);

        /// <summary>
        /// Deletes the sermon.
        /// </summary>
        /// <param name="sermonId">The sermon identifier.</param>
        void DeleteSermon(int sermonId);

        /// <summary>
        /// Gets the sermons.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ISermon> GetSermons();

        /// <summary>
        /// Updates the sermon.
        /// </summary>
        /// <param name="sermonToUpdate">The sermon to update.</param>
        /// <returns></returns>
        bool UpdateSermon(ISermon sermonToUpdate);
    }
}