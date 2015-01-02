using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using InverGrove.Data.Entities;

namespace InverGrove.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<InverGroveContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(InverGroveContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.

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

            roles.ForEach(s => context.Roles.AddOrUpdate(r => r.Description, s));
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
            personTypes.ForEach(s => context.PersonTypes.AddOrUpdate(p => p.PersonTypeDescription, s));
            context.SaveChanges();

            var maritalStatuses = new List<MaritalStatus>
                                  {
                                      new MaritalStatus { MaritalStatusDescription = "Single - Never Married"},
                                      new MaritalStatus { MaritalStatusDescription = "Married" },
                                      new MaritalStatus { MaritalStatusDescription = "Divorced" },
                                      new MaritalStatus { MaritalStatusDescription = "Unknown" }
                                  };
            maritalStatuses.ForEach(s => context.MaritalStatuses.AddOrUpdate(m => m.MaritalStatusDescription, s));
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
            passwordFormats.ForEach(s => context.PasswordFormats.AddOrUpdate(p => p.PasswordFormatDescription, s));
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
            phoneNumberTypes.ForEach(s => context.PhoneNumberTypes.AddOrUpdate(p => p.Description, s));
            context.SaveChanges();

            var relationTypes = new List<RelationType>
                                {
                                    new RelationType { RelationTypeDescription = "Husband" },
                                    new RelationType { RelationTypeDescription = "Wife" },
                                    new RelationType { RelationTypeDescription = "Brother" },
                                    new RelationType { RelationTypeDescription = "Sister" }
                                };
            relationTypes.ForEach(s => context.RelationTypes.AddOrUpdate(r => r.RelationTypeDescription, s));
            context.SaveChanges();

            var timeStamp = DateTime.Now;
            var keenanUser = new User
                             {
                                 UserName = "kbailles",
                                 DateCreated = timeStamp,
                                 DateModified = timeStamp,
                                 IsAnonymous = false,
                                 LastActivityDate = timeStamp
                             };
            context.Users.AddOrUpdate(u => u.UserName, keenanUser);

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
                UserId = keenanUser.UserId,
                User = null
            };

            context.Memberships.AddOrUpdate(m => m.UserId, keenanSiteAdminMembership);
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
                    MiddleInitial = "W",
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

            context.Profiles.AddOrUpdate(p => p.UserId, profileEntity);
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

            context.UserRoles.AddOrUpdate(s => new { s.UserId, s.RoleId }, keenanAdminRole);
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
            context.Sermons.AddOrUpdate(s => s.SoundCloudId, sermon1);

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
            context.Sermons.AddOrUpdate(s => s.SoundCloudId, sermon2);

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
            context.Sermons.AddOrUpdate(s => s.SoundCloudId, sermon3);

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
            context.Sermons.AddOrUpdate(s => s.SoundCloudId, sermon4);

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
            context.Sermons.AddOrUpdate(s => s.SoundCloudId, sermon5);

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
            context.Sermons.AddOrUpdate(s => s.SoundCloudId, sermon6);

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
            context.Sermons.AddOrUpdate(s => s.SoundCloudId, sermon7);

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
            context.Sermons.AddOrUpdate(s => s.SoundCloudId, sermon8);

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
            context.Sermons.AddOrUpdate(s => s.SoundCloudId, sermon9);
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
