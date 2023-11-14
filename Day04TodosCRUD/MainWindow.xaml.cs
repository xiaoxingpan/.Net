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

namespace Day04TodosCRUD
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
                // Globals.dbContext = new TodoDbContext(); // Exceptions
                LvToDos.ItemsSource = Globals.dbContextAuto.Todos.ToList(); // equivalent of SELECT * FROM people
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                // Close();
                Environment.Exit(1);
            }
        }

        private void MenuItem_AddTodoClick(object sender, RoutedEventArgs e)
        {
            // show AddEditDialog Window by instantiating one and reuse it
            AddEditDialog dialog = new AddEditDialog();
            dialog.Owner = this;
            if (dialog.ShowDialog() == true)
            {
                LvToDos.ItemsSource = Globals.dbContextAuto.Todos.ToList();
                LblStatus.Text = "Todo added";
            }
        }

        private void lvTodos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // show selected todo in AddEditDialog Window
            Todo currSelectedTodo = LvToDos.SelectedItem as Todo; // if cast failed, return null, if (Todo)LvToDos.SelectedItem failed will throw ex
            if (currSelectedTodo == null) return; // nothing selected
            AddEditDialog dialog = new AddEditDialog(currSelectedTodo);
            dialog.Owner = this;
            if (dialog.ShowDialog() == true)
            {
                LvToDos.ItemsSource = Globals.dbContextAuto.Todos.ToList();
                LblStatus.Text = "Todo updated";
            }
        }

        private void MenuItem_FileExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            lvTodos_MouseDoubleClick(sender, null);
        }

        private void MenuItemExport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LvTodos_OnSelectionChanged(object sender, RoutedEventArgs e)
        {

        }

    }
}
