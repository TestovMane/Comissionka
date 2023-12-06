using Comissionka.Class;
using Comissionka.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Comissionka.PageWindows
{
    /// <summary>
    /// Логика взаимодействия для Tovars.xaml
    /// </summary>
    public partial class Tovars : Page, INotifyPropertyChanged
    {
        private int SortType = 0;
        public string[] SortList { get; set; } =
        {
            "Без сортировки",
            "Сначала дороже",
            "Сначала дешевле",
            "Скидка больше",
            "Скидка меньше",
            "Год изготовления больше",
            "Год изготовления меньше",
        };

        public event PropertyChangedEventHandler PropertyChanged;
        private void Invalidate()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TovarList"));
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("TovarList"));
            }
            
        }

        private IEnumerable<Tovar> _tovarList;
        public IEnumerable<Tovar> TovarList
        {
            get
            {
                var Result = _tovarList;

                switch (SortType)
                {
                    case 0:
                        Result = ComissiyaEntities.GetContext().Tovar.ToList();
                        break;
                    case 1:
                        Result = ComissiyaEntities.GetContext().Tovar.OrderByDescending(p => p.TovarCost);
                        break;
                    case 2:
                        Result = ComissiyaEntities.GetContext().Tovar.OrderBy(p => p.TovarCost);
                        break;
                    case 3:
                        Result = ComissiyaEntities.GetContext().Tovar.OrderByDescending(p => p.TovarDiscountAmount);
                        break;
                    case 4:
                        Result = ComissiyaEntities.GetContext().Tovar.OrderBy(p => p.TovarDiscountAmount);
                        break;
                    case 5:
                        Result = ComissiyaEntities.GetContext().Tovar.OrderByDescending(p => p.TovarDateCreation);
                        break;
                    case 6:
                        Result = ComissiyaEntities.GetContext().Tovar.OrderBy(p => p.TovarDateCreation);
                        break;

                }

                if (SearchBoxs != "")
                    Result = Result.Where(p => p.TovarName.IndexOf(SearchBoxs, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    p.TovarDescription.IndexOf(SearchBoxs, StringComparison.OrdinalIgnoreCase) >= 0);

                if (CategoryFilterId > 0)
                    Result = Result.Where(i => i.TovarCategoriya == CategoryFilterId);

                return Result.Take(100);
            }
            set
            {
                _tovarList = value;
                Invalidate();
            }
        }

        public List<TovarCategory> Categories { get; set; }

        public Tovars(int UserRoles)
        {
            InitializeComponent();
            DataContext = this;
            TovarList = ComissiyaEntities.GetContext().Tovar.ToList();
            Categories = ComissiyaEntities.GetContext().TovarCategory.ToList();
            Categories.Insert(0, new TovarCategory { Name = "Все категории" });
            if (UserRoles == 2)
            {
                FirstGround.Height = new GridLength(0);

            }
            if (UserRoles == 3)
            {
                TovaringAdd.Visibility = Visibility.Hidden;

            }
        }

        private void ReturnButt_Click(object sender, RoutedEventArgs e)
        {
            MainWindow a = new MainWindow();
            a.Show();
        }

        public void ChangeButt_Click(object sender, RoutedEventArgs e)
        {
            AddTovar addTovar = new AddTovar((sender as Button).DataContext as Tovar);
            addTovar.ShowDialog();
        }

        private void TovaringAdd_Click(object sender, RoutedEventArgs e)
        {
            AddTovar addTovar = new AddTovar(null);
            addTovar.ShowDialog();
        }

        public string SearchBoxs = "";
        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            SearchBoxs = SearchBox.Text;
            Invalidate();
        }

        private void SortBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortType = SortBox.SelectedIndex;
            Invalidate();
        }

        private int CategoryFilterId = 0;
        private void CategoryFilterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoryFilterId = CategoryFilterBox.SelectedIndex;
            Invalidate();
        }

        private void DeleteButt_Click(object sender, RoutedEventArgs e)
        {
            var DeletingTovar = TovarListView.SelectedItems.Cast<Tovar>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {DeletingTovar.Count()} товар(ов)?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ComissiyaEntities.GetContext().Tovar.RemoveRange(DeletingTovar);
                    ComissiyaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Товары удалены!");
                    ClassManager.HomeFrame.Navigate(new Tovars(1));
                }
                catch
                {
                    MessageBox.Show("Удаление товара невозможно, так как он содержится в заказе!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
