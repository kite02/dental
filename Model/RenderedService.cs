using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class RenderedService : ValidatableModel
    {
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

        private int tooth;

        public int Tooth
        {
            get { return tooth; }
            set
            {
                tooth = value;
                RaisePropertyChanged("Tooth");
            }
        }

        private decimal cost;

        public decimal Cost
        {
            get { return cost; }
            set
            {
                cost = value;
                RaisePropertyChanged("Cost");
            }
        }

        private int serviceid;

        public int ServiceId
        {
            get { return serviceid; }
            set { serviceid = value;
                RaisePropertyChanged("ServiceId");
            }
        }


        private decimal additional;

        public decimal Additional
        {
            get { return additional; }
            set
            {
                additional = value;
                RaisePropertyChanged("Additional");
            }
        }

        private decimal subtotal;

        public decimal SubTotal
        {
            get { return subtotal; }
            set
            {
                subtotal = value;
                RaisePropertyChanged("SubTotal");
            }
        }






    }
}
