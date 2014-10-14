using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            // -------------------------------------------------- start ENTITY user
            // Assign Lance, Heidi, and Keenan SiteAdmin
            var users = new List<User>
                           {
                               new User
                               {
                                    UserName = "lancebailles@hotmail.com",
                                    DateCreated = DateTime.Now,
                                    DateModified = DateTime.Now /* ,
                                    UserRoles = new List<UserRole>().Add("SiteAdmin") */
                                

                               },
                           };

            roles.ForEach(s => context.Roles.Add(s));
            context.SaveChanges();

            // -------------------------------------------------- ENTIGY USER end


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

            var relationTypes = new List<RelationType>
                                {
                                    new RelationType { RelationTypeDescription = "Husband" },
                                    new RelationType { RelationTypeDescription = "Wife" },
                                    new RelationType { RelationTypeDescription = "Brother" },
                                    new RelationType { RelationTypeDescription = "Sister" }
                                };
            relationTypes.ForEach(s => context.RelationTypes.Add(s));
            context.SaveChanges();
        }
    }
}