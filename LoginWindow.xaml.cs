using laboratory.database;
using laboratory.interfaces;
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

namespace laboratory
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, IErrorMessage, IFieldble
    {
        private string _messageErrorString = string.Empty;

        public string MessageErrorString
        {
            get { return _messageErrorString; }
            set
            {
                _messageErrorString = value;
                msgText.Text = _messageErrorString;
            }
        }

        private login _login;

        public LoginWindow()
        {
            InitializeComponent();
            logoImage.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../Resources/logo_picture.png")));
            _login = new login();
            DataContext = _login;
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Instance.IsLoginValidated(_login))
            {
                ShowMessageErrorString("Необходимо заполнить все поля!");
                return;
            }

            if (saveAuthorizationAttempt(Instance.GetContext().login.Where(p => p.login1 == _login.login1 && p.password == _login.password).ToList().Count() != 1))
            {
                ShowMessageErrorString("Не удалось авторизироваться!\nЛогин или пароль не корректны.");
                return;
            }

            Hide();
            MainWindow window = new MainWindow(_login.user);
            window.Owner = this;

            if (!(bool)window.ShowDialog())
                ClearFields();
        }

        private bool saveAuthorizationAttempt(bool attempt)
        {
            if (!Instance.GetContext().login.Select(p => p.login1).Contains(_login.login1))
                return true;

            _login.user = Instance.GetContext().user.Where(p => p.login == _login.login1).FirstOrDefault();
            if (_login.user.code_role == 4)
                return false;

            Instance.GetContext().login_history.Add(new login_history()
            {
                last_data_time = DateTime.Now,
                login = _login.login1,
                attempt = !attempt
            });
            Instance.GetContext().SaveChanges();
            Instance.Reload();

            return attempt;
        }

        private void passwordText_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _login.password = passwordText.Password;
        }

        private void loginText_TextChanged(object sender, TextChangedEventArgs e)
        {
            _login.login1 = loginText.Text;
        }

        public async void ShowMessageErrorString(string errorString)
        {
            MessageErrorString = errorString;
            await Task.Delay(5000);
            MessageErrorString = string.Empty;
        }

        public void ClearFields()
        {
            loginText.Text = string.Empty;
            passwordText.Password = string.Empty;
        }
    }
}
