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
        bonusTableAdapter bonusDataAdapter;

        public RolesViewModel ViewModel { get; set; }
        public Vacancy()
        {
            InitializeComponent();
            ViewModel = new RolesViewModel();
            this.DataContext = ViewModel;
            UserAuth con = UserAuth.getUserAuth();
            bonusDataAdapter = new bonusTableAdapter();
            bonusDataAdapter.Connection = con.con;
            dataTable = new isenrolleeDataSet();
            bonusDataAdapter.Fill(dataTable.bonus);
            dataGrid.ItemsSource = dataTable.bonus.DefaultView;
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
            bonusDataAdapter.Adapter.UpdateCommand = new NpgsqlCommandBuilder(bonusDataAdapter.Adapter).GetUpdateCommand();
            bonusDataAdapter.Adapter.Update(dataTable.bonus);
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

            bonusDataAdapter.Adapter.InsertCommand = new NpgsqlCommandBuilder(bonusDataAdapter.Adapter).GetInsertCommand();

            bonusDataAdapter.InsertQuery1();
            // обноляем записи в таблице datagrid
            bonusDataAdapter.Fill(dataTable.bonus);
            // выбираем последнюю запись, это будет та что создали
            dataGrid.SelectedIndex = dataTable.bonus.Count - 1;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            /* Удаление */
            int index = dataGrid.SelectedIndex;
            var selectedItem = dataGrid.SelectedItem;
            if (selectedItem != null)
            {
                dataTable.bonus.DefaultView.Delete(index);
            }
            bonusDataAdapter.Adapter.DeleteCommand = new NpgsqlCommandBuilder(bonusDataAdapter.Adapter).GetDeleteCommand();
            bonusDataAdapter.Adapter.Update(dataTable.bonus);
        }
    }
}
