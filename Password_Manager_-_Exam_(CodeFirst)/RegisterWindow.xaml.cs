using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
			WindowStartupLocation = WindowStartupLocation.CenterScreen; // вывод окна в центр экрана
			PreviewKeyDown += RegisterWindow_PreviewKeyDown;
			loginTxtbox.Focus(); // фокус на поле логина, для быстрого ввода данных
		}

		private void RegisterWindow_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			// вход по Enter
			if(e.Key == Key.Enter) 
				SubmitBtn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			// проверка на заполненность полей
			if(!string.IsNullOrWhiteSpace(loginTxtbox.Text) && !string.IsNullOrWhiteSpace(passwordTxtbox.Password))
			{
				using (PasswordDatabase db = new PasswordDatabase())
				{
					// ищем юзера в базе по логину
					User user = db.Users.FirstOrDefault(u => u.Login.ToLower() == loginTxtbox.Text.ToLower());
					if(user == null) // если не находим, то предлагаем зарегистрировать
					{
						if(MessageBox.Show("User not found.\nDo you want to register?",
							"User not found", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
						{
							// если пользователь согласился на регистрацию, то добавляем в базу
							user = db.Users.Add(new User
							{
								Login = loginTxtbox.Text,
								Password = StringCipher.Encrypt(passwordTxtbox.Password)
							});
							db.SaveChanges();
						}
					}
					else // если юзер найдет, то проверяем пароль
					{
						if(StringCipher.Decrypt(user.Password) != passwordTxtbox.Password)
						{
							MessageBox.Show("Password is wrong!",
							"Wrong password!", MessageBoxButton.OK);
							user = null;
						}
					}

					// если юзер не null, значит он есть в базе
					if (user != null)
					{
						// выполняем вход
						MainWindow mainWindow = new MainWindow(user);
						mainWindow.Show(); // открываем главное окно программы
						Close(); // закрываем окна входа
					}
				}
			}
		}

		private void ExitBTn_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
