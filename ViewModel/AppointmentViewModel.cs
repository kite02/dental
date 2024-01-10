
using GalaSoft.MvvmLight.CommandWpf;
using MySql.Data.MySqlClient;
using SmileLineDentalClinic.Model;
using SmileLineDentalClinic.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace SmileLineDentalClinic.ViewModel
{


    public class AppointmentViewModel : ValidatableModel
    {

        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Schedule> Schedules { get; set; }
        public ICollectionView PatientsView { get; set; }
        private Patient selectedpatient;

        public Patient SelectedPatient
        {
            get { return selectedpatient; }
            set
            {
                selectedpatient = value;
                RaisePropertyChanged("SelectedPatient");
                if (SelectedPatient != null)
                    FullName = SelectedPatient.Firstname + " " + SelectedPatient.Middlename + " " + SelectedPatient.Lastname;
            }
        }


        private string searchpatient;

        public string SearchPatient
        {
            get { return searchpatient; }
            set
            {
                searchpatient = value;
                RaisePropertyChanged("SearchPatient");
                PatientsView.Refresh();
            }
        }

        private DateTime scheduledate;

        public DateTime ScheduleDate
        {
            get { return scheduledate; }
            set
            {
                scheduledate = value;
                RaisePropertyChanged("ScheduleDate");
                selectdate();
            }
        }

        private int accountid;

        public int AccountId
        {
            get { return accountid; }
            set
            {
                accountid = value;
                RaisePropertyChanged("AccountId");
            }
        }

        private string fullname;

        public string FullName
        {
            get { return fullname; }
            set
            {
                fullname = value;
                RaisePropertyChanged("FullName");
            }
        }


        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                RaisePropertyChanged("Date");
                settimesched();
            }
        }

        private ObservableCollection<string> timeschedules;

        public ObservableCollection<string> TimeSchedules
        {
            get { return timeschedules; }
            set
            {
                timeschedules = value;
                RaisePropertyChanged("TimeSchedules");
            }
        }

        private string selectedtime;

        public string SelectedTime
        {
            get { return selectedtime; }
            set
            {
                selectedtime = value;
                RaisePropertyChanged("SelectedTime");
            }
        }

        private Schedule selectedschedule;

        public Schedule SelectedSchedule
        {
            get { return selectedschedule; }
            set
            {
                selectedschedule = value;
                RaisePropertyChanged("SelectedSchedule");
            }
        }

        private Service selectedservice;

        public Service SelectedService
        {
            get { return selectedservice; }
            set
            {
                selectedservice = value;
                RaisePropertyChanged("SelectedService");
            }

        }






        public RelayCommand SaveAppointment { get; set; }
        public RelayCommand SelectDate { get; set; }
        public RelayCommand AddDay { get; set; }
        public RelayCommand MinusDay { get; set; }
        public RelayCommand SetAsReserved { get; set; }
        public RelayCommand SetAsCancelled { get; set; }
        public RelayCommand GotoChooseTreatmentWindow { get; set; }




        public AppointmentViewModel()
        {
            TimeSchedules = new ObservableCollection<string>();
            Schedules = new ObservableCollection<Schedule>();
            Patients = new ObservableCollection<Patient>();
            SelectedPatient = new Patient();
            SelectedSchedule = new Schedule();
            setup();
            PatientsView = CollectionViewSource.GetDefaultView(Patients);
            PatientsView.Filter = patientviewfilter;
            SaveAppointment = new RelayCommand(saveappointment, cansaveappointment);
            SelectDate = new RelayCommand(selectdate);
            Date = DateTime.Today;
            ScheduleDate = DateTime.Today;
            AddDay = new RelayCommand(addday);
            MinusDay = new RelayCommand(minusday);
            SetAsCancelled = new RelayCommand(setascancelled, cansetascancelled);
            SetAsReserved = new RelayCommand(setasreserved, cansetasreserved);
            GotoChooseTreatmentWindow = new RelayCommand(gotochoosetreatmentwindow);
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

        private void saveappointment()
        {
            MessageBoxResult dialogResult = MessageBox.Show("Are you sure to add this new Appointment?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dialogResult == MessageBoxResult.Yes)
            {
                var date = DateTime.Now;
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);

                MySqlConnection connection;
                String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
                connection = new MySqlConnection(connString);
                try
                {
                    connection.Open();
                    String query = "INSERT INTO `appointment` VALUES (null,@patient_id,@service_id,@date,@time,@status)";
                    MySqlCommand comm = new MySqlCommand(query, connection);
                    comm.Parameters.AddWithValue("@patient_id", SelectedPatient.Id);
                    comm.Parameters.AddWithValue("@service_id", SelectedService.Id);
                    comm.Parameters.AddWithValue("@date", Date.ToShortDateString());
                    comm.Parameters.AddWithValue("@time", SelectedTime);
                    comm.Parameters.AddWithValue("@status", "Reserved");
                    comm.ExecuteNonQuery();
                    comm.Parameters.Clear();

                    //query = "INSERT INTO `patienthistory` VALUES (null,@patient_id,@transaction,@datetimehappen,@account_id)";
                    //comm = new MySqlCommand(query, connection);
                    //string detail = "Sets an appointment: Date: " + Date.ToShortDateString() + " Time: " + SelectedTime;
                    //comm.Parameters.AddWithValue("@patient_id", SelectedPatient.Id);
                    //comm.Parameters.AddWithValue("@transaction", detail);
                    //comm.Parameters.AddWithValue("@datetimehappen", date);
                    //comm.Parameters.AddWithValue("@account_id", this.AccountId);
                    //comm.ExecuteNonQuery();

                    TransactionLog transactionLog = new TransactionLog();
                    transactionLog.Description = "Appointment";
                    transactionLog.Transactedin = date;
                    transactionLog.Details = "Added New Appointment for: " + SelectedPatient.Firstname + " " + SelectedPatient.Middlename + " " + SelectedPatient.Lastname;
                    transactionLog.AccountId = this.AccountId;


                    query = "INSERT INTO `transactionlog` VALUES (null,@description,@transactedin,@details,@account_id)";
                    comm = new MySqlCommand(query, connection);
                    comm.Parameters.AddWithValue("@description", transactionLog.Description);
                    comm.Parameters.AddWithValue("@transactedin", transactionLog.Transactedin);
                    comm.Parameters.AddWithValue("@details", transactionLog.Details);
                    comm.Parameters.AddWithValue("@account_id", transactionLog.AccountId);
                    comm.ExecuteNonQuery();

                    MessageBox.Show("New Appointment Added!");
                    SelectedPatient = null;
                    setup();
                    selectdate();
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

        private bool cansaveappointment()
        {
            return SelectedPatient != null && !String.IsNullOrWhiteSpace(Date.ToString()) && !String.IsNullOrWhiteSpace(SelectedTime) && SelectedService != null;
        }

        private void setup()
        {
            Patients.Clear();

            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "SELECT * FROM `patient` where `status` = 'Active'";
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
                settimesched();
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

        private void settimesched()
        {
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                TimeSchedules.Clear();
                TimeSchedules.Add("8:00AM - 9:00AM");
                TimeSchedules.Add("9:00AM - 10:00AM");
                TimeSchedules.Add("10:00AM - 11:00AM");
                TimeSchedules.Add("11:00AM - 12:00PM");
                TimeSchedules.Add("1:00PM - 2:00PM");
                TimeSchedules.Add("2:00PM - 3:00PM");
                TimeSchedules.Add("3:00PM - 4:00PM");
                TimeSchedules.Add("4:00PM - 5:00PM");
                TimeSchedules.Add("5:00PM - 6:00PM");
                TimeSchedules.Add("7:00PM - 8:00PM");
                TimeSchedules.Add("8:00PM - 9:00PM");

                String query = "SELECT `time` FROM `appointment` WHERE `status` = 'Reserved' AND `date` = @date";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@date", Date.ToShortDateString());
                MySqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    TimeSchedules.Remove(reader[0].ToString());
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

        private void selectdate()
        {
            Schedules.Clear();
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "SELECT `appointment`.`id`,`patient`.`id`,`patient`.`firstname`,`patient`.`middlename`,`patient`.`lastname`,`appointment`.`time`,`service`.`name`,`appointment`.`status` FROM `appointment` INNER JOIN `patient` ON `appointment`.`patient_id` = `patient`.`id` INNER JOIN `service` ON `appointment`.`service_id` = `service`.`id` WHERE `appointment`.`date` = @date";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@date", ScheduleDate.ToShortDateString());
                MySqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Schedule newSchedule = new Schedule();
                    newSchedule.Id = Convert.ToInt32(reader[0]);
                    newSchedule.PatientId = Convert.ToInt32(reader[1]);
                    newSchedule.PatientName = reader[2].ToString() + " " + reader[3].ToString() + " " + reader[4].ToString();
                    newSchedule.Time = reader[5].ToString();
                    newSchedule.Treatment = reader[6].ToString();
                    newSchedule.Status = reader[7].ToString();
                    Schedules.Add(newSchedule);
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

        private void minusday()
        {
            ScheduleDate = ScheduleDate.AddDays(-1);
        }

        private void addday()
        {
            ScheduleDate = ScheduleDate.AddDays(1);
        }

        private void setascancelled()
        {
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "UPDATE `appointment` SET `status` = @status WHERE `id` = @id";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@status", "Cancelled");
                comm.Parameters.AddWithValue("@id", SelectedSchedule.Id);
                comm.ExecuteNonQuery();

                TransactionLog transactionLog = new TransactionLog();
                transactionLog.Description = "Appointment";
                var date = DateTime.Now;
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
                transactionLog.Transactedin = date;
                transactionLog.Details = "Appointment cancelled for Patient ID: " + SelectedSchedule.PatientId + ".";
                transactionLog.AccountId = this.AccountId;

                query = "INSERT INTO `transactionlog` VALUES (null,@description,@transactedin,@details,@account_id)";
                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@description", transactionLog.Description);
                comm.Parameters.AddWithValue("@transactedin", transactionLog.Transactedin);
                comm.Parameters.AddWithValue("@details", transactionLog.Details);
                comm.Parameters.AddWithValue("@account_id", transactionLog.AccountId);
                comm.ExecuteNonQuery();

                MessageBox.Show("Appointment cancelled for Patient ID: " + SelectedSchedule.PatientId + ".");
                setup();
                selectdate();
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
        private bool cansetascancelled()
        {
            if (SelectedSchedule != null)
                return SelectedSchedule.Status == "Reserved";
            return false;
        }




        private void setasreserved()
        {
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "UPDATE `appointment` SET `status` = @status WHERE `id` = @id";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@status", "Reserved");
                comm.Parameters.AddWithValue("@id", SelectedSchedule.Id);
                comm.ExecuteNonQuery();

                TransactionLog transactionLog = new TransactionLog();
                transactionLog.Description = "Appointment";
                var date = DateTime.Now;
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
                transactionLog.Transactedin = date;
                transactionLog.Details = "Appointment reserved for Patient ID: " + SelectedSchedule.PatientId + ".";
                transactionLog.AccountId = this.AccountId;

                query = "INSERT INTO `transactionlog` VALUES (null,@description,@transactedin,@details,@account_id)";
                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@description", transactionLog.Description);
                comm.Parameters.AddWithValue("@transactedin", transactionLog.Transactedin);
                comm.Parameters.AddWithValue("@details", transactionLog.Details);
                comm.Parameters.AddWithValue("@account_id", transactionLog.AccountId);
                comm.ExecuteNonQuery();

                MessageBox.Show("Appointment reserved for Patient ID: " + SelectedSchedule.PatientId + ".");
                setup();
                selectdate();
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
        private bool cansetasreserved()
        {
            if (SelectedSchedule != null)
                return SelectedSchedule.Status == "Cancelled";
            return false;
        }

        private void gotochoosetreatmentwindow()
        {
            TreatmentToBeDoneWindowView view = new TreatmentToBeDoneWindowView();
            TreatmentTobeDoneWindowViewModel viewModel = new TreatmentTobeDoneWindowViewModel();
            view.DataContext = viewModel;
            viewModel.MyView = view;
            view.ShowDialog();
            if (viewModel.IsTreatmentSelected == true)
            {
                this.SelectedService = viewModel.SelectedService;
            }
        }




    }
}
