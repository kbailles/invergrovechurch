using InverGrove.Data;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Repositories
{
    public class PhoneNumberRepository : EntityRepository<Data.Entities.PhoneNumber, int>, IPhoneNumberRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumberRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PhoneNumberRepository(IInverGroveContext dataContext)
            : base(dataContext)
        {
        }
    }
}