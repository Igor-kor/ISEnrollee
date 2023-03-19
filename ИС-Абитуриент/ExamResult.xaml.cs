using System;
using System.Collections.Generic;
using NGS.Templater;
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
using System.Data;

namespace ИС_Абитуриент
{
    /// <summary>
    /// Логика взаимодействия для ExamResult.xaml
    /// </summary>
    public partial class ExamResult : Window
    {
        isenrolleeDataSet dataTable;
        vacancyresultTableAdapter vacancyDataAdapter;

        vacancyTableAdapter vacancytDataAdapter;
        specialityTableAdapter specialitytDataAdapter;

        public RolesViewModel ViewModel { get; set; }
        public ExamResult(int vacancy_id = 0, int formed_id = 0)
        {
            InitializeComponent();
            ViewModel = new RolesViewModel();
            this.DataContext = ViewModel;
            UserAuth con = UserAuth.getUserAuth();
            vacancyDataAdapter = new vacancyresultTableAdapter();
            vacancytDataAdapter = new vacancyTableAdapter();
            specialitytDataAdapter = new specialityTableAdapter();
            vacancyDataAdapter.Connection = con.con;
            vacancytDataAdapter.Connection = con.con;
            specialitytDataAdapter.Connection = con.con;
            dataTable = new isenrolleeDataSet();
            vacancyDataAdapter.Fill(dataTable.vacancyresult, vacancy_id, formed_id);
            vacancytDataAdapter.FillBy2(dataTable.vacancy, vacancy_id);

            specialitytDataAdapter.FillBy(dataTable.speciality, Convert.ToInt32(dataTable.vacancy.Rows[0]["speciality_id"]));
            dataGrid.ItemsSource = dataTable.vacancyresult.DefaultView;
            using (var doc = Configuration.Factory.Open("WordTables.docx"))
            {
                doc.Process(
                new
                {
                    Table1 = dataTable.vacancyresult,
                    specname = dataTable.speciality.Rows[0]["name"],
                    speccount = dataTable.vacancy.Rows[0]["count"]
                });
            }
        }
    }
}
