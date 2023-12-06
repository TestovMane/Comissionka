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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Comissionka.PageWindows
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        private IEnumerable<Order> _orderList;
        public Orders()
        {
            InitializeComponent();
            DataContext = this;
            OrderList = ComissiyaEntities.GetContext().Order.ToList();
        }

        public IEnumerable<Order> OrderList
        {
            get
            {
                var Result = _orderList;
                return Result;
            }
            set
            {
                _orderList = value;
            }
        }
    }
}
