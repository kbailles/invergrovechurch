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

        public int Add(ISermon newSermon)
        {
            if (newSermon == null)
            {
                throw new ParameterNullException("newSermon");
            }

            var newEntitySermon = ((Models.Sermon) newSermon).ToEntity();
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
    }
}