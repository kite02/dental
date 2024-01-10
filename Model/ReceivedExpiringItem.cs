using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class ReceivedExpiringItem :  ValidatableModel
    {
        private int no;

        public int No
        {
            get { return no; }
            set { no = value;
                RaisePropertyChanged("No");
            }
        }


        private string id;

        public string Id
        {
            get { return id; }
            set { id = value;
                RaisePropertyChanged("Id");
            }
        }


        private string datereceived;

        public string DateReceived
        {
            get { return datereceived; }
            set
            {
                datereceived = value;
                RaisePropertyChanged("DateReceived");
            }
        }

        private int poid;

        public int PoId
        {
            get { return poid; }
            set { poid = value;
                RaisePropertyChanged("PoId"); }
        }

        private int ponumber;

        public int PoNumber
        {
            get { return ponumber; }
            set { ponumber = value;
                RaisePropertyChanged("PoNumber");
            }
        }

        private int quantityordered;

        public int QuantityOrdered
        {
            get { return quantityordered; }
            set { quantityordered = value;
                RaisePropertyChanged("QuantityOrdered");
            }
        }






        private int stockid;

        public int StockId
        {
            get { return stockid; }
            set { stockid = value;
                RaisePropertyChanged("StockId");
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

        private string expirationdate;

        public string ExpirationDate
        {
            get { return expirationdate; }
            set { expirationdate = value;
                RaisePropertyChanged("ExpirationDate");
            }
        }

        private decimal price;

        public decimal Price
        {
            get { return price; }
            set { price = value; RaisePropertyChanged("Price"); }
        }


        private int quantityreceived;

        public int QuantityReceived
        {
            get { return quantityreceived; }
            set
            {
                quantityreceived = value;
                RaisePropertyChanged("QuantityReceived");
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


        private int employeeid;

        public int EmployeeId
        {
            get { return employeeid; }
            set { employeeid = value;
                RaisePropertyChanged("EmployeeId");
            }
        }

        private string employeename;

        public string EmployeeName
        {
            get { return employeename; }
            set { employeename = value;
                RaisePropertyChanged("EmployeeName"); }
        }


       
        private int months;

        public int Months
        {
            get { return months; }
            set { months = value;
                RaisePropertyChanged("Months"); }
        }


        private int days;

        public int Days
        {
            get { return days; }
            set { days = value;
                RaisePropertyChanged("Days");
            }
        }

        private int years;

        public int Years
        {
            get { return years; }
            set { years = value;
                RaisePropertyChanged("Years");
            }
        }





        private decimal subtotal;

        public decimal Subtotal
        {
            get { return subtotal; }
            set { subtotal = value;
                RaisePropertyChanged("Subtotal");
            }
        }

        private decimal totalcost;

        public decimal TotalCost
        {
            get { return totalcost; }
            set { totalcost = value;
                RaisePropertyChanged("TotalCost");
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

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value;
                RaisePropertyChanged("Type");
            }
        }





    }
}
