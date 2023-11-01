using System;
using System.Collections.Generic;
using System.Globalization;
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
using static MidtermPizzaOrders.PizzaOrder;

namespace MidtermPizzaOrders
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            { 
                LvOrders.ItemsSource = Globals.dbContextAuto.PizzaOrders.ToList(); 
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                // Close();
                Environment.Exit(1);
            }
        }

        private void MenuItem_NewOrderClick(object sender, RoutedEventArgs e)
        {
            // show NewOrderDialog Window 
            NewOrderDialog dialog = new NewOrderDialog();
            dialog.Owner = this;
            if (dialog.ShowDialog() == true)
            {
                LvOrders.ItemsSource = Globals.dbContextAuto.PizzaOrders.ToList();
                LblStatusMessage.Text = "Order placed";
            }
        }

        private void ChangeStatusToPlaced_Click(object sender, RoutedEventArgs e)
        {
            ChangeOrderStatus(OrderStatusEnum.Placed);
            LblStatusMessage.Text = "Order status updated to placed";
            LvOrders.ItemsSource = Globals.dbContextAuto.PizzaOrders.ToList();

        }

        private void ChangeStatusToFulfilled_Click(object sender, RoutedEventArgs e)
        {
            ChangeOrderStatus(OrderStatusEnum.Fulfilled);
            LblStatusMessage.Text = "Order status updated to fulfilled";
            LvOrders.ItemsSource = Globals.dbContextAuto.PizzaOrders.ToList();
        }

        private void ChangeOrderStatus(OrderStatusEnum newStatus)
        {
            foreach (var selectedItem in LvOrders.SelectedItems)
            {
                if (selectedItem is PizzaOrder order)
                {
                    order.OrderStatus = newStatus;
                }
            }
            LblStatusMessage.Text = "Order status updated";
        }

        private void MenuItemMarkSelectedPlaced_Click(object sender, RoutedEventArgs e)
        {
            ChangeOrderStatus(OrderStatusEnum.Placed);
            LblStatusMessage.Text = "Order status updated to placed";
            LvOrders.ItemsSource = Globals.dbContextAuto.PizzaOrders.ToList();
        }

        private void MenuItemMarkSelectedFulfilled_Click(object sender, RoutedEventArgs e)
        {
            ChangeOrderStatus(OrderStatusEnum.Fulfilled);
            LblStatusMessage.Text = "Order status updated to fulfilled";
            LvOrders.ItemsSource = Globals.dbContextAuto.PizzaOrders.ToList();
        }

    }
}
