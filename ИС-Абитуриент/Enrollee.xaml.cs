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
        personTableAdapter cmdperson;
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
                cmdperson = new personTableAdapter();
                cmdperson.Connection = con.con;
                npgsqlDataAdapter = cmd;
                dataTable = new isenrolleeDataSet();

                if (person_id > 0)
                {
                    cmdperson.FillByPersonid(dataTable.person, person_id);
                    cmd.FillByPersonId(dataTable.enrollee, person_id);
                }
                else
                {
                   // cmdperson.Fill(dataTable.person);
                    cmd.Fill(dataTable.enrollee);
                }

                // todo: Переделать на новый табладаптер!!!
                comboBox.ItemsSource = dataTable.person.DefaultView;
                dataGrid.ItemsSource = dataTable.enrollee.DefaultView;
                dataGrid1.ItemsSource = dataTable.person.DefaultView;
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

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selectedItem = dataGrid.SelectedItem as DataRowView;
            if (selectedItem != null)
            {
                if (selectedItem[2].ToString().Length > 0)
                {
                    cmdperson.FillByPersonid(dataTable.person, selectedItem[2]);
                    comboBox.SelectedValue = selectedItem[2];
                }

            }
        }
    }
}
