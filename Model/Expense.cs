using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class Expense:ValidatableModel
    {
        private string date;

        public string Date
        {
            get { return date; }
            set { date = value;
                RaisePropertyChanged("Date");
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

        private decimal price;

        public decimal Price
        {
            get { return price; }
            set { price = value;
                RaisePropertyChanged("Price");
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


        private decimal subtotal;

        public decimal Subtotal
        {
            get { return subtotal; }
            set { subtotal = value;
                RaisePropertyChanged("Subtotal");
            }
        }




    }
}
