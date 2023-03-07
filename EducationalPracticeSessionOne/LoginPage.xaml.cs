using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace EducationalPracticeSessionOne
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private List<Employees> _employees = MainWindow.entities.Employees.ToList();
        private Employees _employee;

        private void phoneInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(Convert.ToChar(e.Key)))
            {
                e.Handled = true;
            }

            if (e.Key == Key.Enter)
            {
                _employee = _employees.FirstOrDefault(x => x.Number_Employee == numberInput.Text.ToString());

                if (_employee != null)
                {
                    numberInput.IsEnabled = false;
                    passwordInput.IsEnabled = true;
                    passwordInput.Focus();
                }
                else
                {
                    MessageBox.Show("Номер телефона не зарегистрирован", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private string _checkCode = "";
        private DispatcherTimer _timer = new DispatcherTimer();

        private void passwordInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (_employee.Password_Employee == passwordInput.Text.ToString())
                {
                    passwordInput.IsEnabled = false;
                    GetCode();
                }
                else
                {
                    MessageBox.Show("Неверный пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void GetCode()
        {
            CaptchaFieldWindow captchaFieldWindow = new CaptchaFieldWindow();
            captchaFieldWindow.ShowDialog();

            _checkCode = CaptchaFieldWindow.getCode();

            _timer.Interval = new TimeSpan(0, 0, 10);
            _timer.Tick += new EventHandler(Timer_Tick);
            _timer.Start();

            enterButton.IsEnabled = true;
            codeInput.IsEnabled = true;
            codeInput.Focus();

            if (refreshButton.IsEnabled)
            {
                refreshButton.IsEnabled = false;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            MessageBox.Show("Время вышло. Нажмите на кнопку повторной генерации кода.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            codeInput.Text = "";
            codeInput.IsEnabled = false;
            refreshButton.IsEnabled = true;
            enterButton.IsEnabled = false;
            _timer.Stop();
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            GetCode();
        }

        private void codeInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CheckCode();
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            numberInput.Text = "";
            numberInput.IsEnabled = true;

            passwordInput.Text = "";
            passwordInput.IsEnabled = false;

            codeInput.Text = "";
            codeInput.IsEnabled = false;

            refreshButton.IsEnabled = false;
            enterButton.IsEnabled = false;

            _timer.Stop();
        }

        private void CheckCode()
        {
            MessageBox.Show($"{_checkCode}");
            if (codeInput.Text == _checkCode)
            {
                _timer.Stop();
                Employee_Roles role = MainWindow.entities.Employee_Roles.FirstOrDefault(x => x.Code_Role == _employee.Code_Role);
                MessageBox.Show($"Поздравляем, товарищ {_employee.Number_Employee}\nВаш уровень доступа: {role.Name_Role}\nУдачного дня!", "Доступ разрешен", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _timer.Stop();
                MessageBox.Show($"Неверный код. Нажмите на кнопку повторной генерации кода.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                refreshButton.IsEnabled = true;
                codeInput.Text = "";
            }
        }

        private void enterButton_Click(object sender, RoutedEventArgs e)
        {
            CheckCode();
        }
    }
}
