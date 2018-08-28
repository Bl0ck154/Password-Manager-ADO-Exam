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
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
		Account editingAccount = null;
        public EditWindow()
        {
            InitializeComponent();
			WindowStartupLocation = WindowStartupLocation.CenterScreen;
			KeyDown += EditWindow_KeyDown;
		}

		private void EditWindow_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				Submit.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
		}

		public EditWindow(Account account) : this()
		{
			editingAccount = account;
			TitleTextbox.Text = account.Title;
			DescriptionTextbox.Text = account.Description;
			URLTextbox.Text = account.Url;
			LoginTextbox.Text = account.Login;
			PasswordTextbox.Text = StringCipher.IsCrypted(account.Password) ?
				StringCipher.Decrypt(account.Password, MainWindow.currentUser.Password) 
				: account.Password;
			//PasswordTextbox.Text = StringCipher.Decrypt(account.Password, MainWindow.currentUser.Password);
		}

		private void Submit_Click(object sender, RoutedEventArgs e)
		{
			bool fields_is_empty = true;
			foreach (TextBox item in FindVisualChildren<TextBox>(this))
			{
				if(!string.IsNullOrWhiteSpace(item.Text))
				{
					fields_is_empty = false;
					break;
				}
			}
			if (fields_is_empty)
			{
				MessageBox.Show("You must fill at least one field!");
				return;
			}

			using (PasswordDatabase db = new PasswordDatabase())
			{
				Account account = editingAccount == null ? new Account() : db.Accounts.FirstOrDefault(a => a.Id == editingAccount.Id);

				account.Title = TitleTextbox.Text;
				account.Description = DescriptionTextbox.Text;
				account.Url = URLTextbox.Text;
				account.Login = LoginTextbox.Text;
				account.Password = EncryptChkbox.IsChecked == true ? 
					StringCipher.Encrypt(PasswordTextbox.Text, MainWindow.currentUser.Password)
					: PasswordTextbox.Text;
				
				if (editingAccount == null)
				{
					account.UserId = MainWindow.currentUser.Id;
					db.Accounts.Add(account);
				}

				db.SaveChanges();
				DialogResult = true;
				Close();
			}
		}
		public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
		{
			if (depObj != null)
			{
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
				{
					DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
					if (child != null && child is T)
					{
						yield return (T)child;
					}

					foreach (T childOfChild in FindVisualChildren<T>(child))
					{
						yield return childOfChild;
					}
				}
			}
		}

	}
}
