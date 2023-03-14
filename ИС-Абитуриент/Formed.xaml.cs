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
    /// Логика взаимодействия для Formed.xaml
    /// </summary>
    public partial class Formed : Window
    {
        isenrolleeDataSet dataTable;
        formedTableAdapter formedDataAdapter;

        public RolesViewModel ViewModel { get; set; }
        public Formed()
        {
            InitializeComponent();
            ViewModel = new RolesViewModel();
            this.DataContext = ViewModel;
            UserAuth con = UserAuth.getUserAuth();
            formedDataAdapter = new formedTableAdapter();
            formedDataAdapter.Connection = con.con;
            dataTable = new isenrolleeDataSet();
            formedDataAdapter.Fill(dataTable.formed);
            dataGrid.ItemsSource = dataTable.formed.DefaultView;
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
            formedDataAdapter.Adapter.UpdateCommand = new NpgsqlCommandBuilder(formedDataAdapter.Adapter).GetUpdateCommand();
            formedDataAdapter.Adapter.Update(dataTable.formed);
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

            formedDataAdapter.Adapter.InsertCommand = new NpgsqlCommandBuilder(formedDataAdapter.Adapter).GetInsertCommand();

            formedDataAdapter.InsertQuery1();
            // обноляем записи в таблице datagrid
            formedDataAdapter.Fill(dataTable.formed);
            // выбираем последнюю запись, это будет та что создали
            dataGrid.SelectedIndex = dataTable.formed.Count - 1;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            /* Удаление */
            int index = dataGrid.SelectedIndex;
            var selectedItem = dataGrid.SelectedItem;
            if (selectedItem != null)
            {
                dataTable.formed.DefaultView.Delete(index);
            }
            formedDataAdapter.Adapter.DeleteCommand = new NpgsqlCommandBuilder(formedDataAdapter.Adapter).GetDeleteCommand();
            formedDataAdapter.Adapter.Update(dataTable.formed);
        }
    }
}
