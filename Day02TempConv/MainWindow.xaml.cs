using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Day02TempConv
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AnyRadioButton_Click(object sender, RoutedEventArgs e)
        {
            recalculate();
        }
        private void TbxInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            recalculate();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            recalculate();
        }
        private void TbxInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
           

            // anything that is not a numer, dot, or - is not allowed
            Regex regex = new Regex(@"[^0-9\.-]");
            // e.Handled set to true makes WPF ignore the new input from user
            e.Handled = regex.IsMatch(e.Text);

            // . can only appear once, if - appear, it should be the first one
            if (!Regex.IsMatch(TbxInput.Text, @"^-?(\d+(\.\d*)?|\.\d+)$"))
            {
                TbxOutput.Text = "Error";
                return;
            }
        }


        private void recalculate()
        {
            // do nothing if the whole XAML hasn't loaded yet
            if (!this.IsLoaded) return;
            // 1. parse the input
            if (!double.TryParse(TbxInput.Text, out double inVal))
            {
                TbxOutput.Text = "Error";
                return;
            }
            // 2. convert to celsius
            double cel;
            if (RbnInCel.IsChecked == true)
            {
                cel = inVal;
            }
            else if (RbnInFah.IsChecked == true)
            {
                cel = (inVal - 32) * 5 / 9;
            }
            else if (RbnInKel.IsChecked == true)
            {
                cel = inVal - 273.15;
            }
            else
            { // should never happen
                MessageBox.Show(this, "Invalid control flow", "Internal error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            // 3. convert to the output unit
            double outVal;
            string unit;
            if (RbnOutCel.IsChecked == true)
            {
                outVal = cel;
                unit = "C";
            }
            else if (RbnOutFah.IsChecked == true)
            {
                outVal = cel * 9 / 5 + 32;
                unit = "F";
            }
            else if (RbnOutKel.IsChecked == true)
            {
                outVal = cel + 273.15;
                unit = "K";
            }
            else
            { // should never happen
                MessageBox.Show(this, "Invalid control flow", "Internal error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // 4. display the result
            TbxOutput.Text = $"{outVal:0.0} \u00B0{unit}";
        }

    }
}
