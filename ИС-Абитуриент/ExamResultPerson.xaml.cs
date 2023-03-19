using System;
using System.Collections.Generic;
using System.IO;
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
using NGS.Templater;

namespace ИС_Абитуриент
{
    /// <summary>
    /// Логика взаимодействия для ExamResultPerson.xaml
    /// </summary>
    public partial class ExamResultPerson : Window
    {
        isenrolleeDataSet dataTable;
        examTableAdapter examDataAdapter;

        isenrolleeDataSet datasetCombobox;
        disciplineTableAdapter disciplineComboboxData;
        personTableAdapter personComboboxData;
        public RolesViewModel ViewModel { get; set; }
        public ExamResultPerson(int person_id = 0)
        {
            InitializeComponent();
            ViewModel = new RolesViewModel();
            this.DataContext = ViewModel;
            UserAuth con = UserAuth.getUserAuth();
            examDataAdapter = new examTableAdapter();
            examDataAdapter.Connection = con.con;
            personComboboxData = new personTableAdapter();
            personComboboxData.Connection = con.con;
            dataTable = new isenrolleeDataSet();
            if (ViewModel.EnrolleeVisible)
            {
                person_id = con.person_id;
            }
            examDataAdapter.FillBy1(dataTable.exam, person_id);
            personComboboxData.FillByPersonid(dataTable.person, person_id);
            dataGrid.ItemsSource = dataTable.exam.DefaultView;
            label.Content = dataTable.person.DefaultView[0].Row[1].ToString()+ " "+
                dataTable.person.DefaultView[0].Row[2].ToString() + " " +
                dataTable.person.DefaultView[0].Row[3].ToString();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Отчет"; // Default file name
            dialog.DefaultExt = ".docx"; // Default file extension
            dialog.Filter = "Word documents (.docx)|*.docx"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                var outfile = File.Open(dialog.FileName, FileMode.OpenOrCreate);
                using (var doc = Configuration.Factory.Open(File.Open("examperson.docx", FileMode.Open), "docx", outfile))
                {
                    doc.Process(
                    new
                    {
                        table1 = dataTable.exam,
                        firstname = dataTable.person.Rows[0]["firstname"],
                        lastname = dataTable.person.Rows[0]["lastname"],
                        patronomic = dataTable.person.Rows[0]["patronimic"]
                    });
                    
                }
                outfile.Close();
            }
        }
    }
}
