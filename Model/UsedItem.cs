using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class UsedItem:ValidatableModel
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value;
                RaisePropertyChanged("Name");
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

        private string dateused;

        public string DateUsed
        {
            get { return dateused; }
            set { dateused = value;
                RaisePropertyChanged("DateUsed");
            }
        }                                   



    }
}
