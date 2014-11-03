namespace InverGrove.Domain.Interfaces
{
    public interface IPersonService
    {
        /// <summary>
        /// Adds the person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">person</exception>
        int AddPerson(IPerson person);
    }
}