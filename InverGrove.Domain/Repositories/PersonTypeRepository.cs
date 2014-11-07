using InverGrove.Data;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Repositories
{
    public class PersonTypeRepository : EntityRepository<Data.Entities.PersonType, int>, IPersonTypeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PersonTypeRepository(IInverGroveContext dataContext)
            : base(dataContext)
        {
        }
    }
}