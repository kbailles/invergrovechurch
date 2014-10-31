using System.Collections.Generic;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Services
{
    public class SermonService : ISermonService
    {
        private readonly ISermonRepository sermonRepository;

        public SermonService(ISermonRepository sermonRepository)
        {
            this.sermonRepository = sermonRepository;
        }

        public int AddSermon(ISermon newSermon)
        {
            if (newSermon == null)
            {
                throw new ParameterNullException("newSermon");
            }

            return this.sermonRepository.Add(newSermon);
        }

        public IEnumerable<ISermon> GetSermons()
        {
            var sermons = this.sermonRepository.Get();

            return sermons.ToModelCollection();
        }
    }
}