namespace InverGrove.Domain.Interfaces
{
    public interface IProfileRepository : IEntityRepository<Data.Entities.Profile, int>
    {
        /// <summary>
        /// Adds the specified profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns></returns>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">profile</exception>
        int Add(IProfile profile);

        /// <summary>
        /// Adds the person profile.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <param name="profile">The profile.</param>
        /// <returns></returns>
        int AddPersonProfile(IPerson person, IProfile profile);

        /// <summary>
        /// Updates the specified profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">profile</exception>
        bool Update(IProfile profile);
    }
}