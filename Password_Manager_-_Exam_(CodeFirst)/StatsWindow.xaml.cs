using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Password_Manager___Exam__CodeFirst_
{
    /// <summary>
    /// Interaction logic for Stats.xaml
    /// </summary>
    public partial class StatsWindow : Window
    {
        public StatsWindow()
        {
            InitializeComponent();
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			using (PasswordDatabase db = new PasswordDatabase())
			{
				DataGrid.ItemsSource = (from account in db.Accounts
										where account.UserId == MainWindow.currentUser.Id
										group account by account.Login into c
										orderby c.Count() descending
										select new { Login = c.Key, Count = c.Count() }).ToList();
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			using (PasswordDatabase db = new PasswordDatabase())
			{
				DataGrid.ItemsSource = (from account in db.Accounts
										where account.UserId == MainWindow.currentUser.Id
										group account by account.Password into c
										orderby c.Count() descending
										select new { Login = c.Key, Count = c.Count() }).ToList();
			}
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			using (PasswordDatabase db = new PasswordDatabase())
			{
				DataGrid.ItemsSource = (from account in db.Accounts
										where account.UserId == MainWindow.currentUser.Id
										group account by new { account.Login, account.Password } into c
										orderby c.Count() descending
										select new { c.Key.Login, c.Key.Password, Count = c.Count() }).ToList();
			}
		}
	}
}
