namespace InverGrove.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendance",
                c => new
                    {
                        AttendanceId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        DateAttended = c.DateTime(nullable: false),
                        IsWednesday = c.Boolean(),
                        IsSunday = c.Boolean(),
                        IsEvening = c.Boolean(),
                    })
                .PrimaryKey(t => t.AttendanceId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        LastActivityDate = c.DateTime(nullable: false),
                        IsAnonymous = c.Boolean(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256, unicode: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);

            CreateTable(
                "dbo.MemberNote",
                c => new
                    {
                        PersonNotesId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Note = c.String(maxLength: 500, unicode: false),
                        DateOfNote = c.DateTime(),
                    })
                .PrimaryKey(t => t.PersonNotesId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.Membership",
                c => new
                    {
                        MembershipId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        DateLastLogin = c.DateTime(),
                        DateLockedOut = c.DateTime(),
                        FailedPasswordAnswerAttemptCount = c.Int(nullable: false),
                        FailedPasswordAnswerAttemptWindowStart = c.DateTime(nullable: false),
                        FailedPasswordAttemptCount = c.Int(nullable: false),
                        FailedPasswordAttemptWindowStart = c.DateTime(nullable: false),
                        IsLockedOut = c.Boolean(nullable: false),
                        Password = c.String(nullable: false, maxLength: 128, unicode: false),
                        PasswordSalt = c.String(nullable: false, maxLength: 128, unicode: false),
                        PasswordFormatId = c.Int(nullable: false),
                        PasswordQuestion = c.String(nullable: false, maxLength: 64, unicode: false),
                        PasswordAnswer = c.String(nullable: false, maxLength: 64, unicode: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        DateLastActivity = c.DateTime(),
                        IsApproved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MembershipId)
                .ForeignKey("dbo.PasswordFormat", t => t.PasswordFormatId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.PasswordFormatId);

            CreateTable(
                "dbo.PasswordFormat",
                c => new
                    {
                        PasswordFormatId = c.Int(nullable: false, identity: true),
                        PasswordFormatDescription = c.String(nullable: false, maxLength: 64, unicode: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PasswordFormatId);

            CreateTable(
                "dbo.Profile",
                c => new
                    {
                        ProfileId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ReceiveEmailNotification = c.Boolean(nullable: false),
                        PersonId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDisabled = c.Boolean(nullable: false),
                        IsValidated = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProfileId)
                .ForeignKey("dbo.Person", t => t.PersonId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.PersonId);

            CreateTable(
                "dbo.Person",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50, unicode: false),
                        LastName = c.String(nullable: false, maxLength: 50, unicode: false),
                        MiddleInitial = c.String(maxLength: 5, unicode: false),
                        Address1 = c.String(maxLength: 200, unicode: false),
                        Address2 = c.String(maxLength: 100, unicode: false),
                        City = c.String(maxLength: 100, unicode: false),
                        State = c.String(maxLength: 2, unicode: false),
                        Zip = c.String(maxLength: 10, unicode: false),
                        EmailPrimary = c.String(maxLength: 254, unicode: false),
                        EmailSecondary = c.String(maxLength: 254, unicode: false),
                        DateOfBirth = c.DateTime(storeType: "date"),
                        Gender = c.String(nullable: false, maxLength: 1, unicode: false),
                        GroupPhoto = c.String(maxLength: 100, unicode: false),
                        IndividualPhoto = c.String(maxLength: 100, unicode: false),
                        IsBaptized = c.Boolean(nullable: false),
                        IsMember = c.Boolean(nullable: false),
                        IsVisitor = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        MaritalStatusId = c.Int(nullable: false),
                        ChurchRoleId = c.Int(),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.ChurchRole", t => t.ChurchRoleId)
                .ForeignKey("dbo.MaritalStatus", t => t.MaritalStatusId)
                .Index(t => t.MaritalStatusId)
                .Index(t => t.ChurchRoleId);

            CreateTable(
                "dbo.ChurchRole",
                c => new
                    {
                        ChurchRoleId = c.Int(nullable: false, identity: true),
                        ChurchRoleDescription = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ChurchRoleId);

            CreateTable(
                "dbo.MaritalStatus",
                c => new
                    {
                        MaritalStatusId = c.Int(nullable: false, identity: true),
                        MaritalStatusDescription = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.MaritalStatusId);

            CreateTable(
                "dbo.PhoneNumber",
                c => new
                    {
                        PhoneNumberId = c.Int(nullable: false, identity: true),
                        AreaCode = c.String(nullable: false, maxLength: 3, unicode: false),
                        Phone = c.String(nullable: false, maxLength: 7, unicode: false),
                        PersonId = c.Int(nullable: false),
                        PhoneNumberTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PhoneNumberId)
                .ForeignKey("dbo.PhoneNumberType", t => t.PhoneNumberTypeId)
                .ForeignKey("dbo.Person", t => t.PersonId)
                .Index(t => t.PersonId)
                .Index(t => t.PhoneNumberTypeId);

            CreateTable(
                "dbo.PhoneNumberType",
                c => new
                    {
                        PhoneNumberTypeId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 25, unicode: false),
                    })
                .PrimaryKey(t => t.PhoneNumberTypeId);

            CreateTable(
                "dbo.Relative",
                c => new
                    {
                        RelativesId = c.Int(nullable: false, identity: true),
                        PersonA = c.Int(nullable: false),
                        PersonB = c.Int(nullable: false),
                        RelationTypeId = c.Int(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RelativesId)
                .ForeignKey("dbo.RelationType", t => t.RelationTypeId)
                .ForeignKey("dbo.Person", t => t.PersonA)
                .ForeignKey("dbo.Person", t => t.PersonB)
                .Index(t => t.PersonA)
                .Index(t => t.PersonB)
                .Index(t => t.RelationTypeId);

            CreateTable(
                "dbo.RelationType",
                c => new
                    {
                        RelationTypeId = c.Int(nullable: false, identity: true),
                        RelationTypeDescription = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.RelationTypeId);

            CreateTable(
                "dbo.Responsibility",
                c => new
                    {
                        ResponsibilitiesId = c.Int(nullable: false, identity: true),
                        IsAssemblyDuty = c.Boolean(),
                        IsMaleOnly = c.Boolean(),
                        Activity = c.String(maxLength: 100, unicode: false),
                        ShortDescription = c.String(nullable: false, maxLength: 100, unicode: false),
                        LongDescription = c.String(nullable: false, maxLength: 250, unicode: false),
                        LastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ResponsibilitiesId);

            CreateTable(
                "dbo.Sermon",
                c => new
                    {
                        SermonId = c.Int(nullable: false, identity: true),
                        SermonDate = c.DateTime(nullable: false),
                        Speaker = c.String(maxLength: 75),
                        SoundCloudId = c.Int(nullable: false),
                        Tags = c.String(maxLength: 128, unicode: false),
                        Title = c.String(nullable: false, maxLength: 128, unicode: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        ModifiedByUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SermonId)
                .ForeignKey("dbo.User", t => t.ModifiedByUserId)
                .Index(t => t.ModifiedByUserId);

            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Role", t => t.RoleId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 64, unicode: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId);

            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        ContactsId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250, unicode: false),
                        Address = c.String(maxLength: 250, unicode: false),
                        City = c.String(maxLength: 100, unicode: false),
                        State = c.String(maxLength: 4, unicode: false),
                        Zip = c.String(maxLength: 12, unicode: false),
                        Email = c.String(maxLength: 50, unicode: false),
                        Phone = c.String(maxLength: 20, unicode: false),
                        IsVisitorCard = c.Boolean(nullable: false),
                        IsOnlineContactForm = c.Boolean(nullable: false),
                        Subject = c.String(maxLength: 50),
                        Comments = c.String(storeType: "ntext"),
                        DateSubmitted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ContactsId);

            CreateTable(
                "dbo.UserResponsibilities",
                c => new
                    {
                        ResponsibilitiesId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ResponsibilitiesId, t.UserId })
                .ForeignKey("dbo.Responsibility", t => t.ResponsibilitiesId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ResponsibilitiesId)
                .Index(t => t.UserId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.Sermon", "ModifiedByUserId", "dbo.User");
            DropForeignKey("dbo.UserResponsibilities", "UserId", "dbo.User");
            DropForeignKey("dbo.UserResponsibilities", "ResponsibilitiesId", "dbo.Responsibility");
            DropForeignKey("dbo.Profile", "UserId", "dbo.User");
            DropForeignKey("dbo.Relative", "PersonB", "dbo.Person");
            DropForeignKey("dbo.Relative", "PersonA", "dbo.Person");
            DropForeignKey("dbo.Relative", "RelationTypeId", "dbo.RelationType");
            DropForeignKey("dbo.Profile", "PersonId", "dbo.Person");
            DropForeignKey("dbo.PhoneNumber", "PersonId", "dbo.Person");
            DropForeignKey("dbo.PhoneNumber", "PhoneNumberTypeId", "dbo.PhoneNumberType");
            DropForeignKey("dbo.Person", "MaritalStatusId", "dbo.MaritalStatus");
            DropForeignKey("dbo.Person", "ChurchRoleId", "dbo.ChurchRole");
            DropForeignKey("dbo.Membership", "UserId", "dbo.User");
            DropForeignKey("dbo.Membership", "PasswordFormatId", "dbo.PasswordFormat");
            DropForeignKey("dbo.MemberNote", "UserId", "dbo.User");
            DropForeignKey("dbo.Attendance", "UserId", "dbo.User");
            DropIndex("dbo.UserResponsibilities", new[] { "UserId" });
            DropIndex("dbo.UserResponsibilities", new[] { "ResponsibilitiesId" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.Sermon", new[] { "ModifiedByUserId" });
            DropIndex("dbo.Relative", new[] { "RelationTypeId" });
            DropIndex("dbo.Relative", new[] { "PersonB" });
            DropIndex("dbo.Relative", new[] { "PersonA" });
            DropIndex("dbo.PhoneNumber", new[] { "PhoneNumberTypeId" });
            DropIndex("dbo.PhoneNumber", new[] { "PersonId" });
            DropIndex("dbo.Person", new[] { "ChurchRoleId" });
            DropIndex("dbo.Person", new[] { "MaritalStatusId" });
            DropIndex("dbo.Profile", new[] { "PersonId" });
            DropIndex("dbo.Profile", new[] { "UserId" });
            DropIndex("dbo.Membership", new[] { "PasswordFormatId" });
            DropIndex("dbo.Membership", new[] { "UserId" });
            DropIndex("dbo.MemberNote", new[] { "UserId" });
            DropIndex("dbo.Attendance", new[] { "UserId" });
            DropTable("dbo.UserResponsibilities");
            DropTable("dbo.Contact");
            DropTable("dbo.Role");
            DropTable("dbo.UserRole");
            DropTable("dbo.Sermon");
            DropTable("dbo.Responsibility");
            DropTable("dbo.RelationType");
            DropTable("dbo.Relative");
            DropTable("dbo.PhoneNumberType");
            DropTable("dbo.PhoneNumber");
            DropTable("dbo.MaritalStatus");
            DropTable("dbo.ChurchRole");
            DropTable("dbo.Person");
            DropTable("dbo.Profile");
            DropTable("dbo.PasswordFormat");
            DropTable("dbo.Membership");
            DropTable("dbo.MemberNote");
            DropTable("dbo.User");
            DropTable("dbo.Attendance");
        }
    }
}
