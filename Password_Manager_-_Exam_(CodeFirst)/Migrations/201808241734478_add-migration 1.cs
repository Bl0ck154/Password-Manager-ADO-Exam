namespace Password_Manager___Exam__CodeFirst_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Url", c => c.String());
            DropColumn("dbo.Accounts", "Site");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "Site", c => c.String());
            DropColumn("dbo.Accounts", "Url");
        }
    }
}
