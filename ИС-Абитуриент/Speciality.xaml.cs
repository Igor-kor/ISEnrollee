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
    /// Логика взаимодействия для Speciality.xaml
    /// </summary>
    public partial class Speciality : Window
    {
        isenrolleeDataSet dataTable;
        specialityTableAdapter specialityDataAdapter;

        public RolesViewModel ViewModel { get; set; }
        public Speciality()
        {
            InitializeComponent();
            ViewModel = new RolesViewModel();
            this.DataContext = ViewModel;
            UserAuth con = UserAuth.getUserAuth();
            specialityDataAdapter = new specialityTableAdapter();
            specialityDataAdapter.Connection = con.con;
            dataTable = new isenrolleeDataSet();
            specialityDataAdapter.Fill(dataTable.speciality);
            dataGrid.ItemsSource = dataTable.speciality.DefaultView;
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
            specialityDataAdapter.Adapter.UpdateCommand = new NpgsqlCommandBuilder(specialityDataAdapter.Adapter).GetUpdateCommand();
            specialityDataAdapter.Adapter.Update(dataTable.speciality);
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

            specialityDataAdapter.Adapter.InsertCommand = new NpgsqlCommandBuilder(specialityDataAdapter.Adapter).GetInsertCommand();

            specialityDataAdapter.InsertQuery1();
            // обноляем записи в таблице datagrid
            specialityDataAdapter.Fill(dataTable.speciality);
            // выбираем последнюю запись, это будет та что создали
            dataGrid.SelectedIndex = dataTable.speciality.Count - 1;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            /* Удаление */
            int index = dataGrid.SelectedIndex;
            var selectedItem = dataGrid.SelectedItem;
            if (selectedItem != null)
            {
                dataTable.speciality.DefaultView.Delete(index);
            }
            specialityDataAdapter.Adapter.DeleteCommand = new NpgsqlCommandBuilder(specialityDataAdapter.Adapter).GetDeleteCommand();
            specialityDataAdapter.Adapter.Update(dataTable.speciality);
        }
    }
}
