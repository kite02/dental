using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class UseItem:ValidatableModel
    {

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
