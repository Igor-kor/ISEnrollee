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
using ИС_Абитуриент.isenrolleeDataSetTableAdapters;

namespace ИС_Абитуриент
{
    /// <summary>
    /// Логика взаимодействия для Exam.xaml
    /// </summary>
    public partial class Exam : Window
    {
        isenrolleeDataSet dataTable;
        examTableAdapter examDataAdapter;

        isenrolleeDataSet datasetCombobox;
        disciplineTableAdapter disciplineComboboxData;
        personTableAdapter personComboboxData;

        public RolesViewModel ViewModel { get; set; }
        public Exam()
        {
            InitializeComponent();
            ViewModel = new RolesViewModel();
            this.DataContext = ViewModel;
            UserAuth con = UserAuth.getUserAuth();
            examDataAdapter = new examTableAdapter();
            examDataAdapter.Connection = con.con;
            dataTable = new isenrolleeDataSet();
            examDataAdapter.Fill(dataTable.exam);
            dataGrid.ItemsSource = dataTable.exam.DefaultView;

            datasetCombobox = new isenrolleeDataSet();

            disciplineComboboxData = new disciplineTableAdapter();
            disciplineComboboxData.Connection = con.con;
            disciplineComboboxData.Fill(datasetCombobox.discipline);
            comboBox.ItemsSource = datasetCombobox.discipline.DefaultView;

            personComboboxData = new personTableAdapter();
            personComboboxData.Connection = con.con;
            personComboboxData.Fill(datasetCombobox.person);
            comboBox1.ItemsSource = datasetCombobox.person.DefaultView;
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            /* Сохранение */
            DataRowView selectedItem = dataGrid.SelectedItem as DataRowView;
            if (selectedItem != null)
            {
                selectedItem[0] = comboBox.SelectedValue;
                selectedItem[1] = comboBox1.SelectedValue;
                selectedItem[2] = Convert.ToInt32(textBox1.Text);
                selectedItem[3] = datePicker.SelectedDate;
               // selectedItem[2] = textBox.Text;
            }
            examDataAdapter.Adapter.UpdateCommand = new NpgsqlCommandBuilder(examDataAdapter.Adapter).GetUpdateCommand();
            examDataAdapter.Adapter.Update(dataTable.exam);
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /* Заполнение полей по клику */
            DataRowView selectedItem = dataGrid.SelectedItem as DataRowView;
            if (selectedItem != null)
            {
                comboBox.SelectedValue = selectedItem[0];
                comboBox1.SelectedValue = selectedItem[1];
                textBox.Text = selectedItem[4].ToString();
                datePicker.Text = selectedItem[3].ToString();
                textBox1.Text = selectedItem[2].ToString();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            /* Создание */

            examDataAdapter.Adapter.InsertCommand = new NpgsqlCommandBuilder(examDataAdapter.Adapter).GetInsertCommand();

            examDataAdapter.InsertQuery1();
            // обноляем записи в таблице datagrid
            examDataAdapter.Fill(dataTable.exam);
            // выбираем последнюю запись, это будет та что создали
            dataGrid.SelectedIndex = dataTable.exam.Count - 1;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            /* Удаление */
            int index = dataGrid.SelectedIndex;
            var selectedItem = dataGrid.SelectedItem;
            if (selectedItem != null)
            {
                dataTable.exam.DefaultView.Delete(index);
            }
            examDataAdapter.Adapter.DeleteCommand = new NpgsqlCommandBuilder(examDataAdapter.Adapter).GetDeleteCommand();
            examDataAdapter.Adapter.Update(dataTable.exam);
        }
    }
}
