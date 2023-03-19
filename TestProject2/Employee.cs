using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ИС_Абитуриент;
using ИС_Абитуриент.isenrolleeDataSetTableAdapters;

namespace TestProject2
{
    internal class Employee
    {
        UserAuth auth;
        isenrolleeDataSet isenrolleeDataSet;
        [SetUp]
        public void Setup()
        {
             auth = new UserAuth("EmployeeTest", "EmployeeTest");
             UserAuth.getUserAuth();
             isenrolleeDataSet = new isenrolleeDataSet();
        }

        [Test]
        public void Test1()
        {
            Assert.IsTrue(auth.ConnectToDB(), "Нет подключения к бд");
        }

        [Test]
        public void Test2()
        {
            auth.ConnectToDB();
            Assert.IsTrue(UserAuth.isEmployee, "Роль пользователя не Сотрудник!");
        }

        [Test]
        public void Test3()
        {
            try
            {
                auth.ConnectToDB();
                paymontTableAdapter paymontDataAdapter;
                paymontDataAdapter = new paymontTableAdapter();
                paymontDataAdapter.Connection = UserAuth.getUserAuth().con;
                paymontDataAdapter.Fill(isenrolleeDataSet.paymont);
            }
            catch{
                Assert.Fail();
            }
            Assert.Pass();
        }

        [Test]
        public void Test4()
        {
            try
            {
                auth.ConnectToDB();
                disciplineTableAdapter DataAdapter;
                DataAdapter = new disciplineTableAdapter();
                DataAdapter.Connection = UserAuth.getUserAuth().con;
                DataAdapter.Fill(isenrolleeDataSet.discipline);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.Pass();
        }

        [Test]
        public void Test5()
        {
            try
            {
                auth.ConnectToDB();
                discipline_vacancyTableAdapter DataAdapter;
                DataAdapter = new discipline_vacancyTableAdapter();
                DataAdapter.Connection = UserAuth.getUserAuth().con;
                DataAdapter.Fill(isenrolleeDataSet.discipline_vacancy);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.Pass();
        }

        [Test]
        public void Test6()
        {
            try
            {
                auth.ConnectToDB();
                enrolleeTableAdapter DataAdapter;
                DataAdapter = new enrolleeTableAdapter();
                DataAdapter.Connection = UserAuth.getUserAuth().con;
                DataAdapter.Fill(isenrolleeDataSet.enrollee);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.Pass();
        }

        [Test]
        public void Test7()
        {
            try
            {
                auth.ConnectToDB();
                examTableAdapter DataAdapter;
                DataAdapter = new examTableAdapter();
                DataAdapter.Connection = UserAuth.getUserAuth().con;
                DataAdapter.Fill(isenrolleeDataSet.exam);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.Pass();
        }

        [Test]
        public void Test8()
        {
            try
            {
                auth.ConnectToDB();
                enrolleeTableAdapter DataAdapter;
                DataAdapter = new enrolleeTableAdapter();
                DataAdapter.Connection = UserAuth.getUserAuth().con;
                DataAdapter.Fill(isenrolleeDataSet.enrollee);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.Pass();
        }

        [Test]
        public void Test9()
        {
            try
            {
                auth.ConnectToDB();
                formedTableAdapter DataAdapter;
                DataAdapter = new formedTableAdapter();
                DataAdapter.Connection = UserAuth.getUserAuth().con;
                DataAdapter.Fill(isenrolleeDataSet.formed);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.Pass();
        }

        [Test]
        public void Test10()
        {
            try
            {
                auth.ConnectToDB();
                personTableAdapter DataAdapter;
                DataAdapter = new personTableAdapter();
                DataAdapter.Connection = UserAuth.getUserAuth().con;
                DataAdapter.Fill(isenrolleeDataSet.person);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.Pass();
        }

        [Test]
        public void Test11()
        {
            try
            {
                auth.ConnectToDB();
                specialityTableAdapter DataAdapter;
                DataAdapter = new specialityTableAdapter();
                DataAdapter.Connection = UserAuth.getUserAuth().con;
                DataAdapter.Fill(isenrolleeDataSet.speciality);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.Pass();
        }

        [Test]
        public void Test12()
        {
            try
            {
                auth.ConnectToDB();
                vacancyTableAdapter DataAdapter;
                DataAdapter = new vacancyTableAdapter();
                DataAdapter.Connection = UserAuth.getUserAuth().con;
                DataAdapter.Fill(isenrolleeDataSet.vacancy);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.Pass();
        }
    }
}
