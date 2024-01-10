using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class Schedule:ValidatableModel
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value;
                RaisePropertyChanged("Id");
            }
        }

        private int patientId;

        public int PatientId
        {
            get { return patientId; }
            set { patientId = value;
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

        private string time;

        public string Time
        {
            get { return time; }
            set { time = value;
                RaisePropertyChanged("Time");
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


        private string status;

        public string Status
        {
            get { return status; }
            set { status = value;
                RaisePropertyChanged("Status");
            }
        }

        








    }
}
