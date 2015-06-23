﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using InverGrove.Data;
using InverGrove.Data.Entities;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Utils;

namespace InverGrove.Domain.Repositories
{
    public class PersonRepository : EntityRepository<Person, int>, IPersonRepository
    {
        private readonly IPhoneNumberRepository phoneNumberRepository;
        private readonly object syncRoot = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonRepository" /> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="phoneNumberRepository">The phone number repository.</param>
        public PersonRepository(IInverGroveContext dataContext, IPhoneNumberRepository phoneNumberRepository)
            : base(dataContext)
        {
            this.phoneNumberRepository = phoneNumberRepository;
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

            Person personEntity = ((Models.Person)person).ToEntity();
            personEntity.DateCreated = currentDate;
            personEntity.DateModified = currentDate;
            personEntity.Profiles = null;
            personEntity.Relatives = null;
            personEntity.Relatives1 = null;
            personEntity.MaritalStatus = null;
            personEntity.ChurchRole = null;
            personEntity.Attendances = null;

            this.Insert(personEntity);

            using (TimedLock.Lock(this.syncRoot))
            {
                try
                {
                    this.Save();
                }
                catch (SqlException sql)
                {
                    throw new ApplicationException("Error occurred in attempting to add Person with PersonId: " +
                        person.FirstName + " " + person.LastName + " with message: " + sql.Message);
                }
            }

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

            var currentDate = DateTime.Now;
            Person personEntity = this.Get(x => x.PersonId == person.PersonId, includeProperties: "PhoneNumbers").FirstOrDefault();
            ICollection<PhoneNumber> personPhoneNumbers = null;
            bool hasNewPhoneNumbers = false;

            if (personEntity != null)
            {
                personPhoneNumbers = personEntity.PhoneNumbers;

                personEntity.FirstName = person.FirstName;
                personEntity.LastName = person.LastName;
                personEntity.MiddleInitial = person.MiddleInitial;
                personEntity.Address1 = person.AddressOne;
                personEntity.Address2 = person.AddressTwo;
                personEntity.City = person.City;
                personEntity.DateOfBirth = person.DateOfBirth;
                personEntity.EmailPrimary = person.PrimaryEmail;
                personEntity.EmailSecondary = person.SecondaryEmail;
                personEntity.Gender = person.Gender;
                personEntity.GroupPhoto = person.GroupPhotoFilePath;
                personEntity.IndividualPhoto = person.IndividualPhotoFilePath;
                personEntity.IsBaptized = person.IsBaptized;
                personEntity.IsMember = person.IsMember;
                personEntity.IsVisitor = person.IsVisitor;
                personEntity.MaritalStatusId = person.MaritalStatusId;
                personEntity.ChurchRoleId = person.ChurchRoleId;
                personEntity.State = person.State;
                personEntity.Zip = person.ZipCode;
                personEntity.DateModified = currentDate;
                personEntity.Profiles = null;
                personEntity.Relatives = null;
                personEntity.Relatives1 = null;
                personEntity.MaritalStatus = null;
                personEntity.ChurchRole = null;
                personEntity.PhoneNumbers = null;
                personEntity.Attendances = null;

                base.Update(personEntity);
            }

            foreach (var phone in person.PhoneNumbers)
            {
                if (personPhoneNumbers != null)
                {
                    var phoneEntity = personPhoneNumbers.FirstOrDefault(p => p.PhoneNumberId == phone.PhoneNumberId);

                    if (phoneEntity != null)
                    {
                        phoneEntity.PersonId = phone.PersonId;
                        phoneEntity.Phone = phone.Phone;
                        phoneEntity.PhoneNumberTypeId = phone.PhoneNumberTypeId;
                        phoneEntity.Person = null;
                        phoneEntity.PhoneNumberType = null;

                        this.dataContext.SetModified(phoneEntity);
                    }
                    else
                    {
                        var entityPhone = phone.ToEntity();
                        entityPhone.Person = null;
                        entityPhone.PhoneNumberType = null;

                        hasNewPhoneNumbers = true;
                        this.phoneNumberRepository.Insert(entityPhone);
                    }
                }
                else
                {
                    var entityPhone = phone.ToEntity();
                    entityPhone.Person = null;
                    entityPhone.PhoneNumberType = null;
                    hasNewPhoneNumbers = true;

                    this.phoneNumberRepository.Insert(entityPhone);
                }
            }

            using (TimedLock.Lock(this.syncRoot))
            {
                try
                {
                    this.Save();

                    if (hasNewPhoneNumbers)
                    {
                        this.phoneNumberRepository.Save();
                    }
                }
                catch (SqlException sql)
                {
                    person.ErrorMessage = "Error occurred in attempting to update Person with PersonId: " + person.PersonId +
                                               " with message: " + sql.Message;
                    throw new ApplicationException("Error occurred in attempting to update Person with PersonId: " + person.PersonId +
                                                   " with message: " + sql.Message);
                }
            }

            return person;
        }

        public bool Delete(IPerson person)
        {
            Guard.ArgumentNotNull(person, "person");

            var entityPerson = this.GetById(person.PersonId);

            if (entityPerson == null || entityPerson.LastName == "Bailles")
            {
                //TODO - provider of profiles that can't be deleted.
                return true; // already deleted by concurrent user?  all the better.
            }

            try
            {
                base.Delete(entityPerson);
                this.Save();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
                throw new ApplicationException("Error occurred in attempting to delete Person with PersonId: " + person.PersonId +
                                               " with message: " + ex.Message);
            }
        }

    }
}