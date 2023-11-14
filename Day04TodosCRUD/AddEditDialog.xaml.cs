
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

namespace Day04TodosCRUD
{
    /// <summary>
    /// Interaction logic for AddEditDialog.xaml
    /// </summary>
    public partial class AddEditDialog : Window
    {
        Todo currTodo;
        public AddEditDialog(Todo currTodo = null)
        {
            this.currTodo = currTodo;
            InitializeComponent();
            if(currTodo != null ) 
            { // update, load select values
                TaskInput.Text = currTodo.Task;
                DifficultySlider.Value = currTodo.Difficulty;
                DueDate.SelectedDate = currTodo.DueDate;
                StatusComboBox.SelectedIndex = (int)currTodo.Status;
                BtnSave.Content = "Update";
            }
            else //add
            {
                DueDate.SelectedDate = DateTime.Today;
                BtnSave.Content = "Add";
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            { if (currTodo != null)
                { // update
                    currTodo.Task = TaskInput.Text;
                    currTodo.Difficulty = (int)DifficultySlider.Value;
                    currTodo.DueDate = (DateTime)DueDate.SelectedDate;
                    currTodo.Status = (Todo.StatusEnum)StatusComboBox.SelectedIndex;
                }
                else // add
                {
                    Todo newTodo(TaskInput.Text, (int) DifficultySlider.Value, (DateTime) DueDate.SelectedDate, (Todo.StatusEnum) StatusComboBox.SelectedIndex );
                    Globals.dbContextAuto.Todos.Add(newTodo);
                }
                Globals.dbContextAuto.SaveChanges();
                this.DialogResult = true; // dismiss the dialog
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, , "Error Reading from Database\n" + ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
