using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ИС_Абитуриент
{
    public class RolesViewModel : INotifyPropertyChanged
    {
        private bool adminVisible = UserAuth.isAdmin;
        private bool employeeVisible = UserAuth.isEmployee;
        private bool enrolleeVisible = UserAuth.isEnrollee;

        public bool AdminVisible
        {
            get
            {
                return adminVisible;
            }

            set
            {
                adminVisible = value;
                NotifyPropertyChanged("AdminVisible");
            }
        }
        public bool EmployeeVisible
        {
            get
            {
                return employeeVisible;
            }

            set
            {
                employeeVisible = value;
                NotifyPropertyChanged("EmployeeVisible");
            }
        }
        public bool EnrolleeVisible
        {
            get
            {
                return enrolleeVisible;
            }

            set
            {
                enrolleeVisible = value;
                NotifyPropertyChanged("EnrolleeVisible");
            }
        }

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
