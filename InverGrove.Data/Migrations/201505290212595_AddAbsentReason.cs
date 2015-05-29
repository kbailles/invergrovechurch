namespace InverGrove.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAbsentReason : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AbsentReason",
                c => new
                    {
                        AbsentReasonId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => t.AbsentReasonId);
            
            AddColumn("dbo.Attendance", "IsAbsent", c => c.Boolean(nullable: false));
            AddColumn("dbo.Attendance", "AbsentReasonId", c => c.Int(nullable: false));
            CreateIndex("dbo.Attendance", "AbsentReasonId");
            AddForeignKey("dbo.Attendance", "AbsentReasonId", "dbo.AbsentReason", "AbsentReasonId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendance", "AbsentReasonId", "dbo.AbsentReason");
            DropIndex("dbo.Attendance", new[] { "AbsentReasonId" });
            DropColumn("dbo.Attendance", "AbsentReasonId");
            DropColumn("dbo.Attendance", "IsAbsent");
            DropTable("dbo.AbsentReason");
        }
    }
}
