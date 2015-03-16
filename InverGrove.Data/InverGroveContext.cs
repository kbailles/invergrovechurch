using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using InverGrove.Data.Entities;

namespace InverGrove.Data
{
    public class InverGroveContext : DbContext, IInverGroveContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InverGroveContext"/> class.
        /// </summary>
        public InverGroveContext()
            : base("InverGroveContext")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InverGroveContext"/> class.
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
        public InverGroveContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InverGroveContext"/> class.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        public InverGroveContext(DbConnection dbConnection)
            : base(dbConnection, true)
        {
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public static IInverGroveContext Create()
        {
            return new InverGroveContext();
        }

        /// <summary>
        /// Gets or sets the attendances.
        /// </summary>
        /// <value>
        /// The attendances.
        /// </value>
        public IDbSet<Attendance> Attendances { get; set; }

        /// <summary>
        /// Gets or sets the contacts.
        /// </summary>
        /// <value>
        /// The contacts.
        /// </value>
        public IDbSet<ChurchRole> ChurchRoles { get; set; }

        /// <summary>
        /// Gets or sets the contacts.
        /// </summary>
        /// <value>
        /// The contacts.
        /// </value>
        public IDbSet<Contact> Contacts { get; set; }

        /// <summary>
        /// Gets or sets the marital statuses.
        /// </summary>
        /// <value>
        /// The marital statuses.
        /// </value>
        public IDbSet<MaritalStatus> MaritalStatuses { get; set; }

        /// <summary>
        /// Gets or sets the member notes.
        /// </summary>
        /// <value>
        /// The member notes.
        /// </value>
        public IDbSet<MemberNote> MemberNotes { get; set; }

        /// <summary>
        /// Gets or sets the memberships.
        /// </summary>
        /// <value>
        /// The memberships.
        /// </value>
        public IDbSet<Membership> Memberships { get; set; }

        /// <summary>
        /// Gets or sets the notifications.
        /// </summary>
        /// <value>
        /// The notifications.
        /// </value>
        public IDbSet<UserVerification> UserVerifications { get; set; }

        /// <summary>
        /// Gets or sets the password formats.
        /// </summary>
        /// <value>
        /// The password formats.
        /// </value>
        public IDbSet<PasswordFormat> PasswordFormats { get; set; }

        /// <summary>
        /// Gets or sets the people.
        /// </summary>
        /// <value>
        /// The people.
        /// </value>
        public IDbSet<Person> People { get; set; }

        /// <summary>
        /// Gets or sets the phone numbers.
        /// </summary>
        /// <value>
        /// The phone numbers.
        /// </value>
        public IDbSet<PhoneNumber> PhoneNumbers { get; set; }

        /// <summary>
        /// Gets or sets the phone number types.
        /// </summary>
        /// <value>
        /// The phone number types.
        /// </value>
        public IDbSet<PhoneNumberType> PhoneNumberTypes { get; set; }

        /// <summary>
        /// Gets or sets the profiles.
        /// </summary>
        /// <value>
        /// The profiles.
        /// </value>
        public IDbSet<Profile> Profiles { get; set; }

        /// <summary>
        /// Gets or sets the relation types.
        /// </summary>
        /// <value>
        /// The relation types.
        /// </value>
        public IDbSet<RelationType> RelationTypes { get; set; }

        /// <summary>
        /// Gets or sets the relatives.
        /// </summary>
        /// <value>
        /// The relatives.
        /// </value>
        public IDbSet<Relative> Relatives { get; set; }

        /// <summary>
        /// Gets or sets the responsibilities.
        /// </summary>
        /// <value>
        /// The responsibilities.
        /// </value>
        public IDbSet<Responsibility> Responsibilities { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public IDbSet<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets the sermons.
        /// </summary>
        /// <value>
        /// The sermons.
        /// </value>
        public IDbSet<Sermon> Sermons { get; set; }

        /// <summary>
        /// Gets or sets the members.
        /// </summary>
        /// <value>
        /// The members.
        /// </value>
        public IDbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the user roles.
        /// </summary>
        /// <value>
        /// The user roles.
        /// </value>
        public IDbSet<UserRole> UserRoles { get; set; }

        /// <summary>
        /// Returns a IDbSet instance for access to entities of the given type in the context,
        /// the ObjectStateManager, and the underlying store.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        public new IDbSet<TEntity> Set<TEntity>()
            where TEntity : class
        {
            return base.Set<TEntity>();
        }

        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public void Attach<TEntity>(TEntity entity)
            where TEntity : class
        {
            if (this.Entry(entity).State == EntityState.Detached)
            {
                base.Set<TEntity>().Attach(entity);
            }

        }

        /// <summary>
        /// Commit all changes made in a container.
        /// </summary>
        /// <remarks>
        /// If the entity have fixed properties and any optimistic concurrency problem exists,
        /// then an exception is thrown
        /// </remarks>
        public void Commit()
        {
            base.SaveChanges();
        }

        /// <summary>
        /// Sets the modified.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public void SetModified<TEntity>(TEntity entity)
            where TEntity : class
        {
            this.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Execute specific query with underliying persistence store
        /// </summary>
        /// <typeparam name="TEntity">Entity type to map query results</typeparam>
        /// <param name="sqlQuery">Dialect Query
        /// <example>
        /// SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer &gt; {0}
        /// </example></param>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>
        /// Enumerable results
        /// </returns>
        /// <remarks>This method is virtual</remarks>
        /// <code>
        ///     context.Database.SqlQuery&lt;EntityType&gt;(
        ///     "EXEC ProcName @param1, @param2",
        ///     new SqlParameter("param1", param1),
        ///     new SqlParameter("param2", param2));
        /// </code>
        /// <code>
        ///     context.Database.SqlQuery&lt;MyEntityType&gt;("mySpName @param1 = {0}", param1)
        /// </code>
        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return this.Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }

        /// <summary>
        /// Execute arbitrary command into underliying persistence store
        /// </summary>
        /// <param name="sqlCommand">Command to execute
        /// <example>
        /// SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer &gt; {0}
        /// </example></param>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>
        /// The number of affected records
        /// </returns>
        /// <remarks>This method is virtual</remarks>
        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            if (string.IsNullOrEmpty(sqlCommand))
            {
                throw new ArgumentException("sqlCommand");
            }

            try
            {
                return this.Database.ExecuteSqlCommand(sqlCommand, parameters);
            }
            catch (SqlException)
            {
                if (this.ReThrowExceptions)
                {
                    throw;
                }
                return -1;
            }
        }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>A task that represents the asynchronous save operation.
        /// The task result contains the number of objects written to the underlying database.</returns>
        /// <throws>System.InvalidOperationException: Thrown if the context has been disposed.</throws>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported.
        /// Use 'await' to ensure that any asynchronous operations have completed before
        /// calling another method on this context.
        /// </remarks>
        public async Task<int> CommitAsync()
        {
            return await base.SaveChangesAsync();
        }

        /// <summary>
        /// Gets or sets a value indicating whether [disable proxy creation]. The default
        /// value is set to <c>true</c>
        /// </summary>
        /// <value>
        /// <c>true</c> if [disable proxy creation]; otherwise, <c>false</c>.
        /// </value>
        public bool CreateProxies
        {
            get { return this.Configuration.ProxyCreationEnabled; }
            set { this.Configuration.ProxyCreationEnabled = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [validate on save].
        /// Default value is
        /// <c>true</c>
        /// </summary>
        /// <value>
        ///   <c>true</c> if [validate on save]; otherwise, <c>false</c>.
        /// </value>
        public bool ValidateOnSave
        {
            get { return this.Configuration.ValidateOnSaveEnabled; }
            set { this.Configuration.ValidateOnSaveEnabled = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the
        /// DbChangeTracker.DetectChanges() method is called automatically
        /// by methods of System.Data.Entity.DbContext and related classes.
        /// The default value is
        /// <c>true</c>.
        /// </summary>
        /// <value>
        /// <c>true</c> if [automatic detect changes]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoDetectChanges
        {
            get { return this.Configuration.AutoDetectChangesEnabled; }
            set { this.Configuration.AutoDetectChangesEnabled = value; }
        }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous save operation.
        /// The task result contains the number of objects written to the underlying database.</returns>
        /// <throws>System.InvalidOperationException: Thrown if the context has been disposed.</throws>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported.
        /// Use 'await' to ensure that any asynchronous operations have completed before
        /// calling another method on this context.
        /// </remarks>
        public virtual async Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Calls the protected Dispose method.
        /// </summary>
        public new void Dispose()
        {
            base.Dispose();
            GC.SuppressFinalize(this);
        }

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
                .WithRequired(e => e.PasswordFormat)
                .HasForeignKey(e => e.PasswordFormatId)
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
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.GroupPhoto)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.IndividualPhoto)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.PhoneNumbers)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

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

            modelBuilder.Entity<ChurchRole>()
                .HasMany(e => e.People)
                .WithOptional(e => e.ChurchRole)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhoneNumber>()
                .Property(e => e.AreaCode)
                .IsUnicode(false);

            modelBuilder.Entity<PhoneNumber>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<PhoneNumberType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<PhoneNumberType>()
                .HasMany(e => e.PhoneNumbers)
                .WithRequired(e => e.PhoneNumberType)
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

            modelBuilder.Entity<Sermon>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Sermon>()
                .Property(e => e.Tags)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Sermons)
                .WithRequired(e => e.User)
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

        /// <summary>
        /// Gets or sets a value indicating whether to [re throw exceptions].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [re throw exceptions]; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>The default value is <c>true</c> this is here to allow for test coverage.</remarks>
        internal bool ReThrowExceptions { get; set; }
    }
}