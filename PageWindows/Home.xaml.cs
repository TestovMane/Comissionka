using Comissionka.Class;
using Comissionka.Model;
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

namespace Comissionka.PageWindows
{
    /// <summary>
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public int UserRole;
        public Home(int UserRoles,string Name)
        {
            InitializeComponent();
            Boba.Text = Name;
            if (UserRoles == 1) 
            {
                ClassManager.HomeFrame = HomeFrame;
                UserRole = 1;
            }
            if (UserRoles == 2)
            {
                ClassManager.HomeFrame = HomeFrame;
                OrderButt.Visibility = Visibility.Hidden;
                TovarButt.Margin = new Thickness(100,0,0,0);
                UserRole = 2;
            }
            if (UserRoles == 3)
            {
                ClassManager.HomeFrame = HomeFrame;
                UserRole = 3;
            }
        }

        private void TovarButt_Click(object sender, RoutedEventArgs e)
        {
            if (UserRole == 1)
                ClassManager.HomeFrame.Navigate(new Tovars(1));
            if (UserRole == 2)
                ClassManager.HomeFrame.Navigate(new Tovars(2));
            if (UserRole == 3)
                ClassManager.HomeFrame.Navigate(new Tovars(3));
        }

        private void OrderButt_Click(object sender, RoutedEventArgs e)
        {
            ClassManager.HomeFrame.Navigate(new Orders());
        }

        private void ReturnButt_Click(object sender, RoutedEventArgs e)
        {
            MainWindow a = new MainWindow();
            a.Show();
            this.Close();
        }
    }
}
