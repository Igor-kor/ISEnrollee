using Microsoft.Build.Framework.XamlTypes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ИС_Абитуриент.isenrolleeDataSetTableAdapters;

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
            SQLAdmin sqlAdmin = new SQLAdmin();
            sqlAdmin.Show();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            CreateUser createUser = new CreateUser();
            createUser.Show();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Person person = new Person();
            person.Show();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Enrollee enrollee = new Enrollee();
            enrollee.Show();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            bonus _bonus = new bonus();
            _bonus.Show();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            Formed formed = new Formed();
            formed.Show();
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            StatusForm statusForm = new StatusForm();
            statusForm.Show();
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            Paymont paymont = new Paymont();
            paymont.Show();
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            Discipline discipline = new Discipline();
            discipline.Show();
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            Vacancy vacancy = new Vacancy();
            vacancy.Show();
        }

        private void button11_Click(object sender, RoutedEventArgs e)
        {
            Speciality speciality = new Speciality();
            speciality.Show();
        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            Exam exam = new Exam();
            exam.Show();
        }

        private void button12_Click(object sender, RoutedEventArgs e)
        {
            Discipline_vacancy discipline_Vacancy = new Discipline_vacancy();
            discipline_Vacancy.Show();
        }

        private void button13_Click(object sender, RoutedEventArgs e)
        {
            ExamResult examResult = new ExamResult();
            examResult.Show();
        }
    }
}
