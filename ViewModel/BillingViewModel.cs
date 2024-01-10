using GalaSoft.MvvmLight.CommandWpf;
using MySql.Data.MySqlClient;
using SmileLineDentalClinic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SmileLineDentalClinic.ViewModel
{
    public class BillingViewModel : ValidatableModel
    {
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

        private ServiceBilling selectedservicebilling;

        public ServiceBilling SelectedServiceBilling
        {
            get { return selectedservicebilling; }
            set
            {
                selectedservicebilling = value;
                RaisePropertyChanged("SelectedServiceBilling");
                if (SelectedServiceBilling != null)
                {
                    Id = SelectedServiceBilling.Id;
                    Name = SelectedServiceBilling.PatientName;
                    Service = SelectedServiceBilling.ServiceName;
                    DateToPay = SelectedServiceBilling.DateToPay;
                    Balance = SelectedServiceBilling.Balance;
                    AmountToPay = SelectedServiceBilling.AmountToPay;
                    TotalBalance = loadTotalBalance(SelectedServiceBilling.PatientId);
                    ExcessBalance = loadExcessBalance(SelectedServiceBilling.PatientId);
                    BillingType = "ServiceBilling";
                    SelectedPaymentTypeIndex = 0;
                    EnablePaymentType = true;

                }
            }
        }

        private string searchservicebilling;

        public string SearchServiceBilling
        {
            get { return searchservicebilling; }
            set
            {
                searchservicebilling = value;
                RaisePropertyChanged("SearchServiceBilling");
                ServiceBillingsView.Refresh();

            }
        }

        private string searchmedicinebilling;

        public string SearchMedicineBilling
        {
            get { return searchmedicinebilling; }
            set
            {
                searchmedicinebilling = value;
                RaisePropertyChanged("SearchMedicineBilling");
            }
        }




        private int selectedpaymenttypeindex;

        public int SelectedPaymentTypeIndex
        {
            get { return selectedpaymenttypeindex; }
            set
            {
                selectedpaymenttypeindex = value;
                RaisePropertyChanged("SelectedPaymentTypeIndex");
            }
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

        private bool enablepaymenttype;

        public bool EnablePaymentType
        {
            get { return enablepaymenttype; }
            set
            {
                enablepaymenttype = value;
                RaisePropertyChanged("EnablePaymentType");
            }
        }


        private string service;

        public string Service
        {
            get { return service; }
            set
            {
                service = value;
                RaisePropertyChanged("Service");
            }
        }

        private string datetopay;

        public string DateToPay
        {
            get { return datetopay; }
            set
            {
                datetopay = value;
                RaisePropertyChanged("DateToPay");
            }
        }

        private decimal balance;

        public decimal Balance
        {
            get { return balance; }
            set
            {
                balance = value;
                RaisePropertyChanged("Balance");
            }
        }

        private decimal amounttopay;

        public decimal AmountToPay
        {
            get { return amounttopay; }
            set
            {
                amounttopay = value;
                RaisePropertyChanged("AmountToPay");
            }
        }

        private decimal totalbalance;

        public decimal TotalBalance
        {
            get { return totalbalance; }
            set
            {
                totalbalance = value;
                RaisePropertyChanged("TotalBalance");
            }
        }

        private string billingtype;

        public string BillingType
        {
            get { return billingtype; }
            set
            {
                billingtype = value;
                RaisePropertyChanged("BillingType");
            }
        }

        private string selectedpaymenttype;

        public string SelectedPaymentType
        {
            get { return selectedpaymenttype; }
            set
            {
                selectedpaymenttype = value;
                RaisePropertyChanged("SelectedPaymentType");
            }
        }

        private decimal excessbalance;

        public decimal ExcessBalance
        {
            get { return excessbalance; }
            set
            {
                excessbalance = value;
                RaisePropertyChanged("ExcessBalance");
            }
        }


        private bool useexcess;

        public bool UseExcess
        {
            get { return useexcess; }
            set
            {
                useexcess = value;
                RaisePropertyChanged("UseExcess");
                if (UseExcess == true)
                {
                    AmountRendered = ExcessBalance;
                }
                else
                {
                    AmountRendered = 0;
                }
            }
        }


        private decimal amountrendered;
        [CustomValidation(typeof(BillingViewModel), "BillingViewValidate")]
        public decimal AmountRendered
        {
            get { return amountrendered; }
            set
            {
                amountrendered = value;
                RaisePropertyChanged("AmountRendered");
            }
        }

        public List<string> PaymentType { get; set; }
        public ICollectionView ServiceBillingsView { get; set; }
        public ObservableCollection<ServiceBilling> ServiceBillings { get; set; }
        public ICollectionView MedicineBillingView { get; set; }
        //public ObservableCollection<MedicineBilling> MedicineBillings { get; set; }
        public RelayCommand Done { get; set; }

        public BillingViewModel()
        {
            ExcessBalance = 0;
            UseExcess = false;
            PaymentType = new List<string>();
            ServiceBillings = new ObservableCollection<ServiceBilling>();
            //MedicineBillings = new ObservableCollection<MedicineBilling>();
            setup();
            Done = new RelayCommand(done, candone);
            ServiceBillingsView = CollectionViewSource.GetDefaultView(ServiceBillings);
            ServiceBillingsView.Filter = servicebillingsviewfilter;
            //MedicineBillingView = CollectionViewSource.GetDefaultView(MedicineBillings);
            //MedicineBillingView.Filter = medicinebillingviewfilter;
        }

        private bool servicebillingsviewfilter(object item)
        {
            if (String.IsNullOrEmpty(SearchServiceBilling))
                return true;
            else
                return ((item as ServiceBilling).Id.ToString().IndexOf(SearchServiceBilling, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as ServiceBilling).PatientId.ToString().IndexOf(SearchServiceBilling, StringComparison.OrdinalIgnoreCase) >= 0)
                    ||
                    ((item as ServiceBilling).Tooth.ToString().IndexOf(SearchServiceBilling, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as ServiceBilling).PatientName.IndexOf(SearchServiceBilling, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as ServiceBilling).ServiceName.ToString().IndexOf(SearchServiceBilling, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as ServiceBilling).Additional.ToString().IndexOf(SearchServiceBilling, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as ServiceBilling).Balance.ToString().IndexOf(SearchServiceBilling, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as ServiceBilling).DateToPay.IndexOf(SearchServiceBilling, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        //private bool medicinebillingviewfilter(object item)
        //{
        //    if (String.IsNullOrEmpty(SearchMedicineBilling))
        //        return true;
        //    else
        //        return ((item as MedicineBilling).Id.ToString().IndexOf(SearchMedicineBilling, StringComparison.OrdinalIgnoreCase) >= 0) ||
        //            ((item as MedicineBilling).PatientId.ToString().IndexOf(SearchMedicineBilling, StringComparison.OrdinalIgnoreCase) >= 0)
        //            || ((item as MedicineBilling).PatientName.IndexOf(SearchMedicineBilling, StringComparison.OrdinalIgnoreCase) >= 0)
        //             || ((item as MedicineBilling).StockName.ToString().IndexOf(SearchMedicineBilling, StringComparison.OrdinalIgnoreCase) >= 0)
        //             || ((item as MedicineBilling).Price.ToString().IndexOf(SearchMedicineBilling, StringComparison.OrdinalIgnoreCase) >= 0)
        //             || ((item as MedicineBilling).Quantity.ToString().IndexOf(SearchMedicineBilling, StringComparison.OrdinalIgnoreCase) >= 0)
        //             || ((item as MedicineBilling).Balance.ToString().IndexOf(SearchMedicineBilling, StringComparison.OrdinalIgnoreCase) >= 0)
        //             || ((item as MedicineBilling).DateToPay.IndexOf(SearchMedicineBilling, StringComparison.OrdinalIgnoreCase) >= 0);
        //}


        private void setup()
        {
            //MedicineBillings.Clear();
            ServiceBillings.Clear();
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();

                List<DateTime> DateList = new List<DateTime>();
                string query = "SELECT DISTINCT `datetopay` FROM `servicebilling`";
                MySqlCommand comm = new MySqlCommand(query, connection);
                MySqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    DateList.Add(Convert.ToDateTime(reader[0]));
                }
                reader.Close();

                foreach (DateTime date in DateList)
                {

                    if (date.Date <= DateTime.Today.Date)
                    {
                        query = "UPDATE `servicebilling` SET `datetopay` = @datetopay,`amounttopay` = (`cost`/2)/6 WHERE `datetopay` = @datetopay2 AND `amounttopay` = '0'";
                        comm = new MySqlCommand(query, connection);
                        comm.Parameters.AddWithValue("@datetopay", date.AddMonths(1).ToShortDateString());
                        comm.Parameters.AddWithValue("@datetopay2", date.ToShortDateString());
                        comm.ExecuteNonQuery();
                        comm.Parameters.Clear();
                    }
                }

                query = "SELECT `servicebilling`.`id`,`patient`.`id`,`patient`.`firstname`,`patient`.`middlename`,`patient`.`lastname`,`service`.`id`,`service`.`name`,`servicebilling`.`tooth`,`servicebilling`.`cost`,`servicebilling`.`additionalpay`,`servicebilling`.`amounttopay`,`servicebilling`.`balance`,`servicebilling`.`datetopay` FROM `servicebilling` INNER JOIN `patient` ON `servicebilling`.`patient_id` = `patient`.`id` INNER JOIN `service` ON `servicebilling`.`service_id` = `service`.`id` WHERE `servicebilling`.`balance` <> '0'";
                comm = new MySqlCommand(query, connection);
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    ServiceBilling newServiceBilling = new ServiceBilling();
                    newServiceBilling.Id = Convert.ToInt32(reader[0]);
                    newServiceBilling.PatientId = Convert.ToInt32(reader[1]);
                    newServiceBilling.PatientName = reader[2].ToString() + " " + reader[3].ToString() + " " + reader[4].ToString();
                    newServiceBilling.ServiceId = Convert.ToInt32(reader[5]);
                    newServiceBilling.ServiceName = reader[6].ToString();
                    newServiceBilling.Tooth = Convert.ToInt32(reader[7]);
                    newServiceBilling.Cost = Convert.ToDecimal(reader[8]);
                    newServiceBilling.Additional = Convert.ToDecimal(reader[9]);
                    newServiceBilling.AmountToPay = Convert.ToDecimal(reader[10]);
                    newServiceBilling.Balance = Convert.ToDecimal(reader[11]);
                    newServiceBilling.DateToPay = reader[12].ToString();
                    ServiceBillings.Add(newServiceBilling);
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




            PaymentType.Add("Installment");
            PaymentType.Add("Full");
        }

        private decimal loadTotalBalance(int Id)
        {
            decimal totalbalance;
            totalbalance = 0;
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "SELECT SUM(`balance`) FROM `servicebilling` WHERE `patient_id` = @patient_id";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@patient_id", Id);
                var servicebalance = comm.ExecuteScalar();
                if (servicebalance != DBNull.Value)
                {
                    totalbalance += Convert.ToDecimal(servicebalance);
                }

                //query = "SELECT SUM(`balance`) FROM `medicinebilling` WHERE `patient_id` = @patient_id";
                //comm = new MySqlCommand(query, connection);
                //comm.Parameters.AddWithValue("@patient_id", Id);
                //var medicinebalance = comm.ExecuteScalar();
                //if (medicinebalance != DBNull.Value)
                //{
                //    totalbalance += Convert.ToDecimal(medicinebalance);
                //}
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "MySQL ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }
            return totalbalance;
        }

        private decimal loadExcessBalance(int Id)
        {
            decimal excessbalance = 0;
            totalbalance = 0;
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "SELECT `excessfee` FROM `excessfee` WHERE `patient_id` = @patient_id";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@patient_id", Id);
                excessbalance = Convert.ToDecimal(comm.ExecuteScalar());

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "MySQL ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }
            return excessbalance;

        }


        private void done()
        {


            var date = DateTime.Now;
            date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = null;
                MySqlCommand comm = null;

                query = "UPDATE `servicebilling` SET `amounttopay` = `amounttopay`- @amounttopay, `balance` = `balance` - @balance WHERE `id` = @id ";
                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@balance", AmountRendered + ExcessBalance);
                comm.Parameters.AddWithValue("@amounttopay", AmountRendered + ExcessBalance);
                comm.Parameters.AddWithValue("@id", SelectedServiceBilling.Id);
                comm.ExecuteNonQuery();

                query = "UPDATE `excessfee` SET `excessfee` = @excessfee WHERE `patient_id` = @patient_id ";
                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@excessfee", AmountRendered - AmountToPay - ExcessBalance);
                comm.Parameters.AddWithValue("@patient_id", SelectedServiceBilling.PatientId);
                comm.ExecuteNonQuery();

                query = "UPDATE `excessfee` SET `excessfee` = '0' WHERE `excessfee` < '0'";
                comm = new MySqlCommand(query, connection);
                comm.ExecuteNonQuery();

                query = "UPDATE `servicebilling` SET `balance` = '0' WHERE `balance` < '0'";
                comm = new MySqlCommand(query, connection);
                comm.ExecuteNonQuery();

                query = "UPDATE `servicebilling` SET `amounttopay` = '0' WHERE `balance`  < '0'";
                comm = new MySqlCommand(query, connection);
                comm.ExecuteNonQuery();

                query = "UPDATE `servicebilling` SET `amounttopay` = '0' WHERE `amounttopay`  < '0'";
                comm = new MySqlCommand(query, connection);
                comm.ExecuteNonQuery();


                query = "INSERT INTO `billinghistory` VALUES (null,@transaction,@datetimehappen,@patient_id,@account_id)";
                comm = new MySqlCommand(query, connection);
                string detail = String.Empty;

                detail = "Paid the bill for : " + SelectedServiceBilling.ServiceName + " with an amount of : " + AmountRendered;
                comm.Parameters.AddWithValue("@patient_id", SelectedServiceBilling.PatientId);
                comm.Parameters.AddWithValue("@transaction", detail);
                comm.Parameters.AddWithValue("@datetimehappen", date);
                comm.Parameters.AddWithValue("@account_id", this.AccountId);
                comm.ExecuteNonQuery();

                query = "INSERT INTO `treatmentsalesreport` VALUES (null,@patientname,@name,@paid,@date)";
                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@patientname", SelectedServiceBilling.PatientName);
                comm.Parameters.AddWithValue("@name", SelectedServiceBilling.ServiceName);
                comm.Parameters.AddWithValue("@paid", AmountRendered + ExcessBalance);
                comm.Parameters.AddWithValue("@date", DateTime.Today.ToShortDateString());
                comm.ExecuteNonQuery();


                TransactionLog transactionLog = new TransactionLog();
                transactionLog.Description = "Billing";
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
                transactionLog.Transactedin = date;
                transactionLog.Details = "Patient Id: " + SelectedServiceBilling.PatientId + " paid the bill for the: " + SelectedServiceBilling.ServiceName;
                transactionLog.AccountId = this.AccountId;

                query = "INSERT INTO `transactionlog` VALUES (null,@description,@transactedin,@details,@account_id)";
                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@description", transactionLog.Description);
                comm.Parameters.AddWithValue("@transactedin", transactionLog.Transactedin);
                comm.Parameters.AddWithValue("@details", transactionLog.Details);
                comm.Parameters.AddWithValue("@account_id", transactionLog.AccountId);
                comm.ExecuteNonQuery();

                MessageBox.Show("Patient Id: " + SelectedServiceBilling.PatientId + " paid the bill for the: " + SelectedServiceBilling.ServiceName);
                setup();
            }

            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message+ex.StackTrace, "MySQL ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }
            UseExcess = false;
            SelectedServiceBilling = null;
            Id = 0;
            Name = String.Empty;
            Service = String.Empty;
            DateToPay = String.Empty;
            Balance = 0;
            AmountToPay = 0;
            TotalBalance = 0;
            ExcessBalance = 0;

        }

        private bool candone()
        {
            return this.HasErrors == false && (SelectedServiceBilling != null);
        }


        public static ValidationResult BillingViewValidate(object obj, ValidationContext context)
        {

            var billingviewmodel = (BillingViewModel)context.ObjectInstance;
            if (billingviewmodel.SelectedPaymentType == "Installment" && billingviewmodel.AmountRendered + billingviewmodel.ExcessBalance < billingviewmodel.AmountToPay)
            {
                return new ValidationResult("The Amount Rendered is less than the amount you must pay", new List<string> { "AmountRendered" });
            }



            if (billingviewmodel.SelectedPaymentType == "Full" && billingviewmodel.AmountRendered + billingviewmodel.ExcessBalance < billingviewmodel.Balance)
            {
                return new ValidationResult("The Amount Rendered must be exact the same or greater than your balance", new List<string> { "AmountRendered" });
            }

            return ValidationResult.Success;

        }
    }
}
