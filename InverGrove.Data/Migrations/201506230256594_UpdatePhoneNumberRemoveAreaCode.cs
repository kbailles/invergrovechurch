namespace InverGrove.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePhoneNumberRemoveAreaCode : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PhoneNumber", "Phone", c => c.String(nullable: false, maxLength: 10, unicode: false));
            DropColumn("dbo.PhoneNumber", "AreaCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PhoneNumber", "AreaCode", c => c.String(nullable: false, maxLength: 3, unicode: false));
            AlterColumn("dbo.PhoneNumber", "Phone", c => c.String(nullable: false, maxLength: 7, unicode: false));
        }
    }
}
