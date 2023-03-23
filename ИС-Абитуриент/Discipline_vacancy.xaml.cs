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
    /// Логика взаимодействия для Discipline_vacancy.xaml
    /// </summary>
    public partial class Discipline_vacancy : Window
    {
        isenrolleeDataSet dataTable;
        isenrolleeDataSet datasetCombobox;
        discipline_vacancyTableAdapter discipline_vacancyTableDataAdapter;
        disciplineTableAdapter disciplineComboboxData;
        vacancyTableAdapter vacancyComboboxData;


        public RolesViewModel ViewModel { get; set; }
        public Discipline_vacancy()
        {
            InitializeComponent();
            ViewModel = new RolesViewModel();
            this.DataContext = ViewModel;
            UserAuth con = UserAuth.getUserAuth();
            discipline_vacancyTableDataAdapter = new discipline_vacancyTableAdapter();
            discipline_vacancyTableDataAdapter.Connection = con.con;
            dataTable = new isenrolleeDataSet();
            discipline_vacancyTableDataAdapter.Fill(dataTable.discipline_vacancy);
            dataGrid.ItemsSource = dataTable.discipline_vacancy.DefaultView;
            datasetCombobox = new isenrolleeDataSet();

            disciplineComboboxData = new disciplineTableAdapter();
            disciplineComboboxData.Connection = con.con;
            disciplineComboboxData.Fill(datasetCombobox.discipline);
            comboBox.ItemsSource = datasetCombobox.discipline.DefaultView;

            vacancyComboboxData = new vacancyTableAdapter();
            vacancyComboboxData.Connection = con.con;
            vacancyComboboxData.Fill(datasetCombobox.vacancy);
            comboBox1.ItemsSource = datasetCombobox.vacancy.DefaultView;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            /* Сохранение */
            DataRowView selectedItem = dataGrid.SelectedItem as DataRowView;
            if (selectedItem != null)
            {
                selectedItem[2] = textBox.Text;
                selectedItem[0] = comboBox.SelectedValue;
                selectedItem[1] = comboBox1.SelectedValue;
            }
            discipline_vacancyTableDataAdapter.Adapter.UpdateCommand = new NpgsqlCommandBuilder(discipline_vacancyTableDataAdapter.Adapter).GetUpdateCommand();
            discipline_vacancyTableDataAdapter.Adapter.Update(dataTable.discipline_vacancy);
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /* Заполнение полей по клику */
            DataRowView selectedItem = dataGrid.SelectedItem as DataRowView;
            if (selectedItem != null)
            {
                textBox.Text = selectedItem[2].ToString();
                comboBox.SelectedValue = selectedItem[0];
                comboBox1.SelectedValue = selectedItem[1];
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            /* Создание */

            discipline_vacancyTableDataAdapter.Adapter.InsertCommand = new NpgsqlCommandBuilder(discipline_vacancyTableDataAdapter.Adapter).GetInsertCommand();

            discipline_vacancyTableDataAdapter.InsertQuery1();
            // обноляем записи в таблице datagrid
            discipline_vacancyTableDataAdapter.Fill(dataTable.discipline_vacancy);
            // выбираем последнюю запись, это будет та что создали
            dataGrid.SelectedIndex = dataTable.discipline_vacancy.Count - 1;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            /* Удаление */
            int index = dataGrid.SelectedIndex;
            var selectedItem = dataGrid.SelectedItem;
            if (selectedItem != null)
            {
                dataTable.discipline_vacancy.DefaultView.Delete(index);
            }
            discipline_vacancyTableDataAdapter.Adapter.DeleteCommand = new NpgsqlCommandBuilder(discipline_vacancyTableDataAdapter.Adapter).GetDeleteCommand();
            discipline_vacancyTableDataAdapter.Adapter.Update(dataTable.discipline_vacancy);
        }
    }
}
