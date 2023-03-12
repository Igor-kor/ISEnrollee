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
        NpgsqlDataAdapter npgsqlDataAdapter;
        isenrolleeDataSet dataTable;
        public RolesViewModel ViewModel { get; set; }
        public Person()
        {
            InitializeComponent();
            ViewModel = new RolesViewModel();
            this.DataContext = ViewModel;
            
            UserAuth con = UserAuth.getUserAuth();
            try
            {
                var cmd = new NpgsqlDataAdapter("SELECT * FROM person", con.con);
                npgsqlDataAdapter = cmd;
                dataTable = new isenrolleeDataSet();
                npgsqlDataAdapter.UpdateCommand = new NpgsqlCommandBuilder(npgsqlDataAdapter).GetUpdateCommand();
                npgsqlDataAdapter.InsertCommand = new NpgsqlCommandBuilder(npgsqlDataAdapter).GetInsertCommand();
                npgsqlDataAdapter.DeleteCommand = new NpgsqlCommandBuilder(npgsqlDataAdapter).GetDeleteCommand();
                cmd.Fill(dataTable.person);
                // dataTable = table;
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
                textBox.Text = selectedItem.Row.ItemArray[0].ToString();
                textBox1.Text = selectedItem.Row.ItemArray[1].ToString();
                textBox2.Text = selectedItem.Row.ItemArray[2].ToString();
                textBox3.Text = selectedItem.Row.ItemArray[3].ToString();
                textBox4.Text = selectedItem.Row.ItemArray[4].ToString();
                textBox5.Text = selectedItem.Row.ItemArray[5].ToString();
                datePicker.SelectedDate = (DateTime?)selectedItem.Row.ItemArray[6];
                textBox6.Text = selectedItem.Row.ItemArray[7].ToString();
                textBox7.Text = selectedItem.Row.ItemArray[8].ToString();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            /*
             * сохраняем веденые данные а затем отправляем в бд
             */
            dataTable.person.Rows.Add(new object[] { textBox.Text, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, datePicker.Text, textBox6.Text, textBox7.Text });
     
            npgsqlDataAdapter.Update(dataTable.person);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
           

            int index = dataGrid.SelectedIndex;
            //dataGrid.Items.RemoveAt(index);
            var selectedItem = dataGrid.SelectedItem;
            if (selectedItem != null)
            {
                dataTable.person.DefaultView.Delete(index);
                //dataGrid.Items.Remove(selectedItem);
            }

            npgsqlDataAdapter.Update(dataTable.person);
        }
    }
}
