﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;
using InverGrove.Data.Entities;

namespace InverGrove.Data
{
    public class InverGroveInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<InverGroveContext>
    {
        protected override void Seed(InverGroveContext context)
        {
            var roles = new List<Role>
                           {
                               new Role
                               {
                                   Description = "Guest",
                                   DateCreated = DateTime.Now,
                                   DateModified = DateTime.Now
                               },
                              new Role
                               {
                                   Description = "Member",
                                   DateCreated = DateTime.Now,
                                   DateModified = DateTime.Now
                               },
                               new Role
                               {
                                   Description = "MemberAdmin",
                                   DateCreated = DateTime.Now,
                                   DateModified = DateTime.Now
                               },
                               new Role
                               {
                                   Description = "SiteAdmin",
                                   DateCreated = DateTime.Now,
                                   DateModified = DateTime.Now
                               }
                           };

            roles.ForEach(s => context.Roles.Add(s));
            context.SaveChanges();

            var personTypes = new List<PersonType>
                          {
                              new PersonType
                              {
                                  PersonTypeDescription = "Visitor"
                              },
                              new PersonType
                              {
                                  PersonTypeDescription = "Member"
                              },
                              new PersonType
                              {
                                  PersonTypeDescription = "Deacon"
                              },
                              new PersonType
                              {
                                  PersonTypeDescription = "Elder"
                              },
                              new PersonType
                              {
                                  PersonTypeDescription = "Preacher"
                              },
                              new PersonType
                              {
                                  PersonTypeDescription = "Child"
                              }
                          };
            personTypes.ForEach(s => context.PersonTypes.Add(s));
            context.SaveChanges();

            var maritalStatuses = new List<MaritalStatus>
                                  {
                                      new MaritalStatus { MaritalStatusDescription = "Single - Never Married"},
                                      new MaritalStatus { MaritalStatusDescription = "Married" },
                                      new MaritalStatus { MaritalStatusDescription = "Divorced" },
                                      new MaritalStatus { MaritalStatusDescription = "Unknown" }
                                  };
            maritalStatuses.ForEach(s => context.MaritalStatuses.Add(s));
            context.SaveChanges();

            var passwordFormats = new List<PasswordFormat>
                                  {
                                      new PasswordFormat
                                      {
                                          PasswordFormatDescription = "Clear", DateCreated = DateTime.Now, DateModified = DateTime.Now
                                      },
                                      new PasswordFormat
                                      {
                                          PasswordFormatDescription = "Hashed", DateCreated = DateTime.Now, DateModified = DateTime.Now
                                      },
                                      new PasswordFormat
                                      {
                                          PasswordFormatDescription = "Encrypted", DateCreated = DateTime.Now, DateModified = DateTime.Now
                                      }
                                  };
            passwordFormats.ForEach(s => context.PasswordFormats.Add(s));
            context.SaveChanges();

            var phoneNumberTypes = new List<PhoneNumberType>
                                  {
                                      new PhoneNumberType
                                      {
                                          Description = "Home"
                                      },
                                      new PhoneNumberType
                                      {
                                          Description = "Mobile"
                                      },
                                      new PhoneNumberType
                                      {
                                          Description = "Work"
                                      }
                                  };
            phoneNumberTypes.ForEach(s => context.PhoneNumberTypes.Add(s));
            context.SaveChanges();

            var relationTypes = new List<RelationType>
                                {
                                    new RelationType { RelationTypeDescription = "Husband" },
                                    new RelationType { RelationTypeDescription = "Wife" },
                                    new RelationType { RelationTypeDescription = "Brother" },
                                    new RelationType { RelationTypeDescription = "Sister" }
                                };
            relationTypes.ForEach(s => context.RelationTypes.Add(s));
            context.SaveChanges();

            var timeStamp = DateTime.Now;
            const string password = "Welcome1";
            var passwordSalt = this.GetRandomSalt(password);
            var passwordAnswer = this.Sha256Hash("because");

            var keenanSiteAdminMembership = new Membership
            {
                DateCreated = timeStamp,
                DateModified = timeStamp,
                FailedPasswordAnswerAttemptCount = 0,
                FailedPasswordAnswerAttemptWindowStart = timeStamp,
                FailedPasswordAttemptWindowStart = timeStamp,
                IsApproved = true,
                IsLockedOut = false,
                DateLastActivity = timeStamp,
                Password = this.HashPasscode(password, passwordSalt),
                PasswordFormatId = 2,
                PasswordQuestion = "why",
                PasswordAnswer = passwordAnswer,
                PasswordSalt = passwordSalt,
                User = new User
                {
                    UserName = "kbailles",
                    DateCreated = timeStamp,
                    DateModified = timeStamp,
                    IsAnonymous = false,
                    LastActivityDate = timeStamp
                },
            };

            context.Memberships.Add(keenanSiteAdminMembership);
            context.SaveChanges();

            var profileEntity = new Profile
            {
                ReceiveEmailNotification = false,
                IsActive = true,
                IsBaptized = true,
                IsDisabled = false,
                IsLocal = true,
                IsValidated = true,
                UserId = keenanSiteAdminMembership.UserId,
                DateModified = timeStamp,
                DateCreated = timeStamp,
                Person = new Person
                {
                    Address1 = "3925 Princeton Trail",
                    City = "Eagan",
                    DateCreated = timeStamp,
                    DateModified = timeStamp,
                    DateOfBirth = DateTime.Parse("07/05/1988"),
                    EmailPrimary = "kbailles@outlook.com",
                    FirstName = "Keenan",
                    LastName = "Bailles",
                    MiddleInitial = "E",
                    MaritalStatus = null,
                    MaritalStatusId = 2,
                    PersonType = null,
                    PersonTypeId = 2,
                    State = "MN",
                    Zip = "55123",
                    Gender = "M"
                },
                User = null
            };

            context.Profiles.Add(profileEntity);
            context.SaveChanges();

            var keenanAdminRole = new UserRole
            {
                DateCreated = timeStamp,
                DateModified = timeStamp,
                RoleId = 4,
                UserId = profileEntity.UserId,
                Role = null,
                User = null
            };

            context.UserRoles.Add(keenanAdminRole);
            context.SaveChanges();
        }

        // These are just copied here so we can add some defaut users
        private string GetRandomSalt(string s)
        {
            //256 bits
            byte[] salt = new byte[32];

            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
            random.GetBytes(salt);
            return BytesToHex(salt);
        }

        private static string BytesToHex(byte[] toConvert)
        {
            StringBuilder s = new StringBuilder(toConvert.Length * 2);
            foreach (byte b in toConvert)
            {
                s.Append(b.ToString("x2"));
            }
            return s.ToString();
        }

        private string HashPasscode(string s, string salt)
        {
            string passcode = s;
            return Sha256Hex(salt + passcode);
        }

        private string Sha256Hex(string toHash)
        {
            SHA256Managed hash = new SHA256Managed();
            byte[] utf8 = Encoding.UTF8.GetBytes(toHash);
            return BytesToHex(hash.ComputeHash(utf8));
        }

        private string Sha256Hash(string s)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] dataSha256 = sha256.ComputeHash(Encoding.Default.GetBytes(s));
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < dataSha256.Length; i++)
            {
                sb.AppendFormat("{0:x2}", dataSha256[i]);
            }

            return sb.ToString();
        }
    }
}