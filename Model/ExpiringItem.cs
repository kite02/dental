using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class ExpiringItem:ValidatableModel
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value;
                RaisePropertyChanged("Id");
            }
        }

        private int supplierid;

        public int SupplierId
        {
            get { return supplierid; }
            set { supplierid = value;
                RaisePropertyChanged("SupplierId");
            }
        }


        private string suppliername;

        public string SupplierName
        {
            get { return suppliername; }
            set { suppliername = value;
                RaisePropertyChanged("SupplierName");
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

        private int months;

        public int Months
        {
            get { return months; }
            set
            {
                months = value;
                RaisePropertyChanged("Months");
            }
        }


        private int days;

        public int Days
        {
            get { return days; }
            set
            {
                days = value;
                RaisePropertyChanged("Days");
            }
        }

        private int years;

        public int Years
        {
            get { return years; }
            set
            {
                years = value;
                RaisePropertyChanged("Years");
            }
        }

        private string expirationdate;

        public string ExpirationDate
        {
            get { return expirationdate; }
            set { expirationdate = value;
                RaisePropertyChanged("ExpirationDate");
            }
        }


        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                RaisePropertyChanged("Quantity");
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

        private string color;

        public string Color
        {
            get { return color; }
            set { color = value;
                RaisePropertyChanged("Color");
            }
        }


    }
}
