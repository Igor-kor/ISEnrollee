using ��_����������;
using System.Runtime.InteropServices;
using Npgsql;

namespace TestProject2
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            UserAuth auth = UserAuth.getUserAuth();
            Assert.IsTrue(auth.ConnectToDB(), "��� ����������� � ��");
        }
    }
}