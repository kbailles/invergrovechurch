namespace InverGrove.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AttendanceUpdate1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attendance", "UserId", "dbo.User");
            DropIndex("dbo.Attendance", new[] { "UserId" });
            AddColumn("dbo.Attendance", "PersonId", c => c.Int(nullable: false));
            AlterColumn("dbo.Attendance", "IsWednesday", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Attendance", "IsSunday", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Attendance", "IsEvening", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Attendance", "PersonId");
            AddForeignKey("dbo.Attendance", "PersonId", "dbo.Person", "PersonId");
            DropColumn("dbo.Attendance", "UserId");
        }

        public override void Down()
        {
            AddColumn("dbo.Attendance", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Attendance", "PersonId", "dbo.Person");
            DropIndex("dbo.Attendance", new[] { "PersonId" });
            AlterColumn("dbo.Attendance", "IsEvening", c => c.Boolean());
            AlterColumn("dbo.Attendance", "IsSunday", c => c.Boolean());
            AlterColumn("dbo.Attendance", "IsWednesday", c => c.Boolean());
            DropColumn("dbo.Attendance", "PersonId");
            CreateIndex("dbo.Attendance", "UserId");
            AddForeignKey("dbo.Attendance", "UserId", "dbo.User", "UserId");
        }
    }
}
