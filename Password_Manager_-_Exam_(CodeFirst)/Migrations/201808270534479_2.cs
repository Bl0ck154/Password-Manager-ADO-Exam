namespace Password_Manager___Exam__CodeFirst_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Accounts", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Accounts", "UserId");
            AddForeignKey("dbo.Accounts", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "UserId", "dbo.Users");
            DropIndex("dbo.Accounts", new[] { "UserId" });
            DropColumn("dbo.Accounts", "UserId");
            DropTable("dbo.Users");
        }
    }
}
