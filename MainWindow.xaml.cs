using Comissionka.Model;
using Comissionka.PageWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
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

namespace Comissionka
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<User> userObj;
        string FIO;
        public MainWindow()
        {
            InitializeComponent();
            userObj = ComissiyaEntities.GetContext().User.ToList();
        }

        private void EnterButt_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = userObj.Where(user => user.UserLogin == Logintxt.Text && user.UserPassword == Passtxt.Password).FirstOrDefault();
            FIO = $"Здравствуйте! {currentUser.UserSurname} {currentUser.UserName} {currentUser.UserPatronymic}";
            if (currentUser != null)
            {
                if (currentUser.UserRole == 1)
                {
                    Home a = new Home(1, FIO);
                    a.Show();
                    this.Close();
                }
                else if (currentUser.UserRole == 2)
                {
                    Home a = new Home(2, FIO);
                    a.Show();
                    this.Close();
                }
                else
                {
                    Home a = new Home(3, FIO);
                    a.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Введите правильные данные!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void GuestEnterButt_Click(object sender, RoutedEventArgs e)
        {
            Home a = new Home(2, FIO);
            a.Show();
            this.Close();
        }

        private void ExitButt_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Logintxt.Text = "Ivan@mail.ru";
            Passtxt.Password = "lhfvb74";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Logintxt.Text = "Roshalka@mail.ru";
            Passtxt.Password = "spjdknpigsud21";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Logintxt.Text = "GjoulzxcMertv@gmail.com";
            Passtxt.Password = "Daniilda21!333z";
        }
    }
}
