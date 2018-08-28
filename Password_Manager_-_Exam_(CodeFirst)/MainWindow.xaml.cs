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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Password_Manager___Exam__CodeFirst_
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static User currentUser = null;
		public MainWindow(User user)
		{
			currentUser = user;
			InitializeComponent();
			WindowStartupLocation = WindowStartupLocation.CenterScreen; // вывод окна в центр экрана
			fillData(); // отображение таблицы с данными
			this.Title = $"Current User: {user.Login}"; // отображение имени текущего пользователя в заголовке окна
		}

		private void fillData(PasswordDatabase db = null) // ф-я отображения данных
		{
			bool toDispose = false;
			if (db == null)
			{
				db = new PasswordDatabase();
				toDispose = true;
			}
			
			DataGrid.ItemsSource = db.Accounts.Where(a => a.UserId == currentUser.Id).ToList();

			if (toDispose)
				db.Dispose();
		}

		private void Add_Click(object sender, RoutedEventArgs e) // кнопка добавления данных
		{
			EditWindow editWindow = new EditWindow();
			editWindow.Title = "Add account";
			if (editWindow.ShowDialog() == true)
				fillData();
		}

		private void Remove_Click(object sender, RoutedEventArgs e) // кнопка удаления
		{
			if (DataGrid.SelectedItems.Count <= 0)
				return;

			if (MessageBox.Show("Confirm removal", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
			{
				using (PasswordDatabase db = new PasswordDatabase())
				{
					Account account = DataGrid.SelectedItem as Account;
					db.Accounts.Remove(db.Accounts.Where(a => a.Id == account.Id).FirstOrDefault());
					db.SaveChanges();
					fillData(db);
				}
			}
		}

		private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			RemoveBtn.IsEnabled = EditBtn.IsEnabled = e.AddedItems.Count > 0 ? true : false;
		}

		private void Search_Click(object sender, RoutedEventArgs e)
		{
			using (PasswordDatabase db = new PasswordDatabase())
			{
				IQueryable<Account> queryable = db.Accounts.Where(a => a.UserId == currentUser.Id);
				if (!string.IsNullOrWhiteSpace(TitleSearch.Text))
				{
					queryable = queryable.Where(a => a.Title.Contains(TitleSearch.Text));
				}
				if (!string.IsNullOrWhiteSpace(DescrSearch.Text))
				{
					queryable = queryable.Where(a => a.Description.Contains(DescrSearch.Text));
				}
				if (!string.IsNullOrWhiteSpace(UrlSearch.Text))
				{
					queryable = queryable.Where(a => a.Url.Contains(UrlSearch.Text));
				}
				if (!string.IsNullOrWhiteSpace(LoginSearch.Text))
				{
					queryable = queryable.Where(a => a.Login.Contains(LoginSearch.Text));
				}
				DataGrid.ItemsSource = queryable.ToList();
			}
		}

		private void SearchEnterKey(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				SearchBtn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
		}

		private void DataGrid_KeyDown(object sender, KeyEventArgs e)
		{// удаление из базы по нажатию на Del
			if (e.Key == Key.Delete)
				RemoveBtn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
			else if(e.Key == Key.Enter)
				EditBtn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
		}

		private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if(DataGrid.SelectedItems.Count > 0)
				EditBtn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
		}

		private void EditBtn_Click(object sender, RoutedEventArgs e)
		{
			EditWindow editWindow = new EditWindow((Account)DataGrid.SelectedItem);
			editWindow.Title = "Edit account";
			if (editWindow.ShowDialog() == true)
				fillData();
		}

		private void StatBtn_Click(object sender, RoutedEventArgs e)
		{
			StatsWindow statsWindow = new StatsWindow();
			statsWindow.Show();
		}

		private void regBtn_Click(object sender, RoutedEventArgs e)
		{
			RegisterWindow registerWindow = new RegisterWindow();
			registerWindow.Show();
			Close();
		}
	}
}
