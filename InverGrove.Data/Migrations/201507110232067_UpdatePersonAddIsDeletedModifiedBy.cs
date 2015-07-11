namespace InverGrove.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePersonAddIsDeletedModifiedBy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Person", "ModifiedByUserId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Person", "ModifiedByUserId");
            DropColumn("dbo.Person", "IsDeleted");
        }
    }
}
