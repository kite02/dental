using GalaSoft.MvvmLight.CommandWpf;
using MySql.Data.MySqlClient;
using SmileLineDentalClinic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SmileLineDentalClinic.ViewModel
{
    public class PatientWindowViewModel:ValidatableModel
    {
        public ObservableCollection<Patient> Patients { get; set; }
        public ICollectionView PatientView { get; set; }

        private string searchpatient;

        public string SearchPatient
        {
            get { return searchpatient; }
            set
            {
                searchpatient = value;
                RaisePropertyChanged("SearchPatient");
                PatientView.Refresh();
            }
        }

        private int patientid;

        public int PatientId
        {
            get { return patientid; }
            set {
                patientid = value;
                RaisePropertyChanged("PatientId");
            }
        }


        private string patientname;

        public string PatientName
        {
            get { return patientname; }
            set { patientname = value;
                RaisePropertyChanged("PatientName");
            }
        }

        private Patient selectedpatient;

        public Patient SelectedPatient
        {
            get { return selectedpatient; }
            set { selectedpatient = value;
                RaisePropertyChanged("SelectedPatient");
                if(SelectedPatient != null)
                {
                    PatientName = SelectedPatient.Firstname + " " + SelectedPatient.Middlename + " " + SelectedPatient.Lastname;
                }
            }
        }


        public RelayCommand<object> SelectPatientNow { get; set; }


        public PatientWindowViewModel()
        {
            Patients = new ObservableCollection<Patient>();
            setup();
            PatientView = CollectionViewSource.GetDefaultView(Patients);
            PatientView.Filter = patientviewfilter;
            SelectPatientNow = new RelayCommand<object>(selectpatientnow);
        }

        private void selectpatientnow(object param)
        {
            Window thisWindow = param as Window;
            if (SelectedPatient != null)
                PatientId = SelectedPatient.Id;
            thisWindow.Close();
        }

        private bool patientviewfilter(object item)
        {
            if (String.IsNullOrEmpty(SearchPatient))
                return true;
            else
                return ((item as Patient).Id.ToString().IndexOf(SearchPatient, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as Patient).Firstname.IndexOf(SearchPatient, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as Patient).Middlename.IndexOf(SearchPatient, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as Patient).Lastname.IndexOf(SearchPatient, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as Patient).Address.IndexOf(SearchPatient, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as Patient).Birthdate.IndexOf(SearchPatient, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as Patient).Age.ToString().IndexOf(SearchPatient, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as Patient).Mobilenumber.IndexOf(SearchPatient, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as Patient).Status.IndexOf(SearchPatient, StringComparison.OrdinalIgnoreCase) >= 0);

        }

        private void setup()
        {
            Patients.Clear();
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            connection.Open();
            String query = "SELECT * FROM `patient` WHERE `status` = 'Active'";
            MySqlCommand comm = new MySqlCommand(query, connection);
            MySqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Patient newPatient = new Patient();
                newPatient.Id = Convert.ToInt32(reader["id"]);
                newPatient.Firstname = reader["firstname"].ToString();
                newPatient.Middlename = reader["middlename"].ToString();
                newPatient.Lastname = reader["lastname"].ToString();
                newPatient.Gender = reader["gender"].ToString();
                newPatient.Birthdate = reader["birthdate"].ToString();
                newPatient.Age = Convert.ToInt32(reader["age"]);
                newPatient.Mobilenumber = reader["mobilenumber"].ToString();
                newPatient.Address = reader["address"].ToString();
                newPatient.Status = reader["status"].ToString();
                Patients.Add(newPatient);
            }
            reader.Close();
        }

    }
}
