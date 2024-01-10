using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class ServiceType :  ValidatableModel
    {

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; RaisePropertyChanged("Id"); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value;
                RaisePropertyChanged("Name");
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
