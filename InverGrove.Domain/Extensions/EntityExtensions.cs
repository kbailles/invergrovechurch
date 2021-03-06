﻿using InverGrove.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using InverGrove.Domain.Enums;

namespace InverGrove.Domain.Extensions
{
    public static class EntityExtensions
    {
        public static Membership ToModel(this Data.Entities.Membership membership)
        {
            var membershipModel = new Membership();

            if (membership == null)
            {
                return membershipModel;
            }

            membershipModel.MembershipId = membership.MembershipId;
            membershipModel.DateLastLogin = membership.DateLastLogin;
            membershipModel.DateLockedOut = membership.DateLockedOut;
            membershipModel.DateModified = membership.DateModified;
            membershipModel.FailedPasswordAnswerAttemptCount = membership.FailedPasswordAnswerAttemptCount;
            membershipModel.FailedPasswordAnswerAttemptWindowStart = membership.FailedPasswordAnswerAttemptWindowStart;
            membershipModel.FailedPasswordAttemptCount = membership.FailedPasswordAttemptCount;
            membershipModel.FailedPasswordAttemptWindowStart = membership.FailedPasswordAttemptWindowStart;
            membershipModel.IsLockedOut = membership.IsLockedOut;
            membershipModel.IsApproved = membership.IsApproved;
            membershipModel.Password = membership.Password;
            membershipModel.PasswordAnswer = membership.PasswordAnswer;
            membershipModel.PasswordFormatId = membership.PasswordFormatId;
            membershipModel.PasswordQuestion = membership.PasswordQuestion;
            membershipModel.PasswordSalt = membership.PasswordSalt;
            membershipModel.DateLastActivity = membership.DateLastActivity;
            membershipModel.UserId = membership.UserId;

            if (membership.User != null)
            {
                membershipModel.User = membership.User.ToModel();
            }

            return membershipModel;
        }

        public static Data.Entities.Membership ToEntity(this Membership membershipModel)
        {
            var membership = new Data.Entities.Membership();

            if (membershipModel == null)
            {
                return membership;
            }

            membership.MembershipId = membershipModel.MembershipId;
            membership.DateLastLogin = membershipModel.DateLastLogin;
            membership.DateLockedOut = membershipModel.DateLockedOut;
            membership.DateModified = DateTime.Now;
            membership.FailedPasswordAnswerAttemptCount = membershipModel.FailedPasswordAnswerAttemptCount;
            membership.FailedPasswordAnswerAttemptWindowStart = membershipModel.FailedPasswordAnswerAttemptWindowStart;
            membership.FailedPasswordAttemptCount = membershipModel.FailedPasswordAttemptCount;
            membership.FailedPasswordAttemptWindowStart = membershipModel.FailedPasswordAttemptWindowStart;
            membership.IsLockedOut = membershipModel.IsLockedOut;
            membership.IsApproved = membershipModel.IsApproved;
            membership.Password = membershipModel.Password;
            membership.PasswordAnswer = membershipModel.PasswordAnswer;
            membership.PasswordFormatId = membershipModel.PasswordFormatId;
            membership.PasswordQuestion = membershipModel.PasswordQuestion;
            membership.PasswordSalt = membershipModel.PasswordSalt;
            membership.DateLastActivity = membershipModel.DateLastActivity;

            if (membershipModel.User != null)
            {
                var user = (User)membershipModel.User;
                membership.User = user.ToEntity();
            }

            return membership;
        }

        public static Data.Entities.User ToEntity(this User userModel)
        {
            var user = new Data.Entities.User();

            if (userModel == null)
            {
                return user;
            }

            user.UserId = userModel.UserId;
            user.UserName = userModel.UserName;
            user.DateCreated = userModel.DateCreated;
            user.DateModified = userModel.DateModified;
            user.LastActivityDate = userModel.LastActivityDate;
            user.IsAnonymous = userModel.IsAnonymous;

            return user;
        }

        public static User ToModel(this Data.Entities.User user)
        {
            var userModel = new User();

            if (user == null)
            {
                return userModel;
            }

            userModel.UserId = user.UserId;
            userModel.UserName = user.UserName;
            userModel.DateCreated = user.DateCreated;
            userModel.DateModified = user.DateModified;
            userModel.LastActivityDate = user.LastActivityDate;
            userModel.IsAnonymous = user.IsAnonymous;

            return userModel;
        }

        public static Data.Entities.Person ToEntity(this Person personModel)
        {
            var person = new Data.Entities.Person();

            if (personModel == null)
            {
                return person;
            }

            person.PersonId = personModel.PersonId;
            person.FirstName = personModel.FirstName.Trim();
            person.LastName = personModel.LastName.Trim();
            person.MiddleInitial = !string.IsNullOrEmpty(personModel.MiddleInitial) ? personModel.MiddleInitial.Trim() : personModel.MiddleInitial;
            person.Address1 = personModel.AddressOne;
            person.Address2 = personModel.AddressTwo;
            person.City = personModel.City;
            person.State = personModel.State;
            person.Zip = personModel.ZipCode;
            person.DateOfBirth = personModel.DateOfBirth;
            person.EmailPrimary = personModel.PrimaryEmail;
            person.EmailSecondary = personModel.SecondaryEmail;
            person.Gender = personModel.Gender;
            person.GroupPhoto = personModel.GroupPhotoFilePath;
            person.IndividualPhoto = personModel.IndividualPhotoFilePath;
            person.IsBaptized = personModel.IsBaptized;
            person.IsMember = personModel.IsMember;
            person.IsVisitor = personModel.IsVisitor;
            person.IsDeleted = personModel.IsDeleted;
            person.ModifiedByUserId = personModel.ModifiedByUserId;
            person.MaritalStatusId = (personModel.MaritalStatusId == 0) ? 4 : personModel.MaritalStatusId; // 4 is unknown
            person.ChurchRoleId = personModel.ChurchRoleId;

            if (personModel.PhoneNumbers != null)
            {
                foreach (var modelPhoneNumber in personModel.PhoneNumbers)
                {
                    if (modelPhoneNumber.Phone.IsValidPhoneNumber(PhoneNumberFormatType.UsAllFormats))
                    {
                        person.PhoneNumbers.Add(new Data.Entities.PhoneNumber
                        {
                            Phone = modelPhoneNumber.Phone.StripPhoneString(),
                            PhoneNumberTypeId = modelPhoneNumber.PhoneNumberTypeId,
                            PersonId = modelPhoneNumber.PersonId,
                            PhoneNumberType = null
                        });
                    }
                }
            }

            return person;
        }

        public static Person ToModel(this Data.Entities.Person person)
        {
            var personModel = new Person();

            if (person == null)
            {
                return personModel;
            }

            personModel.PersonId = person.PersonId;
            personModel.FirstName = person.FirstName;
            personModel.LastName = person.LastName;
            personModel.MiddleInitial = person.MiddleInitial;
            personModel.AddressOne = person.Address1;
            personModel.AddressTwo = person.Address2;
            personModel.City = person.City;
            personModel.State = person.State;
            personModel.ZipCode = person.Zip;
            personModel.DateOfBirth = person.DateOfBirth;
            personModel.PrimaryEmail = person.EmailPrimary;
            personModel.SecondaryEmail = person.EmailSecondary;
            personModel.Gender = person.Gender;
            personModel.GroupPhotoFilePath = person.GroupPhoto;
            personModel.IndividualPhotoFilePath = person.IndividualPhoto;
            personModel.IsBaptized = person.IsBaptized;
            personModel.IsMember = person.IsMember;
            personModel.IsVisitor = person.IsVisitor;
            personModel.IsDeleted = person.IsDeleted;
            personModel.ModifiedByUserId = person.ModifiedByUserId;
            personModel.MaritalStatusId = person.MaritalStatusId;
            personModel.ChurchRoleId = person.ChurchRoleId;
            personModel.State = person.State;

            if (person.PhoneNumbers != null) /* one of these is not set to an instance */
            {
                personModel.PhoneNumbers = new List<PhoneNumber>();

                foreach (var entityPhoneNumber in person.PhoneNumbers)
                {
                    var modelNumber = new PhoneNumber
                                      {
                                          PhoneNumberId = entityPhoneNumber.PhoneNumberId,
                                          Phone = entityPhoneNumber.Phone,
                                          PhoneNumberTypeId = entityPhoneNumber.PhoneNumberTypeId,
                                          PersonId = entityPhoneNumber.PersonId
                                      };

                    if (entityPhoneNumber.PhoneNumberType != null)
                    {
                        modelNumber.PhoneNumberType = entityPhoneNumber.PhoneNumberType.Description;
                    }
                    personModel.PhoneNumbers.Add(modelNumber);
                }
            }

            return personModel;
        }

        public static Data.Entities.PhoneNumber ToEntity(this PhoneNumber phoneNumber)
        {
            var phoneEntity = new Data.Entities.PhoneNumber();

            if (ReferenceEquals(phoneNumber, null) || !phoneNumber.Phone.IsValidPhoneNumber(PhoneNumberFormatType.UsAllFormats))
            {
                return null;
            }

            phoneEntity.PersonId = phoneNumber.PersonId;
            phoneEntity.PhoneNumberId = phoneNumber.PhoneNumberId;
            phoneEntity.Phone = phoneNumber.Phone.StripPhoneString();
            phoneEntity.PhoneNumberTypeId = phoneNumber.PhoneNumberTypeId;

            return phoneEntity;
        }

        // ---------------------------------------------------------------------------------------- IEnumerable<IPerson> .ToModelCollection() START

        public static IEnumerable<Person> ToModelCollection(this IEnumerable<Data.Entities.Person> entityPeople)
        {
            var people = new List<Person>();

            if (entityPeople == null)
            {
                return people;
            }

            people.AddRange(entityPeople.Select(entityPerson => entityPerson.ToModel()));

            return people;
        }

        // ---------------------------------------------------------------------------------------- IEnumerable<IPerson> .ToModelCollection()  END

        public static Data.Entities.Profile ToEntity(this Profile profileModel)
        {
            var profile = new Data.Entities.Profile();

            if (profileModel == null)
            {
                return profile;
            }

            profile.ProfileId = profileModel.ProfileId;
            profile.PersonId = profileModel.PersonId;
            profile.ReceiveEmailNotification = profileModel.ReceiveEmailNotification;
            profile.IsActive = profileModel.IsActive;
            profile.IsDisabled = profileModel.IsDisabled;
            profile.IsValidated = profileModel.IsValidated;
            profile.UserId = profileModel.UserId;
            profile.DateModified = profileModel.DateModified;
            profile.DateCreated = profileModel.DateCreated;

            if (profileModel.Person != null)
            {
                profile.Person = ((Person)profileModel.Person).ToEntity();
            }

            return profile;
        }

        public static Profile ToModel(this Data.Entities.Profile profile)
        {
            var profileModel = new Profile();

            if (profile == null)
            {
                return profileModel;
            }

            profileModel.ProfileId = profile.ProfileId;
            profileModel.PersonId = profile.PersonId;
            profileModel.ReceiveEmailNotification = profile.ReceiveEmailNotification;
            profileModel.IsActive = profile.IsActive;
            profileModel.IsDisabled = profile.IsDisabled;
            profileModel.IsValidated = profile.IsValidated;
            profileModel.UserId = profile.UserId;
            profileModel.DateModified = profile.DateModified;
            profileModel.DateCreated = profile.DateCreated;

            if (profile.Person != null)
            {
                profileModel.Person = profile.Person.ToModel();
            }

            return profileModel;
        }

        public static Role ToModel(this Data.Entities.Role entityRole)
        {
            var roleModel = new Role();

            if (entityRole == null)
            {
                return roleModel;
            }

            roleModel.RoleId = entityRole.RoleId;
            roleModel.Description = entityRole.Description;

            return roleModel;
        }

        public static IEnumerable<Role> ToModelCollection(this IEnumerable<Data.Entities.Role> entityRoles)
        {
            var roles = new List<Role>();

            if (entityRoles == null)
            {
                return roles;
            }

            roles.AddRange(entityRoles.Select(entityRole => entityRole.ToModel()));

            return roles;
        }

        public static UserRole ToModel(this Data.Entities.UserRole entityUserRole)
        {
            var userRoleModel = new UserRole();

            if (entityUserRole == null)
            {
                return userRoleModel;
            }

            userRoleModel.RoleId = entityUserRole.RoleId;
            userRoleModel.UserId = entityUserRole.UserId;

            userRoleModel.Role = entityUserRole.Role.ToModel();
            userRoleModel.User = entityUserRole.User.ToModel();

            return userRoleModel;
        }

        public static IEnumerable<UserRole> ToModelCollection(this IEnumerable<Data.Entities.UserRole> entityUserRoles)
        {
            var userRoles = new List<UserRole>();

            if (entityUserRoles == null)
            {
                return userRoles;
            }

            userRoles.AddRange(entityUserRoles.Select(entityRole => entityRole.ToModel()));

            return userRoles;
        }

        public static Data.Entities.Sermon ToEntity(this Sermon sermonModel)
        {
            var sermon = new Data.Entities.Sermon();

            if (sermonModel == null)
            {
                return sermon;
            }

            sermon.DateCreated = sermonModel.DateCreated;
            sermon.DateModified = sermonModel.DateModified;
            sermon.ModifiedByUserId = sermonModel.ModifiedByUserId;
            sermon.SermonDate = sermonModel.SermonDate;
            sermon.SermonId = sermonModel.SermonId;
            sermon.SoundCloudId = sermonModel.SoundCloudId;
            sermon.Speaker = sermonModel.Speaker;
            sermon.Tags = sermonModel.Tags;
            sermon.Title = sermonModel.Title;

            return sermon;
        }

        public static Sermon ToModel(this Data.Entities.Sermon sermonEntity)
        {
            var sermon = new Sermon();

            if (sermonEntity == null)
            {
                return sermon;
            }

            sermon.DateCreated = sermonEntity.DateCreated;
            sermon.DateModified = sermonEntity.DateModified;
            sermon.ModifiedByUserId = sermonEntity.ModifiedByUserId;
            sermon.SermonDate = sermonEntity.SermonDate;
            sermon.SermonId = sermonEntity.SermonId;
            sermon.SoundCloudId = sermonEntity.SoundCloudId;
            sermon.Speaker = sermonEntity.Speaker;
            sermon.Tags = sermonEntity.Tags;
            sermon.Title = sermonEntity.Title;

            return sermon;
        }

        public static IEnumerable<Sermon> ToModelCollection(this IEnumerable<Data.Entities.Sermon> entitySermons)
        {
            var sermons = new List<Sermon>();

            if (entitySermons == null)
            {
                return sermons;
            }

            sermons.AddRange(entitySermons.Select(entitySermon => entitySermon.ToModel()));

            return sermons;
        }

        public static Data.Entities.Attendance ToEntity(this Attendance attendance)
        {
            var attendanceEntity = new Data.Entities.Attendance();

            if (attendance == null)
            {
                return attendanceEntity;
            }

            attendanceEntity.AttendanceId = attendance.AttendanceId;
            attendanceEntity.AbsentReasonId = attendance.AbsentReasonId == 0 ? null : attendance.AbsentReasonId;
            attendanceEntity.IsAbsent = attendance.IsAbsent;
            attendanceEntity.DateAttended = attendance.DateAttended;
            attendanceEntity.IsEvening = attendance.IsEvening;
            attendanceEntity.IsSunday = attendance.IsSunday;
            attendanceEntity.IsWednesday = attendance.IsWednesday;
            attendanceEntity.PersonId = attendance.PersonId;

            return attendanceEntity;
        }

        public static Attendance ToModel(this Data.Entities.Attendance attendanceEntity)
        {
            var attendance = new Attendance();

            if (attendanceEntity == null)
            {
                return attendance;
            }

            attendance.AttendanceId = attendanceEntity.AttendanceId;
            attendance.AbsentReasonId = attendanceEntity.AbsentReasonId;
            attendance.DateAttended = attendanceEntity.DateAttended;
            attendance.IsAbsent = attendanceEntity.IsAbsent;
            attendance.IsEvening = attendanceEntity.IsEvening;
            attendance.IsSunday = attendanceEntity.IsSunday;
            attendance.IsWednesday = attendanceEntity.IsWednesday;
            attendance.PersonId = attendanceEntity.PersonId;

            return attendance;
        }

        public static IEnumerable<Attendance> ToModelCollection(this IEnumerable<Data.Entities.Attendance> attendanceEntityCollection)
        {
            var attendanceCollection = new List<Attendance>();

            if (attendanceEntityCollection == null)
            {
                return attendanceCollection;
            }

            foreach (var attendance in attendanceEntityCollection)
            {
                attendanceCollection.Add(attendance.ToModel());
            }

            return attendanceCollection;
        }
    }
}