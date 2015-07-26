namespace InverGrove.Domain.Interfaces
{
    public interface IRegisterFactory
    {
        /// <summary>
        /// Builds the by user verification.
        /// </summary>
        /// <param name="userVerification">The user verification.</param>
        /// <returns></returns>
        IRegister BuildByUserVerification(IUserVerification userVerification);
    }
}