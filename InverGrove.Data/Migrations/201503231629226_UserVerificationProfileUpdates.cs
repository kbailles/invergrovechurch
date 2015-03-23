namespace InverGrove.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserVerificationProfileUpdates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserVerification", "DateAccessed", c => c.DateTime());
            DropColumn("dbo.Profile", "IsLocal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profile", "IsLocal", c => c.Boolean(nullable: false));
            AlterColumn("dbo.UserVerification", "DateAccessed", c => c.DateTime(nullable: false));
        }
    }
}
