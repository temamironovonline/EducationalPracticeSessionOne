using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
                _employee = _employees.FirstOrDefault(x => x.Number_Employee == phoneInput.Text.ToString());

                if (_employee != null)
                {
                    passwordInput.IsEnabled = true;
                    passwordInput.Focus();
                }
                else
                {
                    MessageBox.Show("Номер телефона не зарегистрирован", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void passwordInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (_employee.Password_Employee == passwordInput.Text.ToString())
                {
                    CaptchaFieldWindow captchaFieldWindow = new CaptchaFieldWindow();
                    captchaFieldWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Неверный пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
