using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_Manager___Exam__CodeFirst_
{
    public class User
	{
		public User()
		{

		}
		public int Id { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }

		public virtual List<Account> Accounts { get; set; }
	}
}
