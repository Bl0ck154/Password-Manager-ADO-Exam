using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_Manager___Exam__CodeFirst_
{
	public class Account
	{
		public Account()
		{
			
		}
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Url { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public int UserId { get; set; }

		public virtual User User { get; set; }
	}
}
