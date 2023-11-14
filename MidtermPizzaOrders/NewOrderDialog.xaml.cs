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
using System.Xml.Linq;

namespace MidtermPizzaOrders
{
    /// <summary>
    /// Interaction logic for NewOrderDialog.xaml
    /// </summary>
    public partial class NewOrderDialog : Window
    {
        private PizzaOrder currOrder = new PizzaOrder();

        public NewOrderDialog()
        {
            InitializeComponent();
            // set this moment plus  1 hour to date picker and time textbox
            DateTime currentDateTime = DateTime.Now;
            DateTime deliveryDeadline = currentDateTime.AddHours(1);
            DlDate.SelectedDate = deliveryDeadline.Date;
            DlTime.Text = deliveryDeadline.ToString("HH:mm");
        }

        // validate the clientName and postCode when lost focus
        private void TbxName_LostFocus(object sender, RoutedEventArgs e)
        {
            LblErrClientName.Visibility = PizzaOrder.IsClientNameValid(TbxName.Text, out string errorName) ? Visibility.Hidden : Visibility.Visible;
        }

        private void TbxPostCode_LostFocus(object sender, RoutedEventArgs e)
        {
            LblErrClientPostCode.Visibility = PizzaOrder.IsClientPostalCodeValid(TbxPostCode.Text, out string errorAge) ? Visibility.Hidden : Visibility.Visible;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DlDate.SelectedDate == null || DlTime.Text == null)
                {
                    throw new ArgumentException("Delivery deadline error");
                }
                

                // get client name and post code
                currOrder.ClientName = TbxName.Text;
                currOrder.ClientPostalCode = TbxPostCode.Text;

                // get delivery deadline
                var selectedDate = DlDate.SelectedDate.Value;
                if ( DateTime.TryParse(DlTime.Text, out var selectedTime))
                {
                    // Assuming the format of DlTime.Text is "HH:mm"
                    var deliveryDate = DlDate.SelectedDate.Value;

                    var combinedDateTime = new DateTime(
                        deliveryDate.Year, deliveryDate.Month, deliveryDate.Day,
                        selectedTime.Hour, selectedTime.Minute, 0);

                    currOrder.DeliveryDeadline = combinedDateTime;
                }

                // get pizza size
                if (ComboSize.SelectedItem != null)
                {
                    if (!int.TryParse((ComboSize.SelectedItem as ComboBoxItem)?.Tag?.ToString(), out int selectedSizeValue))
                    {
                        throw new ArgumentException("PizzaSize error");
                    }
                    Console.WriteLine(selectedSizeValue);
                    currOrder.Size = (PizzaOrder.SizeEnum)selectedSizeValue;
                }
                else
                {
                    MessageBox.Show(this, "Please select a pizza size", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
               
                // get baking time
                if (BakingTimeSlider.Value >= BakingTimeSlider.Minimum && BakingTimeSlider.Value <= BakingTimeSlider.Maximum)
                {
                    
                    currOrder.BakingTimeMinutes = (int)BakingTimeSlider.Value;
                }
                Console.WriteLine(currOrder.BakingTimeMinutes);

                //get extra
                List<CheckBox> ExtraCheckBoxes = new List<CheckBox> { CbxExtraCheese, CbxDeepDish, CbxWholeWheat };
                string extras = ExtraCheckBoxes.Any(cb => cb.IsChecked == true)
    ? string.Join(";", ExtraCheckBoxes.Where(cb => cb.IsChecked == true).Select(cb => cb.Content))
    : null;
                currOrder.Extras = extras;
                Console.WriteLine(currOrder.Extras);
            }
            
            


            currOrder.OrderStatus = 0;
            G
            lobals.dbContextAuto.PizzaOrders.Add(currOrder);
            Globals.dbContextAuto.SaveChanges();
            Console.WriteLine(currOrder);
            this.DialogResult = true; // dismiss the dialog
        }

        private void BakingTimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (BakingTimeSlider.Value >= BakingTimeSlider.Minimum && BakingTimeSlider.Value <= BakingTimeSlider.Maximum)
            {

                currOrder.BakingTimeMinutes = (int)BakingTimeSlider.Value;
            }
        }
    }
}
