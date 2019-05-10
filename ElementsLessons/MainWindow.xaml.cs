using DataAccess;
using Models;
using Services;
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


namespace ElementsLessons
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SignRegButtonClick(object sender, RoutedEventArgs e)
        {
            var login = loginTextBox.Text;
            var password = passwordBox.Password;
            if(string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                {
                MessageBox.Show("Заполните все поля");
                return;
            }

            if (SignIn.IsChecked == true) // вход
            {
                using (var context = new SecurityContext())
                {
                    var users = context.Users.Where(searchingUser => searchingUser.Login == login).ToList();
                    User find = users.FirstOrDefault();

                    if (users.Count != 0)
                    {
                        if (DataEncryptor.IsValidPassword(password, find.Password))
                        {
                            MessageBox.Show($"\n\tПароль введен правильно.");
                        }
                        else
                        {
                            MessageBox.Show($"\n\tПароль введен неправильно.");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"\n\tЛогин пользователя не найден!");
                        return;
                    }
                }
            }
            else  // регистрация
            {
                using (var context = new SecurityContext())
                {
                    var users = context.Users.Where(searchingUser => searchingUser.Login == login).ToList();

                    if (users.Count != 0)
                    {
                        MessageBox.Show($"\n\tТакой пользователь уже зарегистрирован в базе данных.");
                        return;
                    }
                    else
                    {
                        context.Users.Add(new User
                        {
                            Login = login,
                            Password = DataEncryptor.HashPassword(password)
                        });
                        context.SaveChanges();
                        MessageBox.Show($"\n\tИнформация о пользователе добавлена.");
                    }
                }
            }
        }

        private void SignIn_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                signRegButton.Content = "Войти";
            }
            catch
            {

            }
        }
        private void Registration_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                signRegButton.Content = "Зарегистрироваться";
            }
            catch
            {

            }
        }

    }
}
