using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
using ИС_Абитуриент.isenrolleeDataSetTableAdapters;

namespace ИС_Абитуриент
{
    /// <summary>
    /// Логика взаимодействия для Enrollee.xaml
    /// </summary>
    public partial class Enrollee : Window
    {
        enrolleeTableAdapter npgsqlDataAdapter;
        isenrolleeDataSet dataTable;
        public RolesViewModel ViewModel { get; set; }
        public Enrollee(int person_id = 0)
        {
            InitializeComponent();
            ViewModel = new RolesViewModel();
            this.DataContext = ViewModel;
            UserAuth con = UserAuth.getUserAuth();
            try
            {
                var cmd = new enrolleeTableAdapter();
                cmd.Connection = con.con;
                //var cmdperson = new NpgsqlDataAdapter("SELECT * FROM person", con.con);
                npgsqlDataAdapter = cmd;
                dataTable = new isenrolleeDataSet();
               // npgsqlDataAdapter.UpdateCommand = new NpgsqlCommandBuilder(npgsqlDataAdapter).GetUpdateCommand();
               // npgsqlDataAdapter.InsertCommand = new NpgsqlCommandBuilder(npgsqlDataAdapter).GetInsertCommand();
               // npgsqlDataAdapter.DeleteCommand = new NpgsqlCommandBuilder(npgsqlDataAdapter).GetDeleteCommand();
                //cmdperson.Fill(dataTable.person);
                cmd.Fill(dataTable.enrollee);
               
                //cmd.Fill(dataTable.enrollee);
                dataGrid.ItemsSource = dataTable.enrollee.DefaultView;
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //npgsqlDataAdapter.UpdateQuery();
            //dataTable.enrollee.AcceptChanges();
            //dataTable.AcceptChanges();
            npgsqlDataAdapter.Adapter.UpdateCommand = new NpgsqlCommandBuilder(npgsqlDataAdapter.Adapter).GetUpdateCommand();
            npgsqlDataAdapter.Adapter.Update(dataTable.enrollee);
            
        }
    }
}
