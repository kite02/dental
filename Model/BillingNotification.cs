using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class BillingNotification:ValidatableModel
    {
        private string patientname;

        public string PatientName
        {
            get { return patientname; }
            set { patientname = value;
                RaisePropertyChanged("PatientName");
            }
        }

        private string mobilenumber;

        public string MobileNumber
        {
            get { return mobilenumber; }
            set { mobilenumber = value;
                RaisePropertyChanged("MobileNumber");
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value;
            RaisePropertyChanged("Name");}
        }


        private string datetopay;

        public string DateToPay
        {
            get { return datetopay; }
            set { datetopay = value;
                RaisePropertyChanged("DateToPay");
            }
        }

        private decimal amounttopay;

        public decimal AmountToPay
        {
            get { return amounttopay; }
            set { amounttopay = value;
                RaisePropertyChanged("AmountToPay");
            }
        }


        private decimal balance;

        public decimal Balance
        {
            get { return balance; }
            set { balance = value;
                RaisePropertyChanged("Balance");
            }
        }

        private decimal totalbalance;

        public decimal TotalBalance
        {
            get { return totalbalance; }
            set { totalbalance = value;
                RaisePropertyChanged("TotalBalance");
            }
        }









    }
}
