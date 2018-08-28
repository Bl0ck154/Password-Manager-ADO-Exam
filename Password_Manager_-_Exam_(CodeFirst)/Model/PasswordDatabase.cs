namespace Password_Manager___Exam__CodeFirst_
{
	using System;
	using System.Data.Entity;
	using System.Linq;

	public class PasswordDatabase : DbContext
	{
		public PasswordDatabase()
			: base("name=PasswordDatabase")
		{
		}

		public DbSet<Account> Accounts { get; set; }
		public DbSet<User> Users { get; set; }
	}

	//public class MyEntity
	//{
	//    public int Id { get; set; }
	//    public string Name { get; set; }
	//}
}