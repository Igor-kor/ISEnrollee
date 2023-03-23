using Npgsql;
using System;
using System.Collections.Concurrent;
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

namespace ИС_Абитуриент
{
    /// <summary>
    /// Логика взаимодействия для SQLAdmin.xaml
    /// </summary>
    public partial class SQLAdmin : Window
    {
        NpgsqlDataAdapter npgsqlDataAdapter;
        DataSet dataSet;
        public RolesViewModel ViewModel { get; set; }
        public SQLAdmin()
        {
            InitializeComponent();
            ViewModel = new RolesViewModel();
            this.DataContext = ViewModel;
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            UserAuth con = UserAuth.getUserAuth();
            try
            {
                var cmd = new NpgsqlDataAdapter(new TextRange(textBox.Document.ContentStart,
                 textBox.Document.ContentEnd).Text, con.con);
                npgsqlDataAdapter = cmd;
                dataSet = new DataSet();
                cmd.Fill(dataSet);
                dataGrid.ItemsSource = dataSet.Tables[0].DefaultView;
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            npgsqlDataAdapter.DeleteCommand = new NpgsqlCommandBuilder(npgsqlDataAdapter).GetDeleteCommand();
            npgsqlDataAdapter.Update(dataSet);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            npgsqlDataAdapter.UpdateCommand = new NpgsqlCommandBuilder(npgsqlDataAdapter).GetUpdateCommand();
            npgsqlDataAdapter.Update(dataSet);
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
