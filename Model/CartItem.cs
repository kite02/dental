using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class CartItem:ValidatableModel
    {
        private int stockid;

        public int StockId
        {
            get { return stockid; }
            set { stockid = value;
                RaisePropertyChanged("StockId");
            }
        }

        private string stockname;

        public string StockName
        {
            get { return stockname; }
            set { stockname = value;
                RaisePropertyChanged("StockName");
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

        public decimal SubTotal
        {
            get { return subtotal; }
            set { subtotal = value;
                RaisePropertyChanged("SubTotal");
            }
        }




    }
}
