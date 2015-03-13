using System;
using System.Collections;
using System.Collections.Generic;
using InverGrove.Data;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
using InverGrove.Domain.Utils;
using System.Data.SqlClient;

namespace InverGrove.Domain.Repositories
{
    public class PersonRepository : EntityRepository<Data.Entities.Person, int>, IPersonRepository
    {
        private readonly object syncRoot = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PersonRepository(IInverGroveContext dataContext)
            : base(dataContext)
        {
        }

        /// <summary>
        /// Adds the specified person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">person</exception>
        public int Add(IPerson person)
        {
            if (person == null)
            {
                throw new ParameterNullException("person");
            }

            var currentDate = DateTime.Now;

            Data.Entities.Person personEntity = ((Person)person).ToEntity();
            personEntity.DateCreated = currentDate;
            personEntity.DateModified = currentDate;
            personEntity.Profiles = null;
            personEntity.Relatives = null;
            personEntity.Relatives1 = null;
            personEntity.MaritalStatus = null;
            personEntity.ChurchRole = null;

            this.Insert(personEntity);

            //this.Save();

            return personEntity.PersonId;
        }

        /// <summary>
        /// Updates the specified person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        /// <exception cref="ParameterNullException">person</exception>
        /// <exception cref="System.ApplicationException">Error occurred in attempting to update Person with PersonId:  + person.PersonId +
        ///                                                     with message:  + sql.Message</exception>
        public IPerson Update(IPerson person)
        {
            if (person == null)
            {
                throw new ParameterNullException("person");
            }

            var currentPerson = (Person)person;
            var currentDate = DateTime.Now;
            Data.Entities.Person personEntity = this.GetById(person.PersonId);

            if (personEntity != null)
            {
                personEntity.FirstName = currentPerson.FirstName;
                personEntity.LastName = currentPerson.LastName;
                personEntity.MiddleInitial = currentPerson.MiddleInitial;
                personEntity.Address1 = currentPerson.AddressOne;
                personEntity.Address2 = currentPerson.AddressTwo;
                personEntity.City = currentPerson.City;
                personEntity.DateOfBirth = currentPerson.DateOfBirth;
                personEntity.EmailPrimary = currentPerson.PrimaryEmail;
                personEntity.EmailSecondary = currentPerson.SecondaryEmail;
                personEntity.Gender = currentPerson.Gender;
                personEntity.GroupPhoto = currentPerson.GroupPhotoFilePath;
                personEntity.IndividualPhoto = currentPerson.IndividualPhotoFilePath;
                personEntity.IsBaptized = currentPerson.IsBaptized;
                personEntity.IsMember = currentPerson.IsMember;
                personEntity.IsVisitor = currentPerson.IsVisitor;
                personEntity.MaritalStatusId = currentPerson.MaritalStatusId;
                personEntity.ChurchRoleId = currentPerson.ChurchRoleId;
                personEntity.State = currentPerson.State;
                personEntity.DateModified = currentDate;
                personEntity.Profiles = null;
                personEntity.Relatives = null;
                personEntity.Relatives1 = null;
                personEntity.MaritalStatus = null;
                personEntity.ChurchRole = null;
            }

            using (TimedLock.Lock(this.syncRoot))
            {
                try
                {
                    base.Update(personEntity);

                    this.Save();
                }
                catch (SqlException sql)
                {
                    currentPerson.ErrorMessage = "Error occurred in attempting to update Person with PersonId: " + person.PersonId +
                                               " with message: " + sql.Message;
                    throw new ApplicationException("Error occurred in attempting to update Person with PersonId: " + person.PersonId +
                                                   " with message: " + sql.Message);
                }
            }

            return currentPerson;
        }

    }
}