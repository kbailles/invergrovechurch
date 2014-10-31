using System.Collections.Generic;

namespace InverGrove.Domain.Interfaces
{
    public interface ISermonService
    {
        int AddSermon(ISermon newSermon);

        IEnumerable<ISermon> GetSermons();
    }
}