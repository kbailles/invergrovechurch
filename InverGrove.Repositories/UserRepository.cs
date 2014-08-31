using System;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using InverGrove.Data;
using InverGrove.Domain.Factories;
using InverGrove.Domain.Models;
using InverGrove.Domain.Utils;
using InverGrove.Repositories.Extensions;
using Invergrove.Domain.Models;

namespace InverGrove.Repositories
{
    public class UserRepository : Repository<User>
    {
        private readonly object syncRoot = new object();

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        /// <exception cref="System.ApplicationException">Error occurred in attempting to create User with message:  + sql.Message</exception>
        public override User Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var currentDate = DateTime.Now;
            user.DateCreated = currentDate;
            user.DateModified = currentDate;
            user.LastActivityDate = currentDate;

            Data.Entities.User userEntity = user.ToEntity();
            userEntity.Attendances = null;
            userEntity.MemberNotes = null;
            userEntity.Responsibilities = null;
            userEntity.UserRoles = null;
            userEntity.Memberships = null;
            userEntity.Profiles = null;

            using (var db = (InverGroveContext)contextFactory.GetObjectContext())
            {
                db.Users.Add(userEntity);

                using (var scope = new TransactionScope())
                {
                    try
                    {
                        db.SaveChanges();

                        scope.Complete();
                    }
                    catch (SqlException sql)
                    {
                        throw new ApplicationException("Error occurred in attempting to create User with message: " + sql.Message);
                    }
                }
            }

            user.UserId = userEntity.UserId;

            return user;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">userId is out of range in UserRepository.GetUser</exception>
        /// <exception cref="System.ArgumentNullException">name</exception>
        /// <exception cref="System.ApplicationException">Error occurred in attempting to get User with message:  + sql.Message</exception>
        public override User Get(int? id = null, string name = null)
        {
            if ((id == null) || ((id != null) && (id <= 0)))
            {
                throw new ArgumentException("userId is out of range in UserRepository.GetUser");
            }

            User user = ObjectFactory.Create<User>();

            using (var db = (InverGroveContext)contextFactory.GetObjectContext())
            {
                try
                {
                    var userEntity = db.Users.FirstOrDefault(u => u.UserId == id);

                    if (userEntity != null)
                    {
                        user = userEntity.ToModel();
                    }
                }
                catch (SqlException sql)
                {
                    throw new ApplicationException("Error occurred in attempting to get User with message: " + sql.Message);
                }
            }

            return user;
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public override User Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var currentDate = DateTime.Now;
            user.DateModified = currentDate;
            user.LastActivityDate = currentDate;

            using (var db = (InverGroveContext)contextFactory.GetObjectContext())
            {
                Data.Entities.User userEntity = db.Users.FirstOrDefault(u => u.UserId == user.UserId);

                if (userEntity != null)
                {
                    userEntity.UserName = user.UserName;
                    userEntity.IsAnonymous = user.IsAnonymous;
                    userEntity.DateModified = user.DateModified;
                    userEntity.LastActivityDate = user.LastActivityDate;
                    userEntity.Attendances = null;
                    userEntity.MemberNotes = null;
                    userEntity.Responsibilities = null;
                    userEntity.UserRoles = null;
                    userEntity.Memberships = null;
                    userEntity.Profiles = null;
                }

                using (TimedLock.Lock(this.syncRoot))
                {
                    using (var scope = new TransactionScope())
                    {
                        try
                        {
                            db.SaveChanges();

                            scope.Complete();
                        }
                        catch (SqlException sql)
                        {
                            user.ErrorMessage = "Error occurred in attempting to update User with userId: " + user.UserId +
                                                " with message: " + sql.Message;
                            throw new ApplicationException("Error occurred in attempting to update User with userId: " + user.UserId +
                                                           " with message: " + sql.Message);
                        }
                    }
                }
            }

            return user;
        }

        public override User Delete(User resource)
        {
            if (resource == null)
            {
                throw new ArgumentNullException("resource");
            }
            throw new NotImplementedException();
        }
    }
}