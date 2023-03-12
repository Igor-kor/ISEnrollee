using Microsoft.Graph;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace ИС_Абитуриент
{
    /// <summary>
    /// Логика взаимодействия для Person.xaml
    /// </summary>
    public partial class Person : Window
    {
        NpgsqlDataAdapter npgsqlDataAdapter;
        isenrolleeDataSet dataTable;
        public RolesViewModel ViewModel { get; set; }
        public Person()
        {
            InitializeComponent();
            ViewModel = new RolesViewModel();
            this.DataContext = ViewModel;
            UserAuth con = UserAuth.getUserAuth();
            try
            {
                var cmd = new NpgsqlDataAdapter("SELECT * FROM person", con.con);
                npgsqlDataAdapter = cmd;
                dataTable = new isenrolleeDataSet();
                cmd.Fill(dataTable.person);
                // dataTable = table;
                dataGrid.ItemsSource = dataTable.person.DefaultView;
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            int index = dataGrid.SelectedIndex;
            //dataGrid.Items.RemoveAt(index);
            var selectedItem = dataGrid.SelectedItem;
            if (selectedItem != null)
            {
                dataTable.person.DefaultView.Delete(index);
                //dataGrid.Items.Remove(selectedItem);
            }
            npgsqlDataAdapter.UpdateCommand = new NpgsqlCommandBuilder(npgsqlDataAdapter).GetUpdateCommand();
            npgsqlDataAdapter.InsertCommand = new NpgsqlCommandBuilder(npgsqlDataAdapter).GetInsertCommand();
            npgsqlDataAdapter.DeleteCommand = new NpgsqlCommandBuilder(npgsqlDataAdapter).GetDeleteCommand();
            npgsqlDataAdapter.Update(dataTable.person);
        }
    }
}
