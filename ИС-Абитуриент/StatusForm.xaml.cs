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
    /// Логика взаимодействия для StatusForm.xaml
    /// </summary>
    public partial class StatusForm : Window
    {
        isenrolleeDataSet dataTable;
        statusTableAdapter statusDataAdapter;

        public RolesViewModel ViewModel { get; set; }
        public StatusForm()
        {
            InitializeComponent();
            ViewModel = new RolesViewModel();
            this.DataContext = ViewModel;
            UserAuth con = UserAuth.getUserAuth();
            statusDataAdapter = new statusTableAdapter();
            statusDataAdapter.Connection = con.con;
            dataTable = new isenrolleeDataSet();
            statusDataAdapter.Fill(dataTable.status);
            dataGrid.ItemsSource = dataTable.status.DefaultView;
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
            statusDataAdapter.Adapter.UpdateCommand = new NpgsqlCommandBuilder(statusDataAdapter.Adapter).GetUpdateCommand();
            statusDataAdapter.Adapter.Update(dataTable.status);
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

            statusDataAdapter.Adapter.InsertCommand = new NpgsqlCommandBuilder(statusDataAdapter.Adapter).GetInsertCommand();

            statusDataAdapter.InsertQuery1();
            // обноляем записи в таблице datagrid
            statusDataAdapter.Fill(dataTable.status);
            // выбираем последнюю запись, это будет та что создали
            dataGrid.SelectedIndex = dataTable.status.Count - 1;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            /* Удаление */
            int index = dataGrid.SelectedIndex;
            var selectedItem = dataGrid.SelectedItem;
            if (selectedItem != null)
            {
                dataTable.status.DefaultView.Delete(index);
            }
            statusDataAdapter.Adapter.DeleteCommand = new NpgsqlCommandBuilder(statusDataAdapter.Adapter).GetDeleteCommand();
            statusDataAdapter.Adapter.Update(dataTable.status);
        }
    }
}
