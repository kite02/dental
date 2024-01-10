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
    public class AddNonExpiringItemWindowViewModel:ValidatableModel
    {

        public ObservableCollection<Supplier> Suppliers { get; set; }

        public InventoryViewModel InventoryViewModel { get; set; }

        public RelayCommand SaveNonExpiringItem { get; set; }
        public RelayCommand ClearText { get; set; }
        public RelayCommand EditNonExpiringItem { get; set; }

        
        public NonExpiringItem SelectedNonExpiringItem { get; set; }

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

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value;
                RaisePropertyChanged("Name");
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

        public AddNonExpiringItemWindowViewModel()
        {
            
            AccountId = 0;
            SelectedSupplier = new Supplier();
            Suppliers = new ObservableCollection<Supplier>();
            EditNonExpiringItem = new RelayCommand(editnonexpiringitem, caneditnonexpiringitem);
            setup();
            SupplierView = CollectionViewSource.GetDefaultView(Suppliers);
            SupplierView.Filter = supplierviewfilter;
            SaveNonExpiringItem = new RelayCommand(saveNonExpiringItem, checkAllFields);
            ClearText = new RelayCommand(cleartext);
        }

        public AddNonExpiringItemWindowViewModel(InventoryViewModel viewModel)
        {
            SelectedSupplier = new Supplier();
            Suppliers = new ObservableCollection<Supplier>();
            setup();
            SupplierView = CollectionViewSource.GetDefaultView(Suppliers);
            SupplierView.Filter = supplierviewfilter;
            SaveNonExpiringItem = new RelayCommand(saveNonExpiringItem, checkAllFields);
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


        public void saveNonExpiringItem()
        {

            MessageBoxResult dialogResult = MessageBox.Show("Are you sure to add this new Non-Expiring Item?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dialogResult == MessageBoxResult.Yes)
            {
                MySqlConnection connection;
                String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
                connection = new MySqlConnection(connString);
                connection.Open();
                String query = "INSERT INTO `nonexpiringitem` VALUES(null,@name,@quantity,@status,@supplier_id)";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@name", Name);
                comm.Parameters.AddWithValue("@quantity", Quantity);
                comm.Parameters.AddWithValue("@status", "Available");
                comm.Parameters.AddWithValue("@supplier_id", SelectedSupplier.Id);
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

                MessageBox.Show("New Non-Expiring Item Added!");
                SelectedSupplier = new Supplier();
                Name = String.Empty;
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
          
            Quantity = 0;
        }

        private void editnonexpiringitem()
        {
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            connection.Open();
            String query = "UPDATE `nonexpiringitem` SET `supplier_id` = @supplier_id, `name` = @name,`quantity` = @quantity WHERE `id` = @id";
            MySqlCommand comm = new MySqlCommand(query, connection);
            comm.Parameters.AddWithValue("@supplier_id", SelectedSupplier.Id);
            comm.Parameters.AddWithValue("@name",Name);
            comm.Parameters.AddWithValue("@quantity", Quantity);
            comm.Parameters.AddWithValue("@id", SelectedNonExpiringItem.Id);
            comm.ExecuteNonQuery();

            TransactionLog transactionLog = new TransactionLog();
            transactionLog.Description = "Inventory";
            var date = DateTime.Now;
            date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
            transactionLog.Transactedin = date;
            transactionLog.Details = "Non-Expiring Item Id: " + SelectedNonExpiringItem.Id + " is edited";
            transactionLog.AccountId = this.AccountId;

            query = "INSERT INTO `transactionlog` VALUES (null,@description,@transactedin,@details,@account_id)";
            comm = new MySqlCommand(query, connection);
            comm.Parameters.AddWithValue("@description", transactionLog.Description);
            comm.Parameters.AddWithValue("@transactedin", transactionLog.Transactedin);
            comm.Parameters.AddWithValue("@details", transactionLog.Details);
            comm.Parameters.AddWithValue("@account_id", transactionLog.AccountId);
            comm.ExecuteNonQuery();

            MessageBox.Show("Non-Expiring Item Id: " + SelectedNonExpiringItem.Id + " is edited");

        }

        private bool caneditnonexpiringitem()
        {
            return SelectedNonExpiringItem != null;
        }

    }
}
