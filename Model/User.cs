using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.Model
{
    public class User : ValidatableModel
    {
        public User()
        {

        }


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

        private string firstname;

        public string Firstname
        {
            get { return firstname; }
            set
            {
                firstname = value;
                RaisePropertyChanged("FirstName");
            }
        }

        private string middlename;

        public string Middlename
        {
            get { return middlename; }
            set
            {
                middlename = value;
                RaisePropertyChanged("Middlename");
            }
        }

        private string lastname;

        public string Lastname
        {
            get { return lastname; }
            set
            {
                lastname = value;

                RaisePropertyChanged("Lastname");
            }
        }

        private string username;
        
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                RaisePropertyChanged("Username");
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                RaisePropertyChanged("Password");
            }
        }


        //private bool ismalechecked;

        //public bool IsMaleChecked
        //{
        //    get { return ismalechecked; }
        //    set { ismalechecked = value;
        //        HandleGender();
        //    }
        //}

        //private bool isfemalechecked;

        //public bool IsFemaleChecked
        //{
        //    get { return isfemalechecked; }
        //    set { isfemalechecked = value;
        //        HandleGender();
        //    }
        //}





        private string gender;

        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                RaisePropertyChanged("Gender");
            }
        }

        private string birthdate;

        public string Birthdate
        {
            get { return birthdate; }
            set
            {
                birthdate = value;
                RaisePropertyChanged("Birthdate");
                OnSelectedDateChanged();
            }
        }



        private int age;

        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                RaisePropertyChanged("Age");
            }
        }

        private string mobilenumber;

        public string Mobilenumber
        {
            get { return mobilenumber; }
            set
            {
                mobilenumber = value;
                RaisePropertyChanged("Mobilenumber");
            }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                RaisePropertyChanged("Address");
            }
        }


        //private bool isstaffchecked;

        //public bool IsStaffChecked
        //{
        //    get { return isstaffchecked; }
        //    set { isstaffchecked = value;
        //        HandleAccountType();
        //    }
        //}

        //private bool isdentistchecked;

        //public bool IsDentistChecked
        //{
        //    get { return isdentistchecked; }
        //    set { isdentistchecked = value;
        //        HandleAccountType();
        //    }
        //}





        private string type;

        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                RaisePropertyChanged("Type");
            }
        }

        private string status;

        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                RaisePropertyChanged("Status");
            }
        }


        public string Fullname
        {
            get
            {
                return Firstname + " " + Middlename[0] + ". " + Lastname;
            }
        }


        //private void checkGender()
        //{
        //    if (!String.IsNullOrEmpty(Gender))
        //    {
        //        if (Gender.Equals("Male"))
        //        {
        //            IsMaleChecked = true;
        //            IsFemaleChecked = false;
        //        }
        //        else if (Gender.Equals("Female"))
        //        {
        //            IsMaleChecked = false;
        //            IsFemaleChecked = true;
        //        }
        //        else
        //        {
        //            IsMaleChecked = true;
        //            IsFemaleChecked = false;
        //        }
        //    }

        //}

        //private void checkType()
        //{
        //    if (!String.IsNullOrEmpty(Type))
        //    {
        //        if (Type.Equals("Staff"))
        //        {
        //            IsStaffChecked = true;
        //            IsDentistChecked = false;
        //        }
        //        else if (Type.Equals("Dentist"))
        //        {
        //            IsStaffChecked = false;
        //            IsDentistChecked = true;
        //        }
        //        else
        //        {
        //            IsStaffChecked = true;
        //            IsDentistChecked = false;
        //        }
        //    }
        //}

        //private void HandleAccountType()
        //{
        //    if (IsDentistChecked == true && IsStaffChecked == false)
        //    {
        //        Type = "Dentist";
        //    }

        //    if (IsDentistChecked == false && IsStaffChecked == true)
        //    {
        //        Type = "Staff";
        //    }
        //}

        private void OnSelectedDateChanged()
        {
            Age = DateTime.Now.Year - Convert.ToDateTime(Birthdate).Year;
        }

        //private void HandleGender()
        //{
        //    if (IsFemaleChecked == true && IsMaleChecked == false)
        //    {
        //        Gender = "Female";
        //    }
        //    if (IsFemaleChecked == false && IsMaleChecked == true)
        //    {
        //        Gender = "Male";
        //    }
        //}
        



    }
}
