using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class AccountLog :  ValidatableModel
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

        private int accountid;

        public int AccountId
        {
            get { return accountid; }
            set { accountid = value;
                RaisePropertyChanged("AccountId");
            }
        }


        private string firstname;

        public string Firstname
        {
            get { return firstname; }
            set { firstname = value;
                RaisePropertyChanged("Firstname");
            }
        }

        private string middlename;

        public string Middlename
        {
            get { return middlename; }
            set { middlename = value;
                RaisePropertyChanged("Middlename");
            }
        }


        private string lastname;

        public string Lastname
        {
            get { return lastname; }
            set { lastname = value;
                RaisePropertyChanged("Lastname");
            }
        }


        private DateTime datetimeloggedin;

        public DateTime DateTimeLoggedIn
        {
            get { return datetimeloggedin; }
            set { datetimeloggedin = value;
                RaisePropertyChanged("DateTimeLoggedIn");
            }
        }


        private DateTime datetimeloggedout;

        public DateTime DateTimeLoggedOut
        {
            get { return datetimeloggedout; }
            set
            {
                datetimeloggedout = value;
                RaisePropertyChanged("DateTimeLoggedOut");
            }
        }




       
    }
}
