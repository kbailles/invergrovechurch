using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using InverGrove.Data;
using InverGrove.Data.Entities;
using InverGrove.Domain.Enums;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Utils;

namespace InverGrove.Domain.Repositories
{
    public class PersonRepository : EntityRepository<Person, int>, IPersonRepository
    {
        private readonly IPhoneNumberRepository phoneNumberRepository;
        private readonly ILogService logService;
        private readonly object syncRoot = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonRepository" /> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="phoneNumberRepository">The phone number repository.</param>
        public PersonRepository(IInverGroveContext dataContext, IPhoneNumberRepository phoneNumberRepository, ILogService logService)
            : base(dataContext)
        {
            this.phoneNumberRepository = phoneNumberRepository;
            this.logService = logService;
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
                    this.logService.WriteToErrorLog("Error occurred in attempting to add Person with name: " +
                                                   person.FirstName + " " + person.LastName + " with message: " + sql.Message);
                }
                catch (DbEntityValidationException dbe)
                {
                    var sb = new StringBuilder();
                    foreach (var error in dbe.EntityValidationErrors)
                    {
                        foreach (var ve in error.ValidationErrors)
                        {
                            sb.Append(ve.ErrorMessage + ", ");
                        }
                    }

                    this.logService.WriteToErrorLog("Error occurred in attempting to add Person with name: " +
                                                   person.FirstName + " " + person.LastName + " with message: " + sb.ToString());
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
            
            //ICollection<PhoneNumber> personPhoneNumbers = null;

            if (personEntity != null)
            {
                //personPhoneNumbers = personEntity.PhoneNumbers;

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
                
                foreach (var phone in person.PhoneNumbers)
                {
                    var existingPhone = personEntity.PhoneNumbers.FirstOrDefault(p => p.PhoneNumberId == phone.PhoneNumberId);

                    if (existingPhone != null && phone.Phone.IsValidPhoneNumber(PhoneNumberFormatType.UsAllFormats))
                    {
                        existingPhone.PersonId = phone.PersonId;
                        existingPhone.Phone = phone.Phone;
                        existingPhone.PhoneNumberTypeId = phone.PhoneNumberTypeId;
                    }
                    else
                    {
                        var entityPhone = phone.ToEntity();

                        if (entityPhone != null)
                        {
                            entityPhone.PersonId = personEntity.PersonId;
                            entityPhone.Person = null;
                            entityPhone.PhoneNumberType = null;
                            personEntity.PhoneNumbers.Add(entityPhone);                                                    
                        }
                    }
                }

                // possible other way to do this:
                //var db = ((InverGroveContext)this.Context);
                //db.Attach(personEntity);
                //var entry = db.Entry(personEntity);
                //entry.State = EntityState.Modified;
                //entry.Property(p => p.Attendances).IsModified = false;
                //entry.Property(p => p.MaritalStatus).IsModified = false;
                //entry.Property(p => p.Profiles).IsModified = false;
                //entry.Property(p => p.Relatives).IsModified = false;
                //entry.Property(p => p.Relatives1).IsModified = false;
                //entry.Property(p => p.ChurchRole).IsModified = false;
                this.Update(personEntity);
            }

            using (TimedLock.Lock(this.syncRoot))
            {
                try
                {
                    this.Save();
                }
                catch (SqlException sql)
                {
                    person.ErrorMessage = "Error occurred in attempting to update Person with PersonId: " + person.PersonId;
                    this.logService.WriteToErrorLog("Error occurred in attempting to update Person with PersonId: " + person.PersonId +
                                                   " with message: " + sql.Message);
                }
                catch (DbEntityValidationException dbe)
                {
                    var sb = new StringBuilder();
                    foreach (var error in dbe.EntityValidationErrors)
                    {
                        foreach (var ve in error.ValidationErrors)
                        {
                            sb.Append(ve.ErrorMessage + ", ");
                        }
                    }

                    person.ErrorMessage = "Error occurred in attempting to update Person with PersonId: " + person.PersonId;
                    this.logService.WriteToErrorLog("Error occurred in attempting to update Person with PersonId: " + person.PersonId +
                                                   " with message: " + sb);
                }
                catch (Exception ex)
                {
                    person.ErrorMessage = "Error occurred in attempting to update Person with PersonId: " + person.PersonId;
                    this.logService.WriteToErrorLog("Error occurred in attempting to update Person with PersonId: " + person.PersonId +
                                                   " with message: " + ex.Message);
                }
            }

            return person;
        }

        private void UpdateNewPhoneNumbers(IPerson person, ICollection<PhoneNumber> personPhoneNumbers)
        {
            foreach (var phone in person.PhoneNumbers)
            {
                if (personPhoneNumbers != null)
                {
                    var phoneEntity = personPhoneNumbers.FirstOrDefault(p => p.PhoneNumberId == phone.PhoneNumberId);

                    if (phoneEntity != null && phone.Phone.IsValidPhoneNumber(PhoneNumberFormatType.UsAllFormats))
                    {
                        phoneEntity.PersonId = phone.PersonId;
                        phoneEntity.Phone = phone.Phone.StripPhoneString();
                        phoneEntity.PhoneNumberTypeId = phone.PhoneNumberTypeId;
                        phoneEntity.Person = null;
                        phoneEntity.PhoneNumberType = null;

                        this.phoneNumberRepository.Update(phoneEntity);
                    }
                    else
                    {
                        this.AddNewPhoneNumber(phone);
                    }
                }
                else
                {
                    this.AddNewPhoneNumber(phone);
                }
            }

            using (TimedLock.Lock(this.syncRoot))
            {
                try
                {
                    this.phoneNumberRepository.Save();
                }
                catch (SqlException sql)
                {
                    person.ErrorMessage = "Error occurred in attempting to update Person with PersonId: " + person.PersonId;
                    this.logService.WriteToErrorLog("Error occurred in attempting to update Person phoneNumbers with PersonId: " + person.PersonId +
                                                   " with message: " + sql.Message);
                }
                catch (DbEntityValidationException dbe)
                {
                    var sb = new StringBuilder();

                    foreach (var error in dbe.EntityValidationErrors)
                    {
                        foreach (var ve in error.ValidationErrors)
                        {
                            sb.Append(ve.ErrorMessage + ", ");
                        }
                    }

                    person.ErrorMessage = "Error occurred in attempting to update Person with PersonId: " + person.PersonId;
                    this.logService.WriteToErrorLog("Error occurred in attempting to update Person phoneNumbers with name: " +
                                                   person.FirstName + " " + person.LastName + " personId: " + person.PersonId +
                                                   " with message: " + sb);
                }
            }
        }

        private void AddNewPhoneNumber(Models.PhoneNumber phone)
        {
            var entityPhone = phone.ToEntity();

            if (entityPhone != null)
            {
                entityPhone.Person = null;
                entityPhone.PhoneNumberType = null;

                this.phoneNumberRepository.Insert(entityPhone);
            }
        }

        /// <summary>
        /// Deletes the specified person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        public bool Delete(IPerson person)
        {
            Guard.ArgumentNotNull(person, "person");

            var entityPerson = this.GetById(person.PersonId);
            var currentDate = DateTime.Now;

            if (entityPerson == null || entityPerson.LastName == "Bailles")
            {
                //TODO - provider of profiles that can't be deleted.
                return true; // already deleted by concurrent user?  all the better.
            }

            entityPerson.DateModified = currentDate;
            entityPerson.ModifiedByUserId = person.ModifiedByUserId;
            entityPerson.IsDeleted = true;
            entityPerson.ChurchRole = null;
            entityPerson.Attendances = null;
            entityPerson.MaritalStatus = null;
            entityPerson.PhoneNumbers = null;
            entityPerson.Profiles = null;
            entityPerson.Relatives = null;
            entityPerson.Relatives1 = null;

            try
            {
                // Not doing a hard delete, just setting the record to IsDeleted = true
                this.Update(entityPerson);
                this.Save();
            }
            catch (SqlException ex)
            {
                this.logService.WriteToErrorLog("Error occurred in attempting to delete Person with PersonId: " + person.PersonId +
                                               " with message: " + ex.Message);
                return false;
            }
            catch (DbEntityValidationException dbe)
            {
                var sb = new StringBuilder();
                foreach (var error in dbe.EntityValidationErrors)
                {
                    foreach (var ve in error.ValidationErrors)
                    {
                        sb.Append(ve.ErrorMessage + ", ");
                    }
                }

                this.logService.WriteToErrorLog("Error occurred in attempting to delete Person with name: " +
                                               person.FirstName + " " + person.LastName + " personId: " + person.PersonId +
                                               " with message: " + sb);

                return false;
            }

            return true;
        }

    }
}