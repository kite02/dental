using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class TransactionLog :  ValidatableModel
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

        private string description;

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                RaisePropertyChanged("Description");
            }
        }


        private DateTime transactedin;

        public DateTime Transactedin
        {
            get { return transactedin; }
            set
            {
                transactedin = value;
                RaisePropertyChanged("Transactedin");
            }
        }

        private string details;

        public string Details
        {
            get { return details; }
            set { details = value;
                RaisePropertyChanged("Details");
            }
        }

        private int accountid;

        public int AccountId
        {
            get { return accountid; }
            set { accountid = value;
                RaisePropertyChanged("AccountId");
            }
        }


        private string transactedby;

        public string Transactedby
        {
            get { return transactedby; }
            set { transactedby = value;
                RaisePropertyChanged("Transactedby");
            }
        }






       
    }
}
