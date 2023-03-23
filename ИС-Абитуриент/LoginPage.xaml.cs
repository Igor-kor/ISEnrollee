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
using Npgsql;

namespace ИС_Абитуриент
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            UserAuth user = new UserAuth(LoginBox.Text, passwordBox.Password);
            if (user.ConnectToDB())
            {
                MainWindow mainwindow = new MainWindow();
                mainwindow.Show();
                Window.GetWindow(this).Close();
            }
            else
            {
                MessageBox.Show("Не удалось подключиться");
            }
            
        }
    }
}
