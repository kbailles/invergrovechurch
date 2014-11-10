using System;
using System.Data.SqlClient;
using InverGrove.Data;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
using InverGrove.Domain.Utils;

namespace InverGrove.Domain.Repositories
{
    public class UserRepository : EntityRepository<Data.Entities.User, int>, IUserRepository
    {
        private readonly object syncRoot = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public UserRepository(IInverGroveContext dataContext)
            : base(dataContext)
        {
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public new static IUserRepository Create()
        {
            return new UserRepository(InverGroveContext.Create());
        }

        /// <summary>
        /// Adds the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">user</exception>
        /// <exception cref="System.ApplicationException">Error occurred in attempting to create User with message:  + ex.Message</exception>
        public int Add(IUser user)
        {
            if (user == null)
            {
                throw new ParameterNullException("user");
            }

            var currentDate = DateTime.Now;
            user.DateCreated = currentDate;
            user.DateModified = currentDate;
            user.LastActivityDate = currentDate;

            Data.Entities.User userEntity = ((User)user).ToEntity();

            try
            {
                this.Insert(userEntity);
                this.Save();
            }
            catch (SqlException ex)
            {

                throw new ApplicationException("Error occurred in attempting to create User with message: " + ex.Message);
            }

            return userEntity.UserId;
        }


        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public IUser Update(IUser user)
        {
            if (user == null)
            {
                throw new ParameterNullException("user");
            }

            var currentUser = (User)user;
            var currentDate = DateTime.Now;
            currentUser.DateModified = currentDate;
            currentUser.LastActivityDate = currentDate;

            Data.Entities.User userEntity = this.GetById(user.UserId);

            if (userEntity != null)
            {
                userEntity.UserName = currentUser.UserName;
                userEntity.IsAnonymous = currentUser.IsAnonymous;
                userEntity.DateModified = currentUser.DateModified;
                userEntity.LastActivityDate = currentUser.LastActivityDate;
            }

            using (TimedLock.Lock(this.syncRoot))
            {
                try
                {
                    base.Update(userEntity);

                    this.Save();
                }
                catch (SqlException sql)
                {
                    currentUser.ErrorMessage = "Error occurred in attempting to update User with userId: " + user.UserId +
                                               " with message: " + sql.Message;
                    throw new ApplicationException("Error occurred in attempting to update User with userId: " + user.UserId +
                                                   " with message: " + sql.Message);
                }
            }

            return currentUser;
        }
    }
}