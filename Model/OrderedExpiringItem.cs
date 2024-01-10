using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class OrderedExpiringItem :  ValidatableModel
    {

        private int no;

        public int No
        {
            get { return no; }
            set { no = value;
                RaisePropertyChanged("No");
            }
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value;
                RaisePropertyChanged("Id");
            }
        }

        private int stockId;

        public int StockId
        {
            get { return stockId; }
            set { stockId = value;
                RaisePropertyChanged("StockId");
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



        private string supplier;

        public string Supplier
        {
            get { return supplier; }
            set { supplier = value;
                RaisePropertyChanged("Supplier");
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

        private int months;

        public int Months
        {
            get { return months; }
            set { months = value;
                RaisePropertyChanged("Months");
            }
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



        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value;
                RaisePropertyChanged("Quantity");
            }
        }


        private string datetimeordered;

        public string DateTimeOrdered
        {
            get { return datetimeordered; }
            set { datetimeordered = value;
                RaisePropertyChanged("DateTimeOrdered");
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
                RaisePropertyChanged("EmployeeName");
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
