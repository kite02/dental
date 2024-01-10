using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class ExpiryNotification :  ValidatableModel
    {

        private string status;

        public string Status
        {
            get { return status; }
            set { status = value;
                RaisePropertyChanged("Status");
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }
        private string expirationdate;

        public string ExpirationDate
        {
            get
            {
                return expirationdate;
            }
            set
            {
                expirationdate = value;
                RaisePropertyChanged("ExpirationDate");
            }
        }

        private int daysleft;

        public int Daysleft
        {
            get { return daysleft; }
            set { daysleft = value;
                RaisePropertyChanged("DaysLeft");
            }
        }


       
    }
}
