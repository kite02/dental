using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class NonExpiringItem : ValidatableModel
    {
        private int id;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                RaisePropertyChanged("Id");
            }
        }

        private int supplierid;

        public int SupplierId
        {
            get { return supplierid; }
            set
            {
                supplierid = value;
                RaisePropertyChanged("SupplierId");
            }
        }


        private string suppliername;

        public string SupplierName
        {
            get { return suppliername; }
            set
            {
                suppliername = value;
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
            set
            {
                status = value;
                RaisePropertyChanged("Status");
            }
        }

        private string color;

        public string Color
        {
            get { return color; }
            set
            {
                color = value;
                RaisePropertyChanged("Color");
            }
        }
    }
}
