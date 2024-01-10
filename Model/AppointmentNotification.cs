using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class AppointmentNotification:ValidatableModel
    {
        private string patientname;

        public string PatientName
        {
            get { return patientname; }
            set { patientname = value;
                RaisePropertyChanged("PatientName");
            }
        }

        private string date;

        public string Date
        {
            get { return date; }
            set { date = value;
                RaisePropertyChanged("Date");
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



        private string time;

        public string Time
        {
            get { return time; }
            set { time = value;
                RaisePropertyChanged("Time");
            }
        }

        private int daysleft;

        public int DaysLeft
        {
            get { return daysleft; }
            set { daysleft = value;
                RaisePropertyChanged("DaysLeft");
            }
        }




    }
}
