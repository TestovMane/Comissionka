using Comissionka.Class;
using Comissionka.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для AddTovar.xaml
    /// </summary>
    public partial class AddTovar : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Tovar _currentTovar = new Tovar();
        public AddTovar(Tovar _selectedTovar)
        {
            InitializeComponent();
            CategoriyaComboBox.ItemsSource = ComissiyaEntities.GetContext().TovarCategory.ToList();
            if (_selectedTovar != null)
                _currentTovar = _selectedTovar;
            DataContext = _currentTovar;
        }

        private void Invalidate()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("_currentTovar"));
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("_currentTovar"));
        }

        private void ImageButt_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog getImageDialog = new OpenFileDialog();
            getImageDialog.Filter = "Файлы изображений: (*.png, *.jpg, *.jpeg) | *.png; *.jpg; *.jpeg";
            if (getImageDialog.ShowDialog() == true)
            {
                _currentTovar.TovarPhoto = getImageDialog.FileName.Substring(Environment.CurrentDirectory.Length - 10);
                Invalidate();
            }
        }

        private void SaveButt_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (NameTovar.Text == "")
                errors.AppendLine("Введите наименование");
            if (SellerTextBox.Text == "")
                errors.AppendLine("Введите продавца");
            if (Description.Text == "")
                errors.AppendLine("Введите описание");
            if (Cost.Text == "")
                errors.AppendLine("Введите стоимость");
            if (CategoriyaComboBox.SelectedIndex < 0)
                errors.AppendLine("Выберите категорию");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if (_currentTovar.TovarID == 0)
                    ComissiyaEntities.GetContext().Tovar.Add(_currentTovar);
                try
                {
                    ComissiyaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена!");
                    ClassManager.HomeFrame.Navigate(new Tovars(1));
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

        }

        private void ReturnButt_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
