using InverGrove.Domain.Exceptions;

namespace InverGrove.Domain.Interfaces
{
    public interface IRegistrationService
    {
        /// <summary>
        /// Gets the register view model.
        /// </summary>
        /// <returns></returns>
        IRegister GetRegisterViewModel();

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userToRegister">The user to register.</param>
        /// <returns></returns>
        /// <exception cref="ParameterNullException">userToRegister</exception>
        bool RegisterUser(IRegister userToRegister);
    }
}