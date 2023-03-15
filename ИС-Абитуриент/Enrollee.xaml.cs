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
        enrolleeTableAdapter enrolleeDataAdapter;
        isenrolleeDataSet dataTable;
        isenrolleeDataSet datasetCombobox;
        personTableAdapter cmdperson;

        personTableAdapter personComboboxData;
        //bonusTableAdapter bonusComboboxData;
        vacancy1TableAdapter vacancy1ComboboxData;
        statusTableAdapter statusComboboxData;
        formedTableAdapter formedComboboxData;

        int person_id;

        public RolesViewModel ViewModel { get; set; }
        public Enrollee(int _person_id = 0)
        {
            person_id = _person_id;
            InitializeComponent();
            ViewModel = new RolesViewModel();
            this.DataContext = ViewModel;
            UserAuth con = UserAuth.getUserAuth();
            try
            {
                enrolleeDataAdapter = new enrolleeTableAdapter();
                enrolleeDataAdapter.Connection = con.con;
                cmdperson = new personTableAdapter();
                cmdperson.Connection = con.con;
                dataTable = new isenrolleeDataSet();
                if (person_id > 0)
                {
                    cmdperson.FillByPersonid(dataTable.person, person_id);
                    enrolleeDataAdapter.FillByPersonId(dataTable.enrollee, person_id);
                }
                else
                {
                    enrolleeDataAdapter.Fill(dataTable.enrollee);
                }
                datasetCombobox = new isenrolleeDataSet();
               // bonusComboboxData = new bonusTableAdapter();
                //bonusComboboxData.Connection = con.con;
                personComboboxData = new personTableAdapter();
                personComboboxData.Connection = con.con;
                vacancy1ComboboxData = new vacancy1TableAdapter();
                vacancy1ComboboxData.Connection = con.con;
                statusComboboxData = new statusTableAdapter();
                statusComboboxData.Connection = con.con;
                formedComboboxData = new formedTableAdapter();
                formedComboboxData.Connection = con.con;
                //bonusComboboxData.Fill(datasetCombobox.bonus);
                personComboboxData.Fill(datasetCombobox.person);
                vacancy1ComboboxData.Fill(datasetCombobox.vacancy1);
                statusComboboxData.Fill(datasetCombobox.status);
                formedComboboxData.Fill(datasetCombobox.formed);
                //comboBox.ItemsSource = datasetCombobox.bonus.DefaultView;
                comboBox1.ItemsSource = datasetCombobox.person.DefaultView;
                comboBox2.ItemsSource = datasetCombobox.vacancy1.DefaultView;
                comboBox3.ItemsSource = datasetCombobox.status.DefaultView;
                comboBox4.ItemsSource = datasetCombobox.formed.DefaultView;

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
            DataRowView selectedItem = dataGrid.SelectedItem as DataRowView;
            if (selectedItem != null)
            {
                cmdperson.FillByPersonid(dataTable.person, selectedItem[1]);
                //selectedItem[1] = comboBox.SelectedValue;
                selectedItem[1] = comboBox1.SelectedValue;
                selectedItem[2] = comboBox2.SelectedValue;
                selectedItem[3] = comboBox3.SelectedValue;
                selectedItem[4] = comboBox4.SelectedValue;
            }
            enrolleeDataAdapter.Adapter.UpdateCommand = new NpgsqlCommandBuilder(enrolleeDataAdapter.Adapter).GetUpdateCommand();
            enrolleeDataAdapter.Adapter.Update(dataTable.enrollee);

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selectedItem = dataGrid.SelectedItem as DataRowView;
            if (selectedItem != null)
            {
                cmdperson.FillByPersonid(dataTable.person, selectedItem[1]);
                //comboBox.SelectedValue = selectedItem[1];
                comboBox1.SelectedValue = selectedItem[1];
                comboBox2.SelectedValue = selectedItem[2];
                comboBox3.SelectedValue = selectedItem[3];
                comboBox4.SelectedValue = selectedItem[4];
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            int index = dataGrid.SelectedIndex;
            var selectedItem = dataGrid.SelectedItem;
            if (selectedItem != null)
            {
                dataTable.enrollee.DefaultView.Delete(index);
            }
            enrolleeDataAdapter.Adapter.DeleteCommand = new NpgsqlCommandBuilder(enrolleeDataAdapter.Adapter).GetDeleteCommand();
            enrolleeDataAdapter.Adapter.Update(dataTable.enrollee);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (person_id > 0)
            {
                // создаем запись в бд
                enrolleeDataAdapter.InsertQuery1( person_id);
            }
            else
            {
                // создаем запись в бд
                enrolleeDataAdapter.InsertQuery2();
            }
            enrolleeDataAdapter.Adapter.InsertCommand = new NpgsqlCommandBuilder(enrolleeDataAdapter.Adapter).GetInsertCommand();
           
            // обноляем записи в таблице datagrid
            enrolleeDataAdapter.Fill(dataTable.enrollee);
            // выбираем последнюю запись, это будет та что создали
            dataGrid.SelectedIndex = dataTable.enrollee.Count - 1;
        }
    }
}
