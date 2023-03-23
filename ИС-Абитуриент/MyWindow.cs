using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ИС_Абитуриент
{
    public partial class MyWindow
    {
        public RolesViewModel ViewModel { get; set; }
        public RolesViewModel getDataContext()
        {
            return new RolesViewModel(); 
        }
    }
}
