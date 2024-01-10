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
    public class PatientViewModel :  ValidatableModel
    {

        public RelayCommand SavePatient { get; set; }

        public RelayCommand<object> HandleGender { get; set; }
        //public RelayCommand<object> HandleStatus { get; set; }
        public RelayCommand ClearText { get; set; }
        public RelayCommand SetAsActive { get; set; }
        public RelayCommand SetAsInactive { get; set; }
        public RelayCommand EditMode { get; set; }
        public RelayCommand EditPatient { get; set; }
        


        public Patient Patient { get; set; }

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

        private Patient selecteddentalhistory;

        public Patient SelectedDentalHistory
        {
            get { return selecteddentalhistory; }
            set { selecteddentalhistory = value;
                RaisePropertyChanged("SelectedDentalHistory");
                if(SelectedDentalHistory != null)
                {
                    loadDentalHistory();
                }
            }
        }

        private Patient selectedbillinghistory;

        public Patient SelectedBillingHistory
        {
            get { return selectedbillinghistory; }
            set
            {
                selectedbillinghistory = value;
                RaisePropertyChanged("SelectedBillingHistory");
                if (SelectedBillingHistory != null)
                {
                    loadBillingHistory();
                }
            }
        }


        public ObservableCollection<DentalHistory> DentalHistories { get; set; }
        public ObservableCollection<BillingHistory> BillingHistories { get; set; }




        public int AccountId { get; set; }

        private Patient selectedpatient;

        public Patient SelectedPatient
        {
            get { return selectedpatient; }
            set
            {
                selectedpatient = value;
                RaisePropertyChanged("SelectedPatient");
            }
        }

        


        public PatientViewModel()
        {
            Patient = new Patient();
            Patients = new ObservableCollection<Patient>();
            DentalHistories = new ObservableCollection<DentalHistory>();
            BillingHistories = new ObservableCollection<BillingHistory>();
            AccountId = 0;
            setup();
            PatientView = CollectionViewSource.GetDefaultView(Patients);
            PatientView.Filter = patientviewfilter;
            SavePatient = new RelayCommand(savePatient, checkAllFields);
            HandleGender = new RelayCommand<object>(param => handleGender(param));
            SetAsActive = new RelayCommand(setasactive, cansetasactive);
            SetAsInactive = new RelayCommand(setasinactive, cansetasinactive);
            EditMode = new RelayCommand(editmode);
            EditPatient = new RelayCommand(editpatient,caneditpatient);
            //HandleStatus = new RelayCommand<object>(param => handleStatus(param));
            ClearText = new RelayCommand(cleartext);
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

        


        public void setup()
        {

            Patients.Clear();
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            connection.Open();
            String query = "SELECT * FROM `patient`";
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

        public void savePatient()
        {

            MessageBoxResult dialogResult = MessageBox.Show("Are you sure to add this new Patient?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dialogResult == MessageBoxResult.Yes)
            {
                MySqlConnection connection;
                String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
                connection = new MySqlConnection(connString);
                try
                {
                    connection.Open();
                    String query = "INSERT INTO `patient` VALUES (null,@firstname,@middlename,@lastname,@gender,@birthdate,@age,@mobilenumber,@address,@status)";
                    MySqlCommand comm = new MySqlCommand(query, connection);
                    comm.Parameters.AddWithValue("@firstname", Patient.Firstname);
                    comm.Parameters.AddWithValue("@middlename", Patient.Middlename);
                    comm.Parameters.AddWithValue("@lastname", Patient.Lastname);
                    comm.Parameters.AddWithValue("@gender", Patient.Gender);
                    comm.Parameters.AddWithValue("@birthdate", Convert.ToDateTime(Patient.Birthdate).ToShortDateString());
                    comm.Parameters.AddWithValue("@age", Patient.Age);
                    comm.Parameters.AddWithValue("@mobilenumber", Patient.Mobilenumber);
                    comm.Parameters.AddWithValue("@address", Patient.Address);
                    comm.Parameters.AddWithValue("@status", "Active");
                    comm.ExecuteNonQuery();

                    query = "SELECT MAX(`id`) FROM `patient`";
                    comm = new MySqlCommand(query, connection);
                    int patientid = Convert.ToInt32(comm.ExecuteScalar());

                    query = "INSERT INTO `excessfee` VALUES (null,@patient_id,0)";
                    comm = new MySqlCommand(query, connection);
                    comm.Parameters.AddWithValue("@patient_id", patientid);
                    comm.ExecuteNonQuery();

                    TransactionLog transactionLog = new TransactionLog();
                    transactionLog.Description = "Manage Patient";
                    var date = DateTime.Now;
                    date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
                    transactionLog.Transactedin = date;
                    transactionLog.Details = "Added New Patient: " + Patient.Firstname + " " + Patient.Middlename + " " + Patient.Lastname;
                    transactionLog.AccountId = this.AccountId;


                    query = "INSERT INTO `transactionlog` VALUES (null,@description,@transactedin,@details,@account_id)";
                    comm = new MySqlCommand(query, connection);
                    comm.Parameters.AddWithValue("@description", transactionLog.Description);
                    comm.Parameters.AddWithValue("@transactedin", transactionLog.Transactedin);
                    comm.Parameters.AddWithValue("@details", transactionLog.Details);
                    comm.Parameters.AddWithValue("@account_id", transactionLog.AccountId);
                    comm.ExecuteNonQuery();

                    MessageBox.Show("New Patient Added!");
                    setup();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message, "MySQL ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }

                Patient.Firstname = String.Empty;
                Patient.Middlename = String.Empty;
                Patient.Lastname = String.Empty;
                Patient.Mobilenumber = String.Empty;
                Patient.Address = String.Empty;
            }
        }

        public void handleGender(object parameter)
        {
            Patient.Gender = (string)parameter;
        }



        //public void handleStatus(object parameter)
        //{
        //    Patient.Status = (string)parameter;
        //}


        public bool checkAllFields()
        {
            return !String.IsNullOrWhiteSpace(Patient.Firstname) && !String.IsNullOrWhiteSpace(Patient.Lastname) && !String.IsNullOrWhiteSpace(Patient.Gender) && !String.IsNullOrWhiteSpace(Patient.Birthdate)
                //&& !String.IsNullOrWhiteSpace(Patient.Age.ToString()) 
                && !String.IsNullOrWhiteSpace(Patient.Mobilenumber) && !String.IsNullOrWhiteSpace(Patient.Address);
        }

        private void cleartext()
        {
            Patient.Firstname = String.Empty;
            Patient.Middlename = String.Empty;
            Patient.Lastname = String.Empty;
            Patient.Mobilenumber = String.Empty;
            Patient.Address = String.Empty;

        }

        private void setasactive()
        {
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "UPDATE `patient` SET `status` = @status WHERE `id` = @id";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@status", "Active");
                comm.Parameters.AddWithValue("@id", SelectedPatient.Id);
                comm.ExecuteNonQuery();

                TransactionLog transactionLog = new TransactionLog();
                transactionLog.Description = "Manage Patient";
                var date = DateTime.Now;
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
                transactionLog.Transactedin = date;
                transactionLog.Details = "Patient No:" + SelectedPatient.Id + " marked as Active";
                transactionLog.AccountId = this.AccountId;

                query = "INSERT INTO `transactionlog` VALUES (null,@description,@transactedin,@details,@account_id)";
                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@description", transactionLog.Description);
                comm.Parameters.AddWithValue("@transactedin", transactionLog.Transactedin);
                comm.Parameters.AddWithValue("@details", transactionLog.Details);
                comm.Parameters.AddWithValue("@account_id", transactionLog.AccountId);
                comm.ExecuteNonQuery();

                MessageBox.Show("Patient No:" + SelectedPatient.Id + " marked as Active");
                setup();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "MySQL ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void setasinactive()
        {
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "UPDATE `patient` SET `status` = @status WHERE `id` = @id";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@status", "Inactive");
                comm.Parameters.AddWithValue("@id", SelectedPatient.Id);
                comm.ExecuteNonQuery();

                TransactionLog transactionLog = new TransactionLog();
                transactionLog.Description = "Manage Patient";
                var date = DateTime.Now;
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
                transactionLog.Transactedin = date;
                transactionLog.Details = "Patient No:" + SelectedPatient.Id + " marked as Inactive";
                transactionLog.AccountId = this.AccountId;

                query = "INSERT INTO `transactionlog` VALUES (null,@description,@transactedin,@details,@account_id)";
                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@description", transactionLog.Description);
                comm.Parameters.AddWithValue("@transactedin", transactionLog.Transactedin);
                comm.Parameters.AddWithValue("@details", transactionLog.Details);
                comm.Parameters.AddWithValue("@account_id", transactionLog.AccountId);
                comm.ExecuteNonQuery();

                MessageBox.Show("Patient No:" + SelectedPatient.Id + " marked as Inactive");
                setup();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "MySQL ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }

        }
        private bool cansetasactive()
        {
            if (SelectedPatient != null)
                return SelectedPatient.Status == "Inactive";
            return false;
        }

        private bool cansetasinactive()
        {
            if (SelectedPatient != null)
                return SelectedPatient.Status == "Active";
            return false;
        }

        private void editmode()
        {
            if (SelectedPatient != null)
            {
                Patient.Firstname = SelectedPatient.Firstname;
                Patient.Middlename = SelectedPatient.Middlename;
                Patient.Lastname = SelectedPatient.Lastname;
                Patient.Birthdate = SelectedPatient.Birthdate;
                Patient.Mobilenumber = SelectedPatient.Mobilenumber;
                Patient.Address = SelectedPatient.Address;
            }

        }
        private void editpatient()
        {
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "UPDATE `patient` SET `firstname` = @firstname,`middlename` = @middlename,`lastname` = @lastname,`gender` = @gender,`birthdate` = @birthdate,`age` = @age, `mobilenumber` = @mobilenumber, `address` = @address WHERE `id` = @id";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@firstname", Patient.Firstname);
                comm.Parameters.AddWithValue("@middlename", Patient.Middlename);
                comm.Parameters.AddWithValue("@lastname", Patient.Lastname);
                comm.Parameters.AddWithValue("@gender", Patient.Gender);
                comm.Parameters.AddWithValue("@birthdate", Convert.ToDateTime(Patient.Birthdate).ToShortDateString());
                comm.Parameters.AddWithValue("@age", Patient.Age);
                comm.Parameters.AddWithValue("@mobilenumber", Patient.Mobilenumber);
                comm.Parameters.AddWithValue("@address", Patient.Address);
                comm.Parameters.AddWithValue("@id", SelectedPatient.Id);
                comm.ExecuteNonQuery();

                TransactionLog transactionLog = new TransactionLog();
                transactionLog.Description = "Manage Patient";
                var date = DateTime.Now;
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
                transactionLog.Transactedin = date;
                transactionLog.Details = "Patient Id: " + SelectedPatient.Id + " is edited";
                transactionLog.AccountId = this.AccountId;

                query = "INSERT INTO `transactionlog` VALUES (null,@description,@transactedin,@details,@account_id)";
                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@description", transactionLog.Description);
                comm.Parameters.AddWithValue("@transactedin", transactionLog.Transactedin);
                comm.Parameters.AddWithValue("@details", transactionLog.Details);
                comm.Parameters.AddWithValue("@account_id", transactionLog.AccountId);
                comm.ExecuteNonQuery();

                MessageBox.Show("Patient Id: " + SelectedPatient.Id + " is edited");
                setup();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "MySQL ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private bool caneditpatient()
        {
            return SelectedPatient != null;
        }

        private void loadDentalHistory()
        {
            DentalHistories.Clear();
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "SELECT `dentalhistory`.`transaction`,`dentalhistory`.`datetimehappen`,`account`.`firstname`,`account`.`middlename`,`account`.`lastname` FROM `dentalhistory` INNER JOIN `account` ON `dentalhistory`.`account_id` = `account`.`id` WHERE `dentalhistory`.`patient_id` = @patient_id";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@patient_id", SelectedDentalHistory.Id);
                MySqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    DentalHistory newDentalHistory = new DentalHistory();
                    newDentalHistory.Detail = reader[0].ToString();
                    newDentalHistory.DateTimeHappen = reader[1].ToString();
                    newDentalHistory.EmployeeName = reader[2].ToString() + " " + reader[3].ToString() + " " + reader[4].ToString();
                    DentalHistories.Add(newDentalHistory);
                }

                reader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "MySQL ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void loadBillingHistory()
        {
            BillingHistories.Clear();
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "SELECT `billinghistory`.`transaction`,`billinghistory`.`datetimehappen`,`account`.`firstname`,`account`.`middlename`,`account`.`lastname` FROM `billinghistory` INNER JOIN `account` ON `billinghistory`.`account_id` = `account`.`id` WHERE `billinghistory`.`patient_id` = @patient_id";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@patient_id", SelectedBillingHistory.Id);
                MySqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    BillingHistory newBillingHistory = new BillingHistory();
                    newBillingHistory.Detail = reader[0].ToString();
                    newBillingHistory.DateTimeHappen = reader[1].ToString();
                    newBillingHistory.EmployeeName = reader[2].ToString() + " " + reader[3].ToString() + " " + reader[4].ToString();
                    BillingHistories.Add(newBillingHistory);
                }

                reader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "MySQL ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
