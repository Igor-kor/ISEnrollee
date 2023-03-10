using Microsoft.Build.Framework.XamlTypes;
using Npgsql;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ИС_Абитуриент
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public RolesViewModel ViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new RolesViewModel();
            this.DataContext = ViewModel;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            UserAuth con = UserAuth.getUserAuth();
            //var sql = "SELECT pg_has_role( current_user,'Admin', 'member')";
            using (var cmd = new NpgsqlCommand(textBox.Text, con.con))
            {
                var result = cmd.ExecuteScalar();
                MessageBox.Show(result.ToString());
                //dataGrid.ItemsSource = (System.Collections.IEnumerable)result;
            }
        }

    }
}
