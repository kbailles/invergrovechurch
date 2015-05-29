using InverGrove.Data;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Repositories
{
    public class AbsentReasonRepository : EntityRepository<Data.Entities.AbsentReason, int>, IAbsentReasonRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChurchRoleRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public AbsentReasonRepository(IInverGroveContext dataContext)
            : base(dataContext)
        {
        }
    }
}
