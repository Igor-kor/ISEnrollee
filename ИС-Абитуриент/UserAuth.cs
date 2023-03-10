using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Npgsql;

namespace ИС_Абитуриент
{
    internal class UserAuth
    {
        static UserAuth context = null;
        public NpgsqlConnection con = null;
        string Host = "127.0.0.1";
        string User = "isenrollee";
        string DBname = "isenrollee";
        string Password = "isenrollee";
        string Port = "5432";
        UserRole Role = UserRole.None;

        public UserRole getRole()
        {
            return Role;
        }

        public static bool isAdmin => (getUserAuth().Role == UserRole.Admin);

        public static bool isEmployee => ((getUserAuth().Role == UserRole.Employee) || (getUserAuth().Role == UserRole.Admin));

        public static bool isEnrollee => (getUserAuth().Role == UserRole.Enrollee);

        public static bool isNone=> (getUserAuth().Role == UserRole.None);

        public static UserAuth getUserAuth()
        {
            return context;
        }
        public UserAuth(string _Host, string _User, string _DBname, string _Password, string _Port)
        {
            context = this;
            Host = _Host;
            User = _User;
            DBname = _DBname;
            Password = _Password;
            Port = _Port;
        }

        public UserAuth(string _User, string _Password)
        {
            context = this;
            User = _User;
            Password = _Password;
        }

        public bool ConnectToDB()
        {
            string connString =
               String.Format(
                   "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
                   Host,
                   User,
                   DBname,
                   Port,
                   Password);
            con = new NpgsqlConnection(connString);
            try
            {
                con.Open();
                checkRoles();
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void checkRoles()
        {
            var sqlAdmin = "SELECT pg_has_role( current_user,'Admin', 'member')";
            var sqlEmployee = "SELECT pg_has_role( current_user,'Employee','member')";
            var sqlEnrollee = "SELECT pg_has_role( current_user,'Enrollee','member')";
            using (var cmd = new NpgsqlCommand(sqlAdmin, con))
            {
                if ((bool)cmd.ExecuteScalar())
                {
                    Role = UserRole.Admin;
                    return;
                }

            }
            using (var cmd = new NpgsqlCommand(sqlEmployee, con))
            {
                if ((bool)cmd.ExecuteScalar())
                {
                    Role = UserRole.Employee;
                    return;
                }

            }
            using (var cmd = new NpgsqlCommand(sqlEnrollee, con))
            {
                if ((bool)cmd.ExecuteScalar())
                {
                    Role = UserRole.Enrollee;
                    return;
                }

            }
            Role = UserRole.None;
        }

        public void CloseConnectToDB()
        {
            con.Close();
        }

        ~UserAuth()
        {
            CloseConnectToDB();
        }
    }
}
