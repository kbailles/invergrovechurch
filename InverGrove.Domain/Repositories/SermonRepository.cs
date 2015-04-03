using System;
using System.Data.SqlClient;
using InverGrove.Data;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Repositories
{
    public class SermonRepository : EntityRepository<Data.Entities.Sermon, int>, ISermonRepository
    {
        public SermonRepository(IInverGroveContext dataContext)
            : base(dataContext)
        {
        }

        /// <summary>
        /// Adds the specified new sermon.
        /// </summary>
        /// <param name="newSermon">The new sermon.</param>
        /// <returns></returns>
        /// <exception cref="ParameterNullException">newSermon</exception>
        /// <exception cref="System.ApplicationException">Error when attempting to add new sermon in SermonRepository:  + ex.Message</exception>
        public int Add(ISermon newSermon)
        {
            if (newSermon == null)
            {
                throw new ParameterNullException("newSermon");
            }

            var currentDate = DateTime.Now;
            newSermon.DateModified = currentDate;

            var newEntitySermon = ((Models.Sermon)newSermon).ToEntity();
            newEntitySermon.DateCreated = currentDate;
            newEntitySermon.User = null;

            this.Insert(newEntitySermon);

            try
            {
                this.Save();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error when attempting to add new sermon in SermonRepository: " + ex.Message);
            }

            return newEntitySermon.SermonId;
        }

        /// <summary>
        /// Updates the specified sermon.
        /// </summary>
        /// <param name="sermon">The sermon.</param>
        /// <exception cref="ParameterNullException">sermon</exception>
        /// <exception cref="System.ApplicationException">Error when attempting to update sermon in SermonRepository:  + ex.Message</exception>
        public void Update(ISermon sermon)
        {
            if (sermon == null)
            {
                throw new ParameterNullException("sermon");
            }

            var currentDate = DateTime.Now;
            sermon.DateModified = currentDate;

            var entitySermon = ((Models.Sermon)sermon).ToEntity();
            this.Update(entitySermon);

            try
            {
                this.Save();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error when attempting to update sermon in SermonRepository: " + ex.Message);
            }
        }
    }
}