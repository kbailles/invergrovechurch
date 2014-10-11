using System;
using System.Collections.Generic;
using System.Linq;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Factories;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
using InverGrove.Domain.Repositories;

namespace InverGrove.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        public UserService(IUserRepository userRepository = null)
        {
            this.userRepository = userRepository ?? UserRepository.Create();
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
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

            var newUserId = this.userRepository.Add(user);
            user.UserId = newUserId;

            return user;
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

            var user = this.userRepository.Get(u => u.UserId == userId).FirstOrDefault();

            if (user != null)
            {
                return user.ToModel();
            }

            return ObjectFactory.Create<User>();
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
                this.userRepository.Update(user);
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IUser> GetAllUsers()
        {
            return this.userRepository.Get().Select(user => user.ToModel()).Cast<IUser>().ToSafeList();
        }
    }
}