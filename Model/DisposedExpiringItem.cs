using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class DisposedExpiringItem:ValidatableModel
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value;
                RaisePropertyChanged("Id");
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

        private string expirydate;

        public string ExpiryDate
        {
            get { return expirydate; }
            set { expirydate = value;
                RaisePropertyChanged("ExpiryDate");
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

        private string datedisposed;

        public string DateDisposed
        {
            get { return datedisposed; }
            set { datedisposed = value;
                RaisePropertyChanged("DateDisposed");
            }
        }







    }
}
