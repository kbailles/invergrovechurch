using System.Data.Entity;
using InverGrove.Data.Entities;

namespace InverGrove.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInverGroveContext : IQueryableDataContext
    {
        /// <summary>
        /// Gets or sets the attendances.
        /// </summary>
        /// <value>
        /// The attendances.
        /// </value>
        IDbSet<Attendance> Attendances { get; set; }

        /// <summary>
        /// Gets or sets the contacts.
        /// </summary>
        /// <value>
        /// The contacts.
        /// </value>
        IDbSet<Contact> Contacts { get; set; }

        /// <summary>
        /// Gets or sets the marital statuses.
        /// </summary>
        /// <value>
        /// The marital statuses.
        /// </value>
        IDbSet<MaritalStatus> MaritalStatuses { get; set; }

        /// <summary>
        /// Gets or sets the member notes.
        /// </summary>
        /// <value>
        /// The member notes.
        /// </value>
        IDbSet<MemberNote> MemberNotes { get; set; }

        /// <summary>
        /// Gets or sets the memberships.
        /// </summary>
        /// <value>
        /// The memberships.
        /// </value>
        IDbSet<Membership> Memberships { get; set; }

        /// <summary>
        /// Gets or sets the password formats.
        /// </summary>
        /// <value>
        /// The password formats.
        /// </value>
        IDbSet<PasswordFormat> PasswordFormats { get; set; }

        /// <summary>
        /// Gets or sets the people.
        /// </summary>
        /// <value>
        /// The people.
        /// </value>
        IDbSet<Person> People { get; set; }

        /// <summary>
        /// Gets or sets the person types.
        /// </summary>
        /// <value>
        /// The person types.
        /// </value>
        IDbSet<PersonType> PersonTypes { get; set; }

        /// <summary>
        /// Gets or sets the profiles.
        /// </summary>
        /// <value>
        /// The profiles.
        /// </value>
        IDbSet<Profile> Profiles { get; set; }

        /// <summary>
        /// Gets or sets the relation types.
        /// </summary>
        /// <value>
        /// The relation types.
        /// </value>
        IDbSet<RelationType> RelationTypes { get; set; }

        /// <summary>
        /// Gets or sets the relatives.
        /// </summary>
        /// <value>
        /// The relatives.
        /// </value>
        IDbSet<Relative> Relatives { get; set; }

        /// <summary>
        /// Gets or sets the responsibilities.
        /// </summary>
        /// <value>
        /// The responsibilities.
        /// </value>
        IDbSet<Responsibility> Responsibilities { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        IDbSet<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets the sermons.
        /// </summary>
        /// <value>
        /// The sermons.
        /// </value>
        IDbSet<Sermon> Sermons { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        IDbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the user roles.
        /// </summary>
        /// <value>
        /// The user roles.
        /// </value>
        IDbSet<UserRole> UserRoles { get; set; }
    }
}