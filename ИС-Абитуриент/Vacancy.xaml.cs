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
    /// Логика взаимодействия для Vacancy.xaml
    /// </summary>
    public partial class Vacancy : Window
    {
        isenrolleeDataSet dataTable;
        isenrolleeDataSet datasetCombobox;
        vacancyTableAdapter vacancyDataAdapter;
        specialityTableAdapter specialityComboboxData;
        paymontTableAdapter paymontComboboxData;

        int person_id = 0;

        public RolesViewModel ViewModel { get; set; }
        public Vacancy()
        {
            InitializeComponent();
            ViewModel = new RolesViewModel();
            this.DataContext = ViewModel;
            UserAuth con = UserAuth.getUserAuth();
            vacancyDataAdapter = new vacancyTableAdapter();
            vacancyDataAdapter.Connection = con.con;
            dataTable = new isenrolleeDataSet();

            if (ViewModel.EnrolleeVisible)
            {
                person_id = UserAuth.getUserAuth().person_id;
                vacancyDataAdapter.FillBy1(dataTable.vacancy, person_id);
            }
            else
            {
                vacancyDataAdapter.Fill(dataTable.vacancy);
            }

            dataGrid.ItemsSource = dataTable.vacancy.DefaultView;
            datasetCombobox = new isenrolleeDataSet();

            specialityComboboxData = new specialityTableAdapter();
            specialityComboboxData.Connection = con.con;
            specialityComboboxData.Fill(datasetCombobox.speciality);
            comboBox.ItemsSource = datasetCombobox.speciality.DefaultView;

            paymontComboboxData = new paymontTableAdapter();
            paymontComboboxData.Connection = con.con;
            paymontComboboxData.Fill(datasetCombobox.paymont);
            comboBox1.ItemsSource = datasetCombobox.paymont.DefaultView;


        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            /* Сохранение */
            DataRowView selectedItem = dataGrid.SelectedItem as DataRowView;
            if (selectedItem != null)
            {
                selectedItem[0] = textBox.Text;
                selectedItem[1] = comboBox.SelectedValue;
                selectedItem[2] = comboBox1.SelectedValue;
                selectedItem[3] = textBox1.Text;
            }
            vacancyDataAdapter.Adapter.UpdateCommand = new NpgsqlCommandBuilder(vacancyDataAdapter.Adapter).GetUpdateCommand();
            vacancyDataAdapter.Adapter.Update(dataTable.vacancy);
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /* Заполнение полей по клику */
            DataRowView selectedItem = dataGrid.SelectedItem as DataRowView;
            if (selectedItem != null)
            {
                textBox.Text = selectedItem[0].ToString();
                comboBox.SelectedValue = selectedItem[1];
                comboBox1.SelectedValue = selectedItem[2];
                textBox1.Text = selectedItem[3].ToString();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            /* Создание */

            vacancyDataAdapter.Adapter.InsertCommand = new NpgsqlCommandBuilder(vacancyDataAdapter.Adapter).GetInsertCommand();

            vacancyDataAdapter.InsertQuery1();
            // обноляем записи в таблице datagrid
            vacancyDataAdapter.Fill(dataTable.vacancy);
            // выбираем последнюю запись, это будет та что создали
            dataGrid.SelectedIndex = dataTable.vacancy.Count - 1;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            /* Удаление */
            int index = dataGrid.SelectedIndex;
            var selectedItem = dataGrid.SelectedItem;
            if (selectedItem != null)
            {
                dataTable.vacancy.DefaultView.Delete(index);
            }
            vacancyDataAdapter.Adapter.DeleteCommand = new NpgsqlCommandBuilder(vacancyDataAdapter.Adapter).GetDeleteCommand();
            vacancyDataAdapter.Adapter.Update(dataTable.vacancy);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            int index = dataGrid.SelectedIndex;
            DataRowView selectedItem = dataGrid.SelectedItem as DataRowView;
            if (selectedItem != null)
            {
                ExamResult examresult = new ExamResult(Convert.ToInt32(selectedItem[0]), 1);
                examresult.Show();
            }
        }
    }
}
