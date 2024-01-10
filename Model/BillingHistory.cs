using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class BillingHistory:ValidatableModel
    {
        private string detail;

        public string Detail
        {
            get { return detail; }
            set
            {
                detail = value;
                RaisePropertyChanged("Detail");
            }
        }

        private string datetimehappen;

        public string DateTimeHappen
        {
            get { return datetimehappen; }
            set
            {
                datetimehappen = value;
                RaisePropertyChanged("DateTimeHappen");
            }
        }

        private string employeename;

        public string EmployeeName
        {
            get { return employeename; }
            set
            {
                employeename = value;
                RaisePropertyChanged("EmployeeName");
            }
        }
    }
}
