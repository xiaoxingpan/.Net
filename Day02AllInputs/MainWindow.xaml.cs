using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace Day02AllInputs
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

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string name = TbxName.Text;
            if (name == "")
            {
                MessageBox.Show("Name must not be empty", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            string age = radioButton.Content.ToString();
            if(age == "")
            {
                MessageBox.Show("Age must not be empty", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string continent = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();
            if (continent == "")
            {
                MessageBox.Show("Age must not be empty", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string pet = checkBox.Content.ToString();
            if (pet == "")
            {
                MessageBox.Show("Pet must not be empty", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //if (checkBox == chkCat || checkBox == chkDog)
            //{
            //    // If "Cat" or "Dog" is clicked, uncheck "Other."
            //    chkOther.IsChecked = false;
            //}
            //else if (checkBox == chkOther)
            //{
            //    // If "Other" is clicked, uncheck "Cat" and "Dog."
            //    chkCat.IsChecked = false;
            //    chkDog.IsChecked = false;
            //}
        }

        private void tempSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int selectedAge = (int)e.NewValue;
            string temp = selectedAge.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
