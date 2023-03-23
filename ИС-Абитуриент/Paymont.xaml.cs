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
    /// Логика взаимодействия для Paymont.xaml
    /// </summary>
    /// 
    public partial class Paymont : Window
    {
        isenrolleeDataSet dataTable;
        paymontTableAdapter paymontDataAdapter;

        public RolesViewModel ViewModel { get; set; }
        public Paymont()
        {
            InitializeComponent();
            ViewModel = new RolesViewModel();
            this.DataContext = ViewModel;
            UserAuth con = UserAuth.getUserAuth();
            paymontDataAdapter = new paymontTableAdapter();
            paymontDataAdapter.Connection = con.con;
            dataTable = new isenrolleeDataSet();
            paymontDataAdapter.Fill(dataTable.paymont);
            dataGrid.ItemsSource = dataTable.paymont.DefaultView;
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
            paymontDataAdapter.Adapter.UpdateCommand = new NpgsqlCommandBuilder(paymontDataAdapter.Adapter).GetUpdateCommand();
            paymontDataAdapter.Adapter.Update(dataTable.paymont);
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

            paymontDataAdapter.Adapter.InsertCommand = new NpgsqlCommandBuilder(paymontDataAdapter.Adapter).GetInsertCommand();

            paymontDataAdapter.InsertQuery1();
            // обноляем записи в таблице datagrid
            paymontDataAdapter.Fill(dataTable.paymont);
            // выбираем последнюю запись, это будет та что создали
            dataGrid.SelectedIndex = dataTable.paymont.Count - 1;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            /* Удаление */
            int index = dataGrid.SelectedIndex;
            var selectedItem = dataGrid.SelectedItem;
            if (selectedItem != null)
            {
                dataTable.paymont.DefaultView.Delete(index);
            }
            paymontDataAdapter.Adapter.DeleteCommand = new NpgsqlCommandBuilder(paymontDataAdapter.Adapter).GetDeleteCommand();
            paymontDataAdapter.Adapter.Update(dataTable.paymont);
        }
    }
}
