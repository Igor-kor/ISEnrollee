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
    /// Логика взаимодействия для Discipline.xaml
    /// </summary>
    public partial class Discipline : Window
    {
        isenrolleeDataSet dataTable;
        disciplineTableAdapter disciplineDataAdapter;

        public RolesViewModel ViewModel { get; set; }
        public Discipline()
        {
            InitializeComponent();
            ViewModel = new RolesViewModel();
            this.DataContext = ViewModel;
            UserAuth con = UserAuth.getUserAuth();
            disciplineDataAdapter = new disciplineTableAdapter();
            disciplineDataAdapter.Connection = con.con;
            dataTable = new isenrolleeDataSet();
            disciplineDataAdapter.Fill(dataTable.discipline);
            dataGrid.ItemsSource = dataTable.discipline.DefaultView;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            /* Сохранение */
            DataRowView selectedItem = dataGrid.SelectedItem as DataRowView;
            if (selectedItem != null)
            {
                selectedItem[0] = textBox.Text;
                selectedItem[1] = textBox1.Text;
            }
            disciplineDataAdapter.Adapter.UpdateCommand = new NpgsqlCommandBuilder(disciplineDataAdapter.Adapter).GetUpdateCommand();
            disciplineDataAdapter.Adapter.Update(dataTable.discipline);
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /* Заполнение полей по клику */
            DataRowView selectedItem = dataGrid.SelectedItem as DataRowView;
            if (selectedItem != null)
            {
                textBox.Text = selectedItem[0].ToString();
                textBox1.Text = selectedItem[1].ToString();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            /* Создание */

            disciplineDataAdapter.Adapter.InsertCommand = new NpgsqlCommandBuilder(disciplineDataAdapter.Adapter).GetInsertCommand();

            disciplineDataAdapter.InsertQuery1();
            // обноляем записи в таблице datagrid
            disciplineDataAdapter.Fill(dataTable.discipline);
            // выбираем последнюю запись, это будет та что создали
            dataGrid.SelectedIndex = dataTable.discipline.Count - 1;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            /* Удаление */
            int index = dataGrid.SelectedIndex;
            var selectedItem = dataGrid.SelectedItem;
            if (selectedItem != null)
            {
                dataTable.discipline.DefaultView.Delete(index);
            }
            disciplineDataAdapter.Adapter.DeleteCommand = new NpgsqlCommandBuilder(disciplineDataAdapter.Adapter).GetDeleteCommand();
            disciplineDataAdapter.Adapter.Update(dataTable.discipline);
        }
    }
}
