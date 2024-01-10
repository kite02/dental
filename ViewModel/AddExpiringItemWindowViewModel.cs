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
    public class AddExpiringItemWindowViewModel : ValidatableModel
    {

        public ObservableCollection<Supplier> Suppliers { get; set; }
        
        public InventoryViewModel InventoryViewModel { get; set; }
        private Supplier selectedsupplier;

        public Supplier SelectedSupplier
        {
            get { return selectedsupplier; }
            set
            {
                selectedsupplier = value;
                RaisePropertyChanged("SelectedSupplier");
            }
        }

        private ExpiringItem selectedexpiringitem;

        public ExpiringItem SelectedExpiringItem
        {
            get { return selectedexpiringitem; }
            set { selectedexpiringitem = value;
                RaisePropertyChanged("SelectedExpiringItem");
            }
        }



        public RelayCommand SaveExpiringItem { get; set; }
        public RelayCommand ClearText { get; set; }
        public RelayCommand EditExpiringItem { get; set; }
        
        
        public ICollectionView SupplierView { get; set; }

        private string searchsupplier;

        public string SearchSupplier
        {
            get { return searchsupplier; }
            set
            {
                searchsupplier = value;
                RaisePropertyChanged("SearchSupplier");
                SupplierView.Refresh();
            }
        }

        public int AccountId { get; set; }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value;
                RaisePropertyChanged("Name");
            }
        }

        private int months;

        public int Months
        {
            get { return months; }
            set { months = value;
                RaisePropertyChanged("Months");
            }
        }


        private int days;

        public int Days
        {
            get { return days; }
            set { days = value;
                RaisePropertyChanged("Days");
            }
        }

        private int years;

        public int Years
        {
            get { return years; }
            set { years = value;
                RaisePropertyChanged("Years");
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










        public AddExpiringItemWindowViewModel()
        {
            AccountId = 0;
            SelectedSupplier = new Supplier();
        
            Suppliers = new ObservableCollection<Supplier>();
        
            EditExpiringItem = new RelayCommand(editexpiringitem, caneditexpiringitem);
            setup();
            SupplierView = CollectionViewSource.GetDefaultView(Suppliers);
            SupplierView.Filter = supplierviewfilter;
            SaveExpiringItem = new RelayCommand(saveExpiringItem, checkAllFields);
            ClearText = new RelayCommand(cleartext);
        }

        public AddExpiringItemWindowViewModel(InventoryViewModel viewModel)
        {
            SelectedSupplier = new Supplier();
            Suppliers = new ObservableCollection<Supplier>();
            setup();
            SupplierView = CollectionViewSource.GetDefaultView(Suppliers);
            SupplierView.Filter = supplierviewfilter;
            SaveExpiringItem = new RelayCommand(saveExpiringItem, checkAllFields);
        }

        

        private bool supplierviewfilter(object item)
        {
            if (String.IsNullOrEmpty(SearchSupplier))
                return true;
            else
                return ((item as Supplier).Id.ToString().IndexOf(SearchSupplier, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as Supplier).Name.IndexOf(SearchSupplier, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as Supplier).SupplierRepresentative.IndexOf(SearchSupplier, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as Supplier).Address.IndexOf(SearchSupplier, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as Supplier).Contactnumber.IndexOf(SearchSupplier, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as Supplier).EmailAddress.IndexOf(SearchSupplier, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as Supplier).Status.IndexOf(SearchSupplier, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void setup()
        {
            Suppliers.Clear();
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try { 
            connection.Open();
            String query = "SELECT * FROM `supplier` WHERE status = 'Active'";
            MySqlCommand comm = new MySqlCommand(query, connection);
            MySqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Supplier newSupplier = new Supplier();
                newSupplier.Id = Convert.ToInt32(reader["id"]);
                newSupplier.Name = reader["name"].ToString();
                newSupplier.SupplierRepresentative = reader["representative"].ToString();
                newSupplier.Address = reader["address"].ToString();
                newSupplier.Contactnumber = reader["contactnumber"].ToString();
                newSupplier.EmailAddress = reader["emailaddress"].ToString();
                newSupplier.Status = reader["status"].ToString();
                Suppliers.Add(newSupplier);
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


        public void saveExpiringItem()
        {

            MessageBoxResult dialogResult = MessageBox.Show("Are you sure to add this new Product?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dialogResult == MessageBoxResult.Yes)
            {
                MySqlConnection connection;
                String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
                connection = new MySqlConnection(connString);
                try { 
                connection.Open();
                String query = "INSERT INTO `expiringitem` VALUES(null,@supplier_id,@name,@months,@days,@years,@expirationdate,@quantity,@status)";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@supplier_id", SelectedSupplier.Id);
                comm.Parameters.AddWithValue("@name", Name);
                comm.Parameters.AddWithValue("@months", Months);
                comm.Parameters.AddWithValue("@days",Days);
                comm.Parameters.AddWithValue("@years", Years);
                comm.Parameters.AddWithValue("@expirationdate", DateTime.Today.AddMonths(Months).AddDays(Days).AddYears(Years).ToShortDateString());
                comm.Parameters.AddWithValue("@quantity", Quantity);
                comm.Parameters.AddWithValue("@status", "Available");
                comm.ExecuteNonQuery();


                TransactionLog transactionLog = new TransactionLog();
                transactionLog.Description = "Inventory";
                var date = DateTime.Now;
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
                transactionLog.Transactedin = date;
                transactionLog.Details = "Added New Item: " + Name;
                transactionLog.AccountId = this.AccountId;


                query = "INSERT INTO `transactionlog` VALUES (null,@description,@transactedin,@details,@account_id)";
                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@description", transactionLog.Description);
                comm.Parameters.AddWithValue("@transactedin", transactionLog.Transactedin);
                comm.Parameters.AddWithValue("@details", transactionLog.Details);
                comm.Parameters.AddWithValue("@account_id", transactionLog.AccountId);
                comm.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message, "MySQL ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
                MessageBox.Show("New Item Added!");
                SelectedSupplier = new Supplier();
                Name = String.Empty;
                Months = 0;
                Days = 0;
                Years = 0;
                Quantity = 0;
            }
        }
        public bool checkAllFields()
        {
            return !String.IsNullOrWhiteSpace(Name) && !String.IsNullOrWhiteSpace(Quantity.ToString());
        }

        private void cleartext()
        {
            SelectedSupplier = new Supplier();
            Name = String.Empty;
            Months = 0;
            Days = 0;
            Years = 0;
        }

        private void editexpiringitem()
        {
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try { 
            connection.Open();
            String query = "UPDATE `expiringitem` SET `supplier_id` = @supplier_id, `name` = @name,`months` = @months, `days` = @days, `years` = @years,`quantity` = @quantity WHERE `id` = @id";
            MySqlCommand comm = new MySqlCommand(query, connection);
            comm.Parameters.AddWithValue("@supplier_id",SelectedSupplier.Id);
            comm.Parameters.AddWithValue("@name", Name);
            comm.Parameters.AddWithValue("@months",Months);
            comm.Parameters.AddWithValue("@days",Days);
            comm.Parameters.AddWithValue("@years",Years);
            comm.Parameters.AddWithValue("@quantity",Quantity);
            comm.Parameters.AddWithValue("@id", SelectedExpiringItem.Id);
            comm.ExecuteNonQuery();

            TransactionLog transactionLog = new TransactionLog();
            transactionLog.Description = "Inventory";
            var date = DateTime.Now;
            date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
            transactionLog.Transactedin = date;
            transactionLog.Details = "Expiring Item Id: " + SelectedExpiringItem.Id + " is edited";
            transactionLog.AccountId = this.AccountId;

            query = "INSERT INTO `transactionlog` VALUES (null,@description,@transactedin,@details,@account_id)";
            comm = new MySqlCommand(query, connection);
            comm.Parameters.AddWithValue("@description", transactionLog.Description);
            comm.Parameters.AddWithValue("@transactedin", transactionLog.Transactedin);
            comm.Parameters.AddWithValue("@details", transactionLog.Details);
            comm.Parameters.AddWithValue("@account_id", transactionLog.AccountId);
            comm.ExecuteNonQuery();

            MessageBox.Show("Expiring Item Id: " + SelectedExpiringItem.Id + " is edited");

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

        private bool caneditexpiringitem()
        {
            return SelectedExpiringItem != null;
        }

       
    }
}
