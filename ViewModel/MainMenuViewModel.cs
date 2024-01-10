using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using SmileLineDentalClinic.Model;
using SmileLineDentalClinic.Reports;
using SmileLineDentalClinic.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SmileLineDentalClinic.ViewModel
{
    public class MainMenuViewModel : ValidatableModel
    {
        private object selectedexpander;

        public object SelectedExpander
        {
            get { return selectedexpander; }
            set
            {
                selectedexpander = value;
                RaisePropertyChanged("SelectedExpander");
            }
        }


        public object display;
        public object Display
        {
            get
            {
                return display;
            }
            set
            {
                display = value;
                RaisePropertyChanged("Display");
            }
        }


        public int Id { get; set; }



        private string fullname;

        public String FullName
        {
            get { return fullname; }
            set
            {
                fullname = value;
                RaisePropertyChanged("FullName");
            }
        }




        private string currenttime;

        public string CurrentTime
        {
            get { return currenttime; }
            set
            {
                if (currenttime != value)
                {
                    currenttime = value;
                }
                RaisePropertyChanged("CurrentTime");
            }
        }


        private string currentdate;

        public string CurrentDate
        {
            get { return currentdate; }
            set
            {

                currentdate = value;

                RaisePropertyChanged("CurrentDate");
            }
        }

        public User User { get; set; }
        public AccountLog AccountLog { get; set; }


        public DashboardViewModel DashboardViewModel { get; set; }
        public SuppliersViewModel SuppliersViewModel { get; set; }
        public AccountViewModel AccountViewModel { get; set; }
        public ServicesViewModel ServicesViewModel { get; set; }
        public InventoryViewModel InventoryViewModel { get; set; }

        public POViewModel POViewModel { get; set; }
        public ReceiveOrderViewModel ReceiveOrderViewModel { get; set; }
        public PatientViewModel PatientViewModel { get; set; }
        public AppointmentViewModel AppointmentViewModel { get; set; }
        public BillingViewModel BillingViewModel { get; set; }
        public TreatmentViewModel TreatmentViewModel { get; set; }

        public SalesReportViewModel SalesReportViewModel { get; set; }
        public AccountLogViewModel AccountLogViewModel { get; set; }
        public TransactionLogViewModel TransactionLogViewModel { get; set; }


        public RelayCommand<Window> RecordLogOut { get; set; }
        public RelayCommand GotoDashboard { get; set; }
        public RelayCommand GotoSuppliers { get; set; }
        public RelayCommand GotoAccounts { get; set; }
        public RelayCommand GotoServices { get; set; }
        public RelayCommand GotoInventory { get; set; }
        public RelayCommand GotoStockIn { get; set; }
        public RelayCommand GotoPO { get; set; }
        public RelayCommand GotoReceiveOrder { get; set; }
        public RelayCommand GotoPatients { get; set; }
        public RelayCommand GotoAppointments { get; set; }
        public RelayCommand GotoBilling { get; set; }
        public RelayCommand GotoTreatment { get; set; }
        public RelayCommand GotoPointofSales { get; set; }
        public RelayCommand GotoSalesReport { get; set; }
        public RelayCommand GotoAccountLogs { get; set; }
        public RelayCommand GotoTransactionLogs { get; set; }
        public RelayCommand BackUpDatabase { get; set; }
        public RelayCommand RestoreDatabase { get; set; }
        public RelayCommand PrintInventoryReport { get; set; }

        private Boolean caniclickdashboard;
        private Boolean caniclicksupplier;
        private Boolean caniclickaccounts;
        private Boolean caniclickservices;
        private Boolean caniclickinventory;
        private Boolean caniclickstockin;
        private Boolean caniclickpo;
        private Boolean caniclickreceiveorder;
        private Boolean caniclickpatients;
        private Boolean caniclickappointments;
        private Boolean caniclickbilling;
        private Boolean caniclicktreatment;
        private Boolean caniclickpointofsales;
        private Boolean caniclicksalesreport;
        private Boolean caniclickaccountlogs;
        private Boolean caniclicktransactionlog;

        public Boolean CanIClickDashboard
        {
            get
            {
                return caniclickdashboard;
            }
            set
            {
                caniclickdashboard = value; RaisePropertyChanged("CanIClickDashboard");
            }
        }

        public Boolean CanIClickSupplier
        {
            get
            {
                return caniclicksupplier;
            }
            set
            {
                caniclicksupplier = value; RaisePropertyChanged("CanIClickSupplier");
            }
        }

        public Boolean CanIClickAccounts
        {
            get
            {
                return caniclickaccounts;
            }
            set
            {
                caniclickaccounts = value; RaisePropertyChanged("CanIClickAccounts");
            }
        }
        public Boolean CanIClickServices
        {
            get
            {
                return caniclickservices;
            }
            set
            {
                caniclickservices = value; RaisePropertyChanged("CanIClickServices");
            }
        }
        public Boolean CanIClickInventory
        {
            get
            {
                return caniclickinventory;
            }
            set
            {
                caniclickinventory = value; RaisePropertyChanged("CanIClickInventory");
            }
        }
        public Boolean CanIClickStockIn
        {
            get
            {
                return caniclickstockin;
            }
            set
            {
                caniclickstockin = value; RaisePropertyChanged("CanIClickStockIn");
            }
        }
        public Boolean CanIClickPO
        {
            get
            {
                return caniclickpo;
            }
            set
            {
                caniclickpo = value; RaisePropertyChanged("CanIClickPO");
            }
        }
        public Boolean CanIClickReceiveOrder
        {
            get
            {
                return caniclickreceiveorder;
            }
            set
            {
                caniclickreceiveorder = value;
                RaisePropertyChanged("CanIClickReceiveOrder");
            }
        }
        public Boolean CanIClickPatients
        {
            get
            {
                return caniclickpatients;
            }
            set
            {
                caniclickpatients = value; RaisePropertyChanged("CanIClickPatients");
            }
        }
        public Boolean CanIClickAppointments
        {
            get
            {
                return caniclickappointments;
            }
            set
            {
                caniclickappointments = value; RaisePropertyChanged("CanIClickAppointments");
            }
        }
        public Boolean CanIClickBilling
        {
            get
            {
                return caniclickbilling;
            }
            set
            {
                caniclickbilling = value; RaisePropertyChanged("CanIClickBilling");
            }
        }
        public Boolean CanIClickTreatment
        {
            get
            {
                return caniclicktreatment;
            }
            set
            {
                caniclicktreatment = value; RaisePropertyChanged("CanIClickTreatment");
            }
        }
        public Boolean CanIClickPointofSales
        {
            get
            {
                return caniclickpointofsales;
            }
            set
            {
                caniclickpointofsales = value; RaisePropertyChanged("CanIClickPointofSales");
            }
        }
        public Boolean CanIClickSalesReport
        {
            get
            {
                return caniclicksalesreport;
            }
            set
            {
                caniclicksalesreport = value; RaisePropertyChanged("CanIClickSalesReport");
            }
        }
        public Boolean CanIClickAccountLogs
        {
            get
            {
                return caniclickaccountlogs;
            }
            set
            {
                caniclickaccountlogs = value; RaisePropertyChanged("CanIClickAccountLogs");
            }
        }
        public Boolean CanIClickTransactionLog
        {
            get
            {
                return caniclicktransactionlog;
            }
            set
            {
                caniclicktransactionlog = value; RaisePropertyChanged("CanIClickTransactionLog");
            }
        }

        public MainMenuViewModel(int id)
        {
            User = new User();
            AccountLog = new AccountLog();



            RecordLogOut = new RelayCommand<Window>(param => recordlogout(param));
            GotoDashboard = new RelayCommand(gotodashboard);
            GotoSuppliers = new RelayCommand(gotosuppliers);
            GotoAccounts = new RelayCommand(gotoaccounts);
            GotoServices = new RelayCommand(gotoservices);
            GotoInventory = new RelayCommand(gotoinventory);
            PrintInventoryReport = new RelayCommand(printinventoryreport);

            GotoPO = new RelayCommand(gotopo);
            GotoReceiveOrder = new RelayCommand(gotoreceiveorder);
            GotoPatients = new RelayCommand(gotopatients);
            GotoAppointments = new RelayCommand(gotoappointments);
            GotoBilling = new RelayCommand(gotobilling);
            GotoTreatment = new RelayCommand(gototreatment);
            //GotoPointofSales = new RelayCommand(gotopointofsales);
            GotoSalesReport = new RelayCommand(gotosalesreport);
            GotoAccountLogs = new RelayCommand(gotoaccountlogs);
            GotoTransactionLogs = new RelayCommand(gototransactionlogs);
            BackUpDatabase = new RelayCommand(backupdatabase);
            RestoreDatabase = new RelayCommand(restoredatabase);

            Id = id;
            FullName = String.Empty;
            startTimer();
            setup();
            gotodashboard();
            //DashboardViewModel.GotoAppointments = this.GotoAppointments;
            //DashboardViewModel.GotoBilling = this.GotoBilling;
            //DashboardViewModel.GotoPO = this.GotoPO;
            //DashboardViewModel.GotoStockIn = this.GotoStockIn;
            //Display = DashboardViewModel;
            //CanIClickDashboard = false;
            //CanIClickSupplier = true;
            //CanIClickAccounts = true;
            //CanIClickServices = true;
            //CanIClickInventory = true;
            //CanIClickStockIn = true;
            //CanIClickPO = true;
            //CanIClickReceiveOrder = true;
            //CanIClickPatients = true;
            //CanIClickAppointments = true;
            //CanIClickBilling = true;
            //CanIClickTreatment = true;
            //CanIClickPointofSales = true;
            //CanIClickSalesReport = true;
            //CanIClickAccountLogs = true;
            //CanIClickTransactionLog = true;


        }




        private void CurrentTimeText(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToLongTimeString();
        }

        private void setup()
        {

            List<DateTime> DateList = new List<DateTime>();
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            connection.Open();
            String query = "SELECT * FROM `account` WHERE `id` = @id";
            MySqlCommand comm = new MySqlCommand(query, connection);
            comm.Parameters.AddWithValue("@id", Id);
            MySqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                User.Id = Convert.ToInt32(reader["id"]);
                User.Firstname = reader["firstname"].ToString();
                User.Middlename = reader["middlename"].ToString();
                User.Lastname = reader["lastname"].ToString();
                User.Username = reader["username"].ToString();
                User.Password = reader["password"].ToString();
                User.Type = reader["account_type"].ToString();
            }
            reader.Close();
            if (!String.IsNullOrWhiteSpace(User.Middlename))
                FullName = User.Firstname + " " + User.Middlename[0] + ". " + User.Lastname;
            else
            {
                FullName = User.Firstname + " " + User.Lastname;
            }


            query = "SELECT `datetopay` FROM `servicebilling`";
            comm = new MySqlCommand(query, connection);
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                DateList.Add(Convert.ToDateTime(reader[0]));
            }
            reader.Close();

            foreach (DateTime date in DateList)
            {

                if (date.Date < DateTime.Today.Date)
                {

                    query = "UPDATE `servicebilling` SET `datetopay` = @datetopay,`amounttopay` = (`cost`/2)/6 WHERE `datetopay` = @datetopay2 AND `amounttopay` = '0'";
                    comm = new MySqlCommand(query, connection);
                    comm.Parameters.AddWithValue("@datetopay", date.AddMonths(1).ToShortDateString());
                    comm.Parameters.AddWithValue("@datetopay2", date.ToShortDateString());
                    comm.ExecuteNonQuery();
                    comm.Parameters.Clear();
                }
            }





            CurrentDate = DateTime.Today.ToLongDateString();
            recordlogin();


        }

        private void startTimer()
        {

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += new EventHandler(CurrentTimeText);
            dispatcherTimer.Start();
        }

        private void recordlogin()
        {
            AccountLog.AccountId = User.Id;
            var date = DateTime.Now;
            date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
            AccountLog.DateTimeLoggedIn = date;
        }

        private void recordlogout(Window param)
        {
            var date = DateTime.Now;
            date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
            AccountLog.DateTimeLoggedOut = date;
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            connection.Open();
            String query = "INSERT `accountlog` VALUES (null,@account_id,@date_time_logged_in,@date_time_logged_out)";
            MySqlCommand comm = new MySqlCommand(query, connection);
            comm.Parameters.AddWithValue("@account_id", AccountLog.AccountId);
            comm.Parameters.AddWithValue("@date_time_logged_in", AccountLog.DateTimeLoggedIn);
            comm.Parameters.AddWithValue("@date_time_logged_out", AccountLog.DateTimeLoggedOut);
            comm.ExecuteNonQuery();
            if (param != null)
            {
                param.Close();
            }
        }


        private void gotodashboard()
        {
            DashboardViewModel = new DashboardViewModel();
            DashboardViewModel.FullName = FullName;
            DashboardViewModel.GotoAppointments = this.GotoAppointments;
            DashboardViewModel.GotoBilling = this.GotoBilling;
            DashboardViewModel.GotoPO = this.GotoPO;
            DashboardViewModel.GotoReceiveOrder = this.GotoReceiveOrder;
            Display = DashboardViewModel;
            CanIClickDashboard = false;
            CanIClickSupplier = true;
            CanIClickAccounts = true;
            CanIClickServices = true;
            CanIClickInventory = true;
            CanIClickStockIn = true;
            CanIClickPO = true;
            CanIClickReceiveOrder = true;
            CanIClickPatients = true;
            CanIClickAppointments = true;
            CanIClickBilling = true;
            CanIClickTreatment = true;
            CanIClickPointofSales = true;
            CanIClickSalesReport = true;
            CanIClickAccountLogs = true;
            CanIClickTransactionLog = true;
        }

        private void gotosuppliers()
        {
            SuppliersViewModel = new SuppliersViewModel();
            SuppliersViewModel.AccountId = Id;
            Display = SuppliersViewModel;
            CanIClickDashboard = true;
            CanIClickSupplier = false;
            CanIClickAccounts = true;
            CanIClickServices = true;
            CanIClickInventory = true;
            CanIClickStockIn = true;
            CanIClickPO = true;
            CanIClickReceiveOrder = true;
            CanIClickPatients = true;
            CanIClickAppointments = true;
            CanIClickBilling = true;
            CanIClickTreatment = true;
            CanIClickPointofSales = true;
            CanIClickSalesReport = true;
            CanIClickAccountLogs = true;
            CanIClickTransactionLog = true;
        }

        private void gotoaccounts()
        {
            AccountViewModel = new AccountViewModel();
            AccountViewModel.AccountId = Id;
            Display = AccountViewModel;
            CanIClickDashboard = true;
            CanIClickSupplier = true;
            CanIClickAccounts = false;
            CanIClickServices = true;
            CanIClickInventory = true;
            CanIClickStockIn = true;
            CanIClickPO = true;
            CanIClickReceiveOrder = true;
            CanIClickPatients = true;
            CanIClickAppointments = true;
            CanIClickBilling = true;
            CanIClickTreatment = true;
            CanIClickPointofSales = true;
            CanIClickSalesReport = true;
            CanIClickAccountLogs = true;
            CanIClickTransactionLog = true;
        }

        private void gotoservices()
        {
            ServicesViewModel = new ServicesViewModel();
            ServicesViewModel.AccountId = Id;
            Display = ServicesViewModel;
            CanIClickDashboard = true;
            CanIClickSupplier = true;
            CanIClickAccounts = true;
            CanIClickServices = false;
            CanIClickInventory = true;
            CanIClickStockIn = true;
            CanIClickPO = true;
            CanIClickReceiveOrder = true;
            CanIClickPatients = true;
            CanIClickAppointments = true;
            CanIClickBilling = true;
            CanIClickTreatment = true;
            CanIClickPointofSales = true;
            CanIClickSalesReport = true;
            CanIClickAccountLogs = true;
            CanIClickTransactionLog = true;
        }

        private void gotoinventory()
        {
            InventoryViewModel = new InventoryViewModel();
            InventoryViewModel.AccountId = Id;
            Display = InventoryViewModel;
            CanIClickDashboard = true;
            CanIClickSupplier = true;
            CanIClickAccounts = true;
            CanIClickServices = true;
            CanIClickInventory = false;
            CanIClickStockIn = true;
            CanIClickPO = true;
            CanIClickReceiveOrder = true;
            CanIClickPatients = true;
            CanIClickAppointments = true;
            CanIClickBilling = true;
            CanIClickTreatment = true;
            CanIClickPointofSales = true;
            CanIClickSalesReport = true;
            CanIClickAccountLogs = true;
            CanIClickTransactionLog = true;
        }
        private void printinventoryreport()
        {
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";

            connection = new MySqlConnection(connString);
            try
            {

                connection.Open();
                Inventory report = new Inventory();
                report.Load();
                CrystalReportView view = new CrystalReportView();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter();
                String query = "SELECT `id`,`name`,`expirationdate`,`quantity` FROM `expiringitemsview`";
                MySqlCommand comm = new MySqlCommand(query, connection);
                da.SelectCommand = comm;
                da.Fill(dt);
                report.OpenSubreport("Expiring Items").SetDataSource(dt);

                query = "SELECT `id`,`name`,`quantity` FROM `nonexpiringitemsview`";
                comm = new MySqlCommand(query, connection);
                da.SelectCommand = comm;
                dt.Clear();
                da.Fill(dt);
                report.OpenSubreport("Non-Expiring Items").SetDataSource(dt);

                query = "SELECT `firstname`,`middlename`,`lastname` FROM `account` WHERE `id` = @id";
                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    report.SetParameterValue("accountname", reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString());
                }
                reader.Close();

                view.Report.ViewerCore.ReportSource = report;
                view.Show();

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

        private void gotopo()
        {
            POViewModel = new POViewModel();
            POViewModel.AccountId = Id;
            Display = POViewModel;
            CanIClickDashboard = true;
            CanIClickSupplier = true;
            CanIClickAccounts = true;
            CanIClickServices = true;
            CanIClickInventory = true;
            CanIClickStockIn = true;
            CanIClickPO = false;
            CanIClickReceiveOrder = true;
            CanIClickPatients = true;
            CanIClickAppointments = true;
            CanIClickBilling = true;
            CanIClickTreatment = true;
            CanIClickPointofSales = true;
            CanIClickSalesReport = true;
            CanIClickAccountLogs = true;
            CanIClickTransactionLog = true;
        }

        private void gotoreceiveorder()
        {

            ReceiveOrderViewModel = new ReceiveOrderViewModel();
            ReceiveOrderViewModel.AccountId = Id;
            Display = ReceiveOrderViewModel;
            CanIClickDashboard = true;
            CanIClickSupplier = true;
            CanIClickAccounts = true;
            CanIClickServices = true;
            CanIClickInventory = true;
            CanIClickStockIn = true;
            CanIClickPO = true;
            CanIClickReceiveOrder = false;
            CanIClickPatients = true;
            CanIClickAppointments = true;
            CanIClickBilling = true;
            CanIClickTreatment = true;
            CanIClickPointofSales = true;
            CanIClickSalesReport = true;
            CanIClickAccountLogs = true;
            CanIClickTransactionLog = true;
        }

        private void gotopatients()
        {
            PatientViewModel = new PatientViewModel();
            PatientViewModel.AccountId = Id;
            Display = PatientViewModel;
            CanIClickDashboard = true;
            CanIClickSupplier = true;
            CanIClickAccounts = true;
            CanIClickServices = true;
            CanIClickInventory = true;
            CanIClickStockIn = true;
            CanIClickPO = true;
            CanIClickReceiveOrder = true;
            CanIClickPatients = false;
            CanIClickAppointments = true;
            CanIClickBilling = true;
            CanIClickTreatment = true;
            CanIClickPointofSales = true;
            CanIClickSalesReport = true;
            CanIClickAccountLogs = true;
            CanIClickTransactionLog = true;
        }

        private void gotoappointments()
        {
            AppointmentViewModel = new AppointmentViewModel();
            AppointmentViewModel.AccountId = Id;
            Display = AppointmentViewModel;
            CanIClickDashboard = true;
            CanIClickSupplier = true;
            CanIClickAccounts = true;
            CanIClickServices = true;
            CanIClickInventory = true;
            CanIClickStockIn = true;
            CanIClickPO = true;
            CanIClickReceiveOrder = true;
            CanIClickPatients = true;
            CanIClickAppointments = false;
            CanIClickBilling = true;
            CanIClickTreatment = true;
            CanIClickPointofSales = true;
            CanIClickSalesReport = true;
            CanIClickAccountLogs = true;
            CanIClickTransactionLog = true;
        }

        private void gotobilling()
        {

            BillingViewModel = new BillingViewModel();
            BillingViewModel.AccountId = Id;
            Display = BillingViewModel;
            CanIClickDashboard = true;
            CanIClickSupplier = true;
            CanIClickAccounts = true;
            CanIClickServices = true;
            CanIClickInventory = true;
            CanIClickStockIn = true;
            CanIClickPO = true;
            CanIClickReceiveOrder = true;
            CanIClickPatients = true;
            CanIClickAppointments = true;
            CanIClickBilling = false;
            CanIClickTreatment = true;
            CanIClickPointofSales = true;
            CanIClickSalesReport = true;
            CanIClickAccountLogs = true;
            CanIClickTransactionLog = true;
        }

        private void gototreatment()
        {
            TreatmentViewModel = new TreatmentViewModel();
            TreatmentViewModel.AccountId = Id;
            Display = TreatmentViewModel;
            CanIClickDashboard = true;
            CanIClickSupplier = true;
            CanIClickAccounts = true;
            CanIClickServices = true;
            CanIClickInventory = true;
            CanIClickStockIn = true;
            CanIClickPO = true;
            CanIClickReceiveOrder = true;
            CanIClickPatients = true;
            CanIClickTreatment = false;
            CanIClickAppointments = true;
            CanIClickBilling = true;
            CanIClickPointofSales = true;
            CanIClickSalesReport = true;
            CanIClickAccountLogs = true;
            CanIClickTransactionLog = true;
        }

        //private void gotopointofsales()
        //{
        //    ProductsViewModel = new ProductsViewModel();
        //    ProductsViewModel.AccountId = Id;
        //    Display = ProductsViewModel;
        //    CanIClickDashboard = true;
        //    CanIClickSupplier = true;
        //    CanIClickAccounts = true;
        //    CanIClickServices = true;
        //    CanIClickInventory = true;
        //    CanIClickStockIn = true;
        //    CanIClickPO = true;
        //    CanIClickReceiveOrder = true;
        //    CanIClickPatients = true;
        //    CanIClickAppointments = true;
        //    CanIClickBilling = true;
        //    CanIClickTreatment = true;
        //    CanIClickPointofSales = false;
        //    CanIClickSalesReport = true;
        //    CanIClickAccountLogs = true;
        //    CanIClickTransactionLog = true;
        //}



        private void gotosalesreport()
        {
            SalesReportViewModel = new SalesReportViewModel();
            SalesReportViewModel.AccountId = Id;
            Display = SalesReportViewModel;
            CanIClickDashboard = true;
            CanIClickSupplier = true;
            CanIClickAccounts = true;
            CanIClickServices = true;
            CanIClickInventory = true;
            CanIClickStockIn = true;
            CanIClickPO = true;
            CanIClickReceiveOrder = true;
            CanIClickPatients = true;
            CanIClickAppointments = true;
            CanIClickBilling = true;
            CanIClickTreatment = true;
            CanIClickPointofSales = true;
            CanIClickSalesReport = false;
            CanIClickAccountLogs = true;
            CanIClickTransactionLog = true;
        }

        private void gotoaccountlogs()
        {
            AccountLogViewModel = new AccountLogViewModel();
            Display = AccountLogViewModel;
            CanIClickDashboard = true;
            CanIClickSupplier = true;
            CanIClickAccounts = true;
            CanIClickServices = true;
            CanIClickInventory = true;
            CanIClickStockIn = true;
            CanIClickPO = true;
            CanIClickReceiveOrder = true;
            CanIClickPatients = true;
            CanIClickAppointments = true;
            CanIClickBilling = true;
            CanIClickTreatment = true;
            CanIClickPointofSales = true;
            CanIClickSalesReport = true;
            CanIClickAccountLogs = false;
            CanIClickTransactionLog = true;
        }

        private void gototransactionlogs()
        {
            TransactionLogViewModel = new TransactionLogViewModel();
            Display = TransactionLogViewModel;
            CanIClickDashboard = true;
            CanIClickSupplier = true;
            CanIClickAccounts = true;
            CanIClickServices = true;
            CanIClickInventory = true;
            CanIClickStockIn = true;
            CanIClickPO = true;
            CanIClickReceiveOrder = true;
            CanIClickPatients = true;
            CanIClickAppointments = true;
            CanIClickBilling = true;
            CanIClickTreatment = true;
            CanIClickPointofSales = true;
            CanIClickSalesReport = true;
            CanIClickAccountLogs = true;
            CanIClickTransactionLog = false;
        }

        private void backupdatabase()
        {
            string constring = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";

            // Important Additional Connection Options
            constring += "charset=utf8;convertzerodatetime=true;";

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            String file = null;

            if (saveFileDialog.ShowDialog() == true)
            {
                file = saveFileDialog.FileName;
            }

            if (!String.IsNullOrEmpty(file))
            {
                using (MySqlConnection conn = new MySqlConnection(constring))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Open();
                            mb.ExportToFile(file);
                            conn.Close();
                        }
                    }
                }
            }
        }

        private void restoredatabase()
        {
            string constring = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";

            // Important Additional Connection Options
            constring += "charset=utf8;convertzerodatetime=true;";

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            String file = null;

            if (saveFileDialog.ShowDialog() == true)
            {
                file = saveFileDialog.FileName;
            }

            if (!String.IsNullOrEmpty(file))
            {
                using (MySqlConnection conn = new MySqlConnection(constring))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Open();
                            mb.ImportFromFile(file);
                            conn.Close();
                        }
                    }
                }
            }
        }

    }
}
