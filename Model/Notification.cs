using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class Notification :  ValidatableModel
    {
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value;
                RaisePropertyChanged("Type");
            }
        }

        private string details;

        public string Details
        {
            get { return details; }
            set { details = value;
                RaisePropertyChanged("Details");
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value;
                RaisePropertyChanged("Name");
            }
        }




        
    }
}
