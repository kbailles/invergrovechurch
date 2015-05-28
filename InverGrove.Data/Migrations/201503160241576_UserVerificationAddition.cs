namespace InverGrove.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UserVerificationAddition : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserVerification",
                c => new
                    {
                        UserVerificationId = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        Identifier = c.Guid(nullable: false),
                        DateSent = c.DateTime(nullable: false),
                        DateAccessed = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserVerificationId);
        }

        public override void Down()
        {
            DropTable("dbo.UserVerification");
        }
    }
}
