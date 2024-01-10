using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class Service :  ValidatableModel
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value;
                RaisePropertyChanged("Id");
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

        private string serviceType;

        public string ServiceType
        {
            get { return serviceType; }
            set { serviceType = value;
                RaisePropertyChanged("ServiceType");
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
