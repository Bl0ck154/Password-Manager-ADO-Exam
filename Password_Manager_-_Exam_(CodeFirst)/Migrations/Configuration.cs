namespace Password_Manager___Exam__CodeFirst_.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Password_Manager___Exam__CodeFirst_.PasswordDatabase>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Password_Manager___Exam__CodeFirst_.PasswordDatabase";
        }

        protected override void Seed(Password_Manager___Exam__CodeFirst_.PasswordDatabase context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
