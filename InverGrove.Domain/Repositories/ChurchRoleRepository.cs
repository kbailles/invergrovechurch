using InverGrove.Data;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Repositories
{
    public class ChurchRoleRepository : EntityRepository<Data.Entities.ChurchRole, int>, IChurchRoleRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChurchRoleRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ChurchRoleRepository(IInverGroveContext dataContext)
            : base(dataContext)
        {
        }
    }
}
