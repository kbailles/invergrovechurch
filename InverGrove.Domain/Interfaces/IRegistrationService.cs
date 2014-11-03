using InverGrove.Domain.Exceptions;

namespace InverGrove.Domain.Interfaces
{
    public interface IRegistrationService
    {
        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userToRegister">The user to register.</param>
        /// <returns></returns>
        /// <exception cref="ParameterNullException">userToRegister</exception>
        bool RegisterUser(IRegister userToRegister);
    }
}