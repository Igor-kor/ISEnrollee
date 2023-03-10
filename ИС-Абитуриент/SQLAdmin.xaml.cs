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
                var table = new DataTable();
                cmd.Fill(table);
                dataGrid.ItemsSource = table.DefaultView;
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
