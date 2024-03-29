﻿using Microsoft.Graph;
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
    /// Логика взаимодействия для Person.xaml
    /// </summary>
    public partial class Person : Window
    {
        personTableAdapter personDataAdapter;
        isenrolleeDataSet dataTable;
        UserAuth con;

        public RolesViewModel ViewModel { get; set; }
        public Person()
        {
            InitializeComponent();
            ViewModel = new RolesViewModel();
            this.DataContext = ViewModel;

            con = UserAuth.getUserAuth();
            try
            {
                personDataAdapter = new personTableAdapter();
                personDataAdapter.Connection = con.con;
                dataTable = new isenrolleeDataSet();
                personDataAdapter.Fill(dataTable.person);
                dataGrid.ItemsSource = dataTable.person.DefaultView;
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selectedItem = dataGrid.SelectedItem as DataRowView;
            if (selectedItem != null)
            {
                textBox.Text = selectedItem[0].ToString();
                textBox1.Text = selectedItem[1].ToString();
                textBox2.Text = selectedItem[2].ToString();
                textBox3.Text = selectedItem[3].ToString();
                textBox4.Text = selectedItem[4].ToString();
                textBox5.Text = selectedItem[5].ToString();
                datePicker.Text = selectedItem[6].ToString();
                textBox6.Text = selectedItem[7].ToString();
                textBox7.Text = selectedItem[8].ToString();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            personDataAdapter.newPersonRow();
            // обноляем записи в таблице datagrid
            personDataAdapter.Fill(dataTable.person);
            // выбираем последнюю запись, это будет та что создали
            dataGrid.SelectedIndex = dataTable.person.Count - 1;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            int index = dataGrid.SelectedIndex;
            var selectedItem = dataGrid.SelectedItem;
            if (selectedItem != null)
            {
                dataTable.person.DefaultView.Delete(index);
            }

            personDataAdapter.Adapter.DeleteCommand = new NpgsqlCommandBuilder(personDataAdapter.Adapter).GetDeleteCommand();
            personDataAdapter.Update(dataTable.person);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

            DataRowView selectedItem = dataGrid.SelectedItem as DataRowView;
            if (selectedItem != null)
            {
                selectedItem.BeginEdit();
                //selectedItem[0] = textBox.Text;
                selectedItem[1] = textBox1.Text;
                selectedItem[2] = textBox2.Text;
                selectedItem[3] = textBox3.Text;
                selectedItem[4] = textBox4.Text;
                selectedItem[5] = textBox5.Text;
                selectedItem[6] = datePicker.SelectedDate;
                selectedItem[7] = textBox6.Text;
                selectedItem[8] = textBox7.Text;
                selectedItem.EndEdit();
            }
            personDataAdapter.Adapter.UpdateCommand = new NpgsqlCommandBuilder(personDataAdapter.Adapter).GetUpdateCommand();
            personDataAdapter.Update(dataTable.person);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedItem = dataGrid.SelectedItem as DataRowView;
            if (selectedItem != null)
            {
                Enrollee enrollee = new Enrollee(Convert.ToInt32(selectedItem[0]));
                enrollee.Show();
            }
            else
            {
                MessageBox.Show("Не выбран абитуриент!");
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            personDataAdapter.FillByFIO(dataTable.person,textBox8.Text,textBox8_Copy.Text,textBox8_Copy1.Text);
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            textBox8.Text = "";
            textBox8_Copy.Text = "";
            textBox8_Copy1.Text = "";
            personDataAdapter.Fill(dataTable.person);
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedItem = dataGrid.SelectedItem as DataRowView;
            if (selectedItem != null)
            {
                ExamResultPerson examResultPerson = new ExamResultPerson(Convert.ToInt32(selectedItem[0]));
                examResultPerson.Show();
            }
            else
            {
                MessageBox.Show("Не выбран абитуриент!");
            }
      
        }
    }
}
