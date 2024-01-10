using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class IncomeSale:ValidatableModel
    {

        private string date;

        public string Date
        {
            get { return date; }
            set { date = value;
                RaisePropertyChanged("Date");
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


        private string treatment;

        public string Treatment
        {
            get { return treatment; }
            set { treatment = value;
                RaisePropertyChanged("Treatment");
            }
        }

        private decimal paid;

        public decimal Paid
        {
            get { return paid; }
            set { paid = value;
                RaisePropertyChanged("Paid");
            }
        }



    }
}
