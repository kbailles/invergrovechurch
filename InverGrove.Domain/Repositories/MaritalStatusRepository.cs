using InverGrove.Data;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Repositories
{
    public class MaritalStatusRepository : EntityRepository<Data.Entities.MaritalStatus, int>, IMaritalStatusRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public MaritalStatusRepository(IInverGroveContext dataContext)
            : base(dataContext)
        {
        }
    }
}