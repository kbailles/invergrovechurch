using System;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Factories;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
using Invergrove.Domain.Interfaces;
using Invergrove.Domain.Models;

namespace InverGrove.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;

        public UserService(IRepository<User> userRepository = null)
        {
            this.userRepository = userRepository ?? Repository<User>.Create();
        }

        public static IUserService Create()
        {
            return new UserService();
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">userName</exception>
        public IUser CreateUser(string userName)
        {

            Guard.ArgumentNotNullOrEmpty(userName, "userName");

            User user = ObjectFactory.Create<User>();
            user.UserName = userName;
            user.LastActivityDate = DateTime.Now;
            user.IsAnonymous = false;

            return this.userRepository.Add(user);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">userId</exception>
        public IUser GetUser(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException("userId");
            }

            return this.userRepository.Get(userId);
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public bool UpdateUser(IUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            bool success = true;

            try
            {
                this.userRepository.Update((User)user);
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }
    }
}