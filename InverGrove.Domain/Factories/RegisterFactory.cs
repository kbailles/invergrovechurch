using System;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Utils;
using InverGrove.Domain.ViewModels;

namespace InverGrove.Domain.Factories
{
    public class RegisterFactory : IRegisterFactory
    {
        private static readonly Lazy<RegisterFactory> instance =
            new Lazy<RegisterFactory>(() => new RegisterFactory());

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public static IRegisterFactory Create()
        {
            return instance.Value;
        }

        /// <summary>
        /// Builds the by user verification.
        /// </summary>
        /// <param name="userVerification">The user verification.</param>
        /// <returns></returns>
        public IRegister BuildByUserVerification(IUserVerification userVerification)
        {
            Guard.ArgumentNotNull(userVerification, "userVerification");

            var register = new Register
            {
                Identifier = userVerification.Identifier
            };

            return register;
        }
    }
}