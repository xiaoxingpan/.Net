using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Linq;

namespace Day03ListGridViewPeopleCRUD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string DataFileName = @"..\..\people.txt";
        List<Person> peopleList = new List<Person>();

        public MainWindow()
        {
            InitializeComponent();
            LoadDataFromFile(); // or call in window_loaded
            LvPeople.ItemsSource = peopleList;
        }

        private bool ArePersonInputsValid()
        {
            if (!Person.IsNameValid(TbxName.Text, out string errorName))
            {
                MessageBox.Show(this, errorName, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!Person.IsAgeValid(TbxAge.Text, out string errorAge))
            {
                MessageBox.Show(this, errorAge, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        // show all the errors in a list, show a sigle message box
        private bool ArePersonInputsValid2()
        {
            List<string> errorList = new List<string>();

            if (!Person.IsNameValid(TbxName.Text, out string errorName))
            {
                errorList.Add(errorName);
            }
            if (!Person.IsAgeValid(TbxAge.Text, out string errorAge))
            {
                errorList.Add(errorAge);
            }
            if (errorList.Count > 0)
            {
                MessageBox.Show(this, String.Join("\n", errorList), "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        // show error underline of the textbox
        private bool ArePersonInputsValid3()
        {
            // FIXME: app logic unnecessarily dependant on UI content
            LblErrName.Visibility = Person.IsNameValid(TbxName.Text, out string errorName) ? Visibility.Hidden : Visibility.Visible;
            LblErrAge.Visibility = Person.IsAgeValid(TbxAge.Text, out string errorAge) ? Visibility.Hidden : Visibility.Visible;
            return (LblErrName.Visibility == Visibility.Hidden && LblErrAge.Visibility == Visibility.Hidden);
        }

        private void BtnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            if (!ArePersonInputsValid3()) return;
            string name = TbxName.Text;
            int.TryParse(TbxAge.Text, out int age); // okay: no need to validate again
            peopleList.Add(new Person(name, age));
            LvPeople.Items.Refresh(); // tell ListView data has changed
            ResetFields();
        }

        

        private void BtnDeletePerson_Click(object sender, RoutedEventArgs e)
        {
            Person selectedPerson = LvPeople.SelectedItem as Person;
            if (selectedPerson == null) return;
            // confirmation 
            var result = MessageBox.Show(this, "Are you sure you want to delete this item?\n" + selectedPerson, "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            peopleList.Remove(selectedPerson);
                MessageBox.Show("Record deleted", "Deletion Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                LvPeople.Items.Refresh();
                ResetFields();
            // make sure nothing is selected anymore
                LvPeople.UnselectAll();
            //  LvPeople.SelectedIndex = -1; 
        }
        private void BtnUpdatePerson_Click(object sender, RoutedEventArgs e)
        {
            Person selectedPerson = LvPeople.SelectedItem as Person;
            if (selectedPerson == null) return;
            if (!ArePersonInputsValid3()) return;
            selectedPerson.Name = TbxName.Text;
            int.TryParse(TbxAge.Text, out int age);
                selectedPerson.Age = age;
                MessageBox.Show("Record updated", "Update Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                LvPeople.Items.Refresh();
                ResetFields();
                LvPeople.UnselectAll();
           
        }

        private void LvPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Person selectedPerson = LvPeople.SelectedItem as Person;

            if (selectedPerson != null)
            {
                // Either this or Binding in XAML to enable/disable buttons
                // BtnUpdatePerson.IsEnabled = (selectedPerson != null);
                // BtnDeletePerson.IsEnabled = (selectedPerson != null);
                TbxName.Text = selectedPerson.Name;
                TbxAge.Text = selectedPerson.Age.ToString();
            }
            else
            {
                ResetFields();
            }
        }

        private void LoadDataFromFile() // call when window is loaded
        {
            // do your best with validation
            // data stored semicolon-separated, one per line:  Jerry;33
            try
            {
                if (!File.Exists(DataFileName)) return; // ignore if no file
                List<string> errorsList = new List<string>();
                string[] linesArray = File.ReadAllLines(DataFileName); // ex IO/System
                for (int i = 0; i < linesArray.Length; i++)
                {
                    string line = linesArray[i];
                    var data = line.Split(';');
                    if (data.Length != 2)
                    {
                        errorsList.Add($"Each line must have exactly 2 fields (line {i + 1})" +
                            "\n" + line);
                        continue;
                    }
                    // TODO: setters would allow us to avoid this code duplication
                    string name = data[0];
                    string ageStr = data[1];
                    if (!Person.IsNameValid(name, out string errorName))
                    {
                        errorsList.Add($"{errorName} (line {i + 1})");
                        continue;
                    }
                    int.TryParse(ageStr, out int age);
                    if (!Person.IsAgeValid(age, out string errorAge))
                    {
                        errorsList.Add($"{errorAge} (line {i + 1})");
                        continue;
                    }
                    peopleList.Add(new Person(name, age));
                }
                LvPeople.Items.Refresh(); // tell ListView data has changed
                if (errorsList.Count != 0)
                {
                    string allErrors = String.Join("\n", errorsList);
                    MessageBox.Show(this, $"Warning: some lines were ignored due to {errorsList.Count} errors:\n" + allErrors, "Data errors", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from file\n" + ex.Message, "File access error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveDataToFile() // call when window is closing
        {
            List<string> lines = new List<string>();
            foreach (Person p in peopleList)
            {
                lines.Add($"{p.Name};{p.Age}");
            }
            try
            {
                File.WriteAllLines(DataFileName, lines); // ex
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error writing to file\n" + ex.Message, "File access error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ResetFields()
        {
            TbxName.Text = "";
            TbxAge.Text = "";
        }

        private void MiExport_Click(object sender, RoutedEventArgs e)
        { }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // LoadDataFromFile();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveDataToFile();
            // ask uer if save failed
        }
    }
}
