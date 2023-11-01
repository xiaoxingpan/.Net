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

namespace Day03FirstListView
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
        private void BtnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            string name = TbxName.Text;
            string pattern = "^[a-zA-Z]+$"; // This pattern allows only letters (both uppercase and lowercase)
            if (name == "" || !Regex.IsMatch(name, pattern))
            {
                MessageBox.Show(this, "Name must not be empty and contains only letters", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(!int.TryParse(TbxAge.Text, out int age) || age <= 0)
            {
                MessageBox.Show(this, "Age must not be empty and in correct format", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            LsvPeopleStrings.Items.Add($"{name} is {age} y/o");
            // clear the inputs
            TbxName.Text = "";
            TbxAge.Text = "";

            Person person = new Person(name, age);
            // List of Objects
            LsvPeopleObjects.Items.Add(person);

            // ListView with GridView
            LsvPeopleGrid.Items.Add(person);
        }
    }
}
