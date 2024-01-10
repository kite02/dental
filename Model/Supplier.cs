using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class Supplier :  ValidatableModel
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

        private string supplierrepresentative;

        public string SupplierRepresentative
        {
            get { return supplierrepresentative; }
            set { supplierrepresentative = value;
                RaisePropertyChanged("SupplierRepresentative");
            }
        }


        private string address;

        public string Address
        {
            get { return address; }
            set { address = value;
                RaisePropertyChanged("Address");
            }
        }

        private string contactnumber;

        public string Contactnumber
        {
            get { return contactnumber; }
            set { contactnumber = value;
                RaisePropertyChanged("Contactnumber");
            }
        }

        private string emailaddress;

        public string EmailAddress
        {
            get { return emailaddress; }
            set { emailaddress = value;
                RaisePropertyChanged("EmailAddress");
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
