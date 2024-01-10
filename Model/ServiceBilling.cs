using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class ServiceBilling:ValidatableModel
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value;
                RaisePropertyChanged("Id");
            }
        }

        private int patientid;

        public int PatientId
        {
            get { return patientid; }
            set { patientid = value;
                RaisePropertyChanged("PatientId");
            }
        }

        private string patientname;

        public string PatientName
        {
            get { return patientname; }
            set { patientname = value;
                RaisePropertyChanged("PatientName");
            }
        }

        private int serviceid;

        public int ServiceId
        {
            get { return serviceid; }
            set { serviceid = value;
                RaisePropertyChanged("ServiceId");
            }
        }



        private string servicename;

        public string ServiceName
        {
            get { return servicename; }
            set { servicename = value;
                RaisePropertyChanged("ServiceName");
            }
        }

        private int tooth;

        public int Tooth
        {
            get { return tooth; }
            set { tooth = value;
                RaisePropertyChanged("Tooth");
            }
        }

        private decimal cost;

        public decimal Cost
        {
            get { return cost; }
            set { cost = value;
                RaisePropertyChanged("Cost");
            }
        }



        private decimal additional;

        public decimal Additional
        {
            get { return additional; }
            set { additional = value;
                RaisePropertyChanged("Additional");
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
            set
            {
                balance = value;
                RaisePropertyChanged("Balance");
            }
        }

        private string datetopay;

        public string DateToPay
        {
            get { return datetopay; }
            set { datetopay = value;
                RaisePropertyChanged("DateToPay");
            }
        }







    }
}
