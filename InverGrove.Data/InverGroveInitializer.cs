﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using InverGrove.Data.Entities;

namespace InverGrove.Data
{
    public class InverGroveInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<InverGroveContext>
    {
        protected override void Seed(InverGroveContext context)
        {
            var absentReasons = new List<AbsentReason>
                          {
                              new AbsentReason
                              {
                                  Description = "Sick"
                              },
                              new AbsentReason
                              {
                                  Description = "Out of Town"
                              },
                              new AbsentReason
                              {
                                  Description = "Aiding for someone sick"
                              },
                              new AbsentReason
                              {
                                  Description = "Homebound/Unable to leave their home or care facility"
                              },
                              new AbsentReason
                              {
                                  Description = "Emergency"
                              },
                              new AbsentReason
                              {
                                  Description = "Unknown"
                              },
                              new AbsentReason
                              {
                                  Description = "Other"
                              }
                          };
            absentReasons.ForEach(s => context.AbsentReasons.Add(s));
            context.SaveChanges();

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

            var churchRoles = new List<ChurchRole>
                          {
                              new ChurchRole
                              {
                                  ChurchRoleDescription = "Deacon"
                              },
                              new ChurchRole
                              {
                                  ChurchRoleDescription = "Elder"
                              },
                              new ChurchRole
                              {
                                  ChurchRoleDescription = "Preacher"
                              }
                          };
            churchRoles.ForEach(s => context.ChurchRoles.Add(s));
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
            var passwordSalt = this.GetRandomSalt();
            var passwordAnswer = this.HashPasscode("because", passwordSalt);

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
                    LastActivityDate = timeStamp                    ,
                    MemberNotes = null,
                    Memberships = null,
                    Profiles = null,
                    Responsibilities = null,
                    Sermons = null,
                    UserRoles = null
                },
            };

            context.Memberships.Add(keenanSiteAdminMembership);
            context.SaveChanges();

            var keenanPerson = new Person
            {
                Attendances = null,
                Address1 = "3925 Princeton Trail",
                City = "Eagan",
                DateCreated = timeStamp,
                DateModified = timeStamp,
                DateOfBirth = DateTime.Parse("07/05/1988"),
                EmailPrimary = "kbailles@outlook.com",
                IsBaptized = true,
                IsMember = true,
                FirstName = "Keenan",
                LastName = "Bailles",
                MiddleInitial = "W",
                MaritalStatus = null,
                MaritalStatusId = 2,
                ChurchRole = null,
                State = "MN",
                Zip = "55123",
                Gender = "M",
                PhoneNumbers = null
            };
            context.People.Add(keenanPerson);
            context.SaveChanges();

            var profileEntity = new Profile
            {
                ReceiveEmailNotification = false,
                IsActive = true,
                IsDisabled = false,
                IsValidated = true,
                UserId = keenanSiteAdminMembership.UserId,
                PersonId = keenanPerson.PersonId,
                DateModified = timeStamp,
                DateCreated = timeStamp,
                Person = null,
                User = null
            };

            context.Profiles.Add(profileEntity);
            context.SaveChanges();

            var mobilePhoneNumberType = phoneNumberTypes.First(p => p.Description == "Mobile");
            var keenanPhoneNumber = new PhoneNumber
            {
                Phone = "9522884313",
                PersonId = keenanPerson.PersonId,
                PhoneNumberTypeId = mobilePhoneNumberType.PhoneNumberTypeId,
                Person = null,
                PhoneNumberType = null
            };

            context.PhoneNumbers.Add(keenanPhoneNumber);
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

            var sermon1 = new Sermon
                              {
                                  SermonDate = new DateTime(2014, 9, 26),
                                  SoundCloudId = 171447693,
                                  Speaker = "Antoine Halloway",
                                  Tags = "Be Bold",
                                  Title = "Boldly Respond to the Invitation",
                                  ModifiedByUserId = 1,
                                  DateCreated = timeStamp,
                                  DateModified = timeStamp,
                                  User = null
                              };
            context.Sermons.Add(sermon1);

            var sermon2 = new Sermon
                              {
                                  SermonDate = new DateTime(2014, 9, 25),
                                  SoundCloudId = 171447888,
                                  Speaker = "Antoine Halloway",
                                  Tags = "Be Bold",
                                  Title = "Boldly Trust Jesus",
                                  ModifiedByUserId = 1,
                                  DateCreated = timeStamp,
                                  DateModified = timeStamp,
                                  User = null
                              };
            context.Sermons.Add(sermon2);

            var sermon3 = new Sermon
                              {
                                  SermonDate = new DateTime(2014, 9, 24),
                                  SoundCloudId = 171447578,
                                  Speaker = "Antoine Halloway",
                                  Tags = "Be Bold",
                                  Title = "Speak Boldly",
                                  ModifiedByUserId = 1,
                                  DateCreated = timeStamp,
                                  DateModified = timeStamp,
                                  User = null
                              };
            context.Sermons.Add(sermon3);

            var sermon4 = new Sermon
                              {
                                  SermonDate = new DateTime(2014, 9, 23),
                                  SoundCloudId = 171447641,
                                  Speaker = "Antoine Halloway",
                                  Tags = "Be Bold",
                                  Title = "Boldly Believe in the Son of God",
                                  ModifiedByUserId = 1,
                                  DateCreated = timeStamp,
                                  DateModified = timeStamp,
                                  User = null
                              };
            context.Sermons.Add(sermon4);

            var sermon5 = new Sermon
                              {
                                  SermonDate = new DateTime(2014, 9, 22),
                                  SoundCloudId = 171447791,
                                  Speaker = "Antoine Halloway",
                                  Tags = "Be Bold",
                                  Title = "Boldly Seek After God",
                                  ModifiedByUserId = 1,
                                  DateCreated = timeStamp,
                                  DateModified = timeStamp,
                                  User = null
                              };
            context.Sermons.Add(sermon5);

            var sermon6 = new Sermon
                              {
                                  SermonDate = new DateTime(2014, 9, 21),
                                  SoundCloudId = 171447331,
                                  Speaker = "Antoine Halloway",
                                  Tags = "Be Bold",
                                  Title = "Biblical Boldness",
                                  ModifiedByUserId = 1,
                                  DateCreated = timeStamp,
                                  DateModified = timeStamp,
                                  User = null
                              };
            context.Sermons.Add(sermon6);

            var sermon7 = new Sermon
                              {
                                  SermonDate = new DateTime(2014, 9, 21),
                                  SoundCloudId = 171447455,
                                  Speaker = "Antoine Halloway",
                                  Tags = "Be Bold",
                                  Title = "Boldly Imitate Christ",
                                  ModifiedByUserId = 1,
                                  DateCreated = timeStamp,
                                  DateModified = timeStamp,
                                  User = null
                              };
            context.Sermons.Add(sermon7);

            var sermon8 = new Sermon
                              {
                                  SermonDate = new DateTime(2014, 9, 21),
                                  SoundCloudId = 171447516,
                                  Speaker = "Antoine Halloway",
                                  Tags = "Be Bold",
                                  Title = "Live Boldly",
                                  ModifiedByUserId = 1,
                                  DateCreated = timeStamp,
                                  DateModified = timeStamp,
                                  User = null
                              };
            context.Sermons.Add(sermon8);

            var sermon9 = new Sermon
            {
                SermonDate = new DateTime(2014, 12, 7),
                SoundCloudId = 180947413,
                Speaker = "Rennie Frazier",
                Tags = "Revelation",
                Title = "A Dead Church with a Living Hope",
                ModifiedByUserId = 1,
                DateCreated = timeStamp,
                DateModified = timeStamp,
                User = null
            };
            context.Sermons.Add(sermon9);
            context.SaveChanges();
        }

        // These are just copied here so we can add some defaut members
        private string GetRandomSalt()
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
    }
}