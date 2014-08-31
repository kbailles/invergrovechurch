using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using InverGrove.Data.Entities;

namespace InverGrove.Data
{
    public class InverGroveContext : DbContext
    {
        public InverGroveContext()
            : base("InverGroveContext")
        {
        }

        public InverGroveContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public InverGroveContext(DbConnection dbConnection)
            : base(dbConnection, true)
        {
        }

        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public DbSet<MemberNote> MemberNotes { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<PasswordFormat> PasswordFormats { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<PersonType> PersonTypes { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<RelationType> RelationTypes { get; set; }
        public DbSet<Relative> Relatives { get; set; }
        public DbSet<Responsibility> Responsibilities { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Contact>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Zip)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<MaritalStatus>()
                .Property(e => e.MaritalStatusDescription)
                .IsUnicode(false);

            modelBuilder.Entity<MaritalStatus>()
                .HasMany(e => e.People)
                .WithRequired(e => e.MaritalStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MemberNote>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<Membership>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Membership>()
                .Property(e => e.PasswordSalt)
                .IsUnicode(false);

            modelBuilder.Entity<Membership>()
                .Property(e => e.PasswordQuestion)
                .IsUnicode(false);

            modelBuilder.Entity<Membership>()
                .Property(e => e.PasswordAnswer)
                .IsUnicode(false);

            modelBuilder.Entity<PasswordFormat>()
                .Property(e => e.PasswordFormatDescription)
                .IsUnicode(false);

            modelBuilder.Entity<PasswordFormat>()
                .HasMany(e => e.Memberships)
                .WithRequired(e => e.PasswordFormat1)
                .HasForeignKey(e => e.PasswordFormat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.MiddleInitial)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.Address1)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.Address2)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.Zip)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.EmailPrimary)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.EmailSecondary)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.PhonePrimary)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.PhoneSecondary)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.GroupPhoto)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.IndividualPhoto)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Profiles)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
               .HasMany(e => e.Relatives)
               .WithRequired(e => e.Person)
               .HasForeignKey(e => e.PersonA)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Relatives1)
                .WithRequired(e => e.Person1)
                .HasForeignKey(e => e.PersonB)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PersonType>()
                .HasMany(e => e.People)
                .WithRequired(e => e.PersonType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RelationType>()
                .HasMany(e => e.Relatives)
                .WithRequired(e => e.RelationType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Responsibility>()
                .Property(e => e.Activity)
                .IsUnicode(false);

            modelBuilder.Entity<Responsibility>()
                .Property(e => e.ShortDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Responsibility>()
                .Property(e => e.LongDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Responsibility>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Responsibilities)
                .Map(m => m.ToTable("UserResponsibilities").MapLeftKey("ResponsibilitiesId").MapRightKey("UserId"));

            modelBuilder.Entity<Role>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Attendances)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.MemberNotes)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Memberships)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Profiles)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}