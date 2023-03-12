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

namespace ИС_Абитуриент
{
    /// <summary>
    /// Логика взаимодействия для CreateUser.xaml
    /// </summary>
    public partial class CreateUser : Window
    {
        public CreateUser()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            UserAuth con = UserAuth.getUserAuth();
            try
            {
                var roles = "";
                if ((checkBox.IsChecked ?? true) || (checkBox.IsChecked ?? true))
                {
                    roles = "IN GROUP";
                }
                    var role =  "";
                if (checkBox.IsChecked ?? true)
                {
                    role = "admin";
                }   

                if (checkBox1.IsChecked ?? true)
                {
                    role = "employee";
                }
                string query = String.Format("CREATE USER \"{0}\" WITH PASSWORD '{1}' {2} {3}",
                    textBox.Text, 
                    textBox1.Text,
                    roles,
                    role
                    );
                var cmd = new NpgsqlCommand(query, con.con);
                MessageBox.Show("Пользователь создан");
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
