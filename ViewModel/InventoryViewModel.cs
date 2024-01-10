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

namespace SmileLineDentalClinic.ViewModel
{
    public class InventoryViewModel : ValidatableModel
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

        private int nearexpirycount;

        public int NearExpiryCount
        {
            get { return nearexpirycount; }
            set
            {
                nearexpirycount = value;
                RaisePropertyChanged("NearExpiryCount");
            }
        }

        private int expiredcount;

        public int ExpiredCount
        {
            get { return expiredcount; }
            set
            {
                expiredcount = value;
                RaisePropertyChanged("ExpiredCount");
            }
        }

        private int lowstockscount;

        public int LowStocksCount
        {
            get { return lowstockscount; }
            set
            {
                lowstockscount = value;
                RaisePropertyChanged("LowStocksCount");
            }
        }

        private int lowstocksnonexpiringitem;

        public int LowStocksCountNonExpiringItem
        {
            get { return lowstocksnonexpiringitem; }
            set { lowstocksnonexpiringitem = value;
                RaisePropertyChanged("LowStocksCountNonExpiringItem");
            }
        }





        public ObservableCollection<ExpiringItem> ExpiringItems { get; set; }
        public ObservableCollection<DisposedExpiringItem> DisposedExpiringItems { get; set; }
        public ObservableCollection<NonExpiringItem> NonExpiringItems { get; set; }
        public ObservableCollection<DisposedNonExpiringItem> DisposedNonExpiringItems { get; set; }
        public ObservableCollection<UsedItem> UsedItems { get; set; }

        private ExpiringItem selectedexpiringitem;

        public ExpiringItem SelectedExpiringItem
        {
            get { return selectedexpiringitem; }
            set
            {
                selectedexpiringitem = value;
                RaisePropertyChanged("SelectedExpiringItem");
            }
        }

        private NonExpiringItem selectednonexpiringitem;

        public NonExpiringItem SelectedNonExpiringItem
        {
            get { return selectednonexpiringitem; }
            set
            {
                selectednonexpiringitem = value;
                RaisePropertyChanged("SelectedNonExpiringItem");
            }
        }

        private DisposedExpiringItem selecteddisposedexpiringitem;

        public DisposedExpiringItem SelectedDisposedExpiringItem
        {
            get { return selecteddisposedexpiringitem; }
            set
            {
                selecteddisposedexpiringitem = value;
                RaisePropertyChanged("SelectedDisposedExpiringItem");
            }
        }

        private DisposedNonExpiringItem selecteddisposednonexpiringitem;

        public DisposedNonExpiringItem SelectedDisposedNonExpiringItem
        {
            get { return selecteddisposednonexpiringitem; }
            set
            {
                selecteddisposednonexpiringitem = value;
                RaisePropertyChanged("SelectedDisposedNonExpiringItem");
            }
        }


        private string searchdisposedexpiringitem;

        public string SearchDisposedExpiringItem
        {
            get { return searchdisposedexpiringitem; }
            set
            {
                searchdisposedexpiringitem = value;
                RaisePropertyChanged("SearchDisposedExpiringItem");
                DisposedExpiringItemsView.Refresh();
            }
        }


        private string searchexpiringitem;

        public string SearchExpiringItem
        {
            get { return searchexpiringitem; }
            set
            {
                searchexpiringitem = value;
                RaisePropertyChanged("SearchExpiringItem");
                ExpiringItemsView.Refresh();
            }
        }


        private string searchdisposednonexpiringitem;

        public string SearchDisposedNonExpiringItem
        {
            get { return searchdisposednonexpiringitem; }
            set
            {
                searchdisposednonexpiringitem = value;
                RaisePropertyChanged("SearchDisposedNonExpiringItem");
                DisposedNonExpiringItemsView.Refresh();
            }
        }


        private string searchnonexpiringitem;

        public string SearchNonExpiringItem
        {
            get { return searchnonexpiringitem; }
            set
            {
                searchnonexpiringitem = value;
                RaisePropertyChanged("SearchNonExpiringItem");
                NonExpiringItemsView.Refresh();
            }
        }

        private string searchuseditem;

        public string SearchUsedItem
        {
            get { return searchuseditem; }
            set { searchuseditem = value;
                RaisePropertyChanged("SearchUsedItem");
                UsedItemsView.Refresh();
            }
        }





        public ICollectionView ExpiringItemsView { get; set; }
        public ICollectionView DisposedExpiringItemsView { get; set; }
        public ICollectionView NonExpiringItemsView { get; set; }
        public ICollectionView DisposedNonExpiringItemsView { get; set; }
        public ICollectionView UsedItemsView { get; set; }


        public RelayCommand AddNewExpiringItem { get; set; }
        public RelayCommand AddNewNonExpiringItem { get; set; }
        public RelayCommand EditMode { get; set; }
        public RelayCommand DisposeItem { get; set; }
        public RelayCommand EditMode2 { get; set; }
        public RelayCommand DisposeItem2 { get; set; }
        public RelayCommand CancelDispose { get; set; }
        public RelayCommand CancelDispose2 { get; set; }

        public InventoryViewModel()
        {
            AddNewExpiringItem = new RelayCommand(addnewexpiringitem);
            AddNewNonExpiringItem = new RelayCommand(addnewnonexpiringitem);
            EditMode = new RelayCommand(editmode);
            DisposeItem = new RelayCommand(disposeitem, candisposeitem);
            EditMode2 = new RelayCommand(editmode2);
            DisposeItem2 = new RelayCommand(disposeitem2, candisposeitem2);
            CancelDispose = new RelayCommand(canceldispose);
            CancelDispose2 = new RelayCommand(canceldispose2);
            ExpiringItems = new ObservableCollection<ExpiringItem>();
            DisposedExpiringItems = new ObservableCollection<DisposedExpiringItem>();
            NonExpiringItems = new ObservableCollection<NonExpiringItem>();
            DisposedNonExpiringItems = new ObservableCollection<DisposedNonExpiringItem>();
            UsedItems = new ObservableCollection<UsedItem>();
            setup();
            ExpiringItemsView = CollectionViewSource.GetDefaultView(ExpiringItems);
            ExpiringItemsView.Filter = expiringitemsviewfilter;
            DisposedExpiringItemsView = CollectionViewSource.GetDefaultView(DisposedExpiringItems);
            DisposedExpiringItemsView.Filter = disposedexpiringitemsviewfilter;
            NonExpiringItemsView = CollectionViewSource.GetDefaultView(NonExpiringItems);
            NonExpiringItemsView.Filter = nonexpiringitemsviewfilter;
            DisposedNonExpiringItemsView = CollectionViewSource.GetDefaultView(DisposedNonExpiringItems);
            DisposedNonExpiringItemsView.Filter = disposednonexpiringitemsviewfilter;
            UsedItemsView = CollectionViewSource.GetDefaultView(UsedItems);
            UsedItemsView.Filter = useditemsviewfilter;
        }

        #region bool methods

        private bool candisposeitem()
        {
            if (SelectedExpiringItem != null)
                return SelectedExpiringItem.Status == "Available";
            return false;
        }

        private bool candisposeitem2()
        {
            if (SelectedNonExpiringItem != null)
                return SelectedNonExpiringItem.Status == "Available";
            return false;
        }

        private bool useditemsviewfilter(object item)
        {
            if (String.IsNullOrEmpty(SearchUsedItem))
                return true;
            else
                return ((item as UsedItem).Name.ToString().IndexOf(SearchUsedItem, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as UsedItem).Quantity.ToString().IndexOf(SearchUsedItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as UsedItem).DateUsed.IndexOf(SearchUsedItem, StringComparison.OrdinalIgnoreCase) >= 0);
        }


        private bool disposedexpiringitemsviewfilter(object item)
        {
            if (String.IsNullOrEmpty(SearchDisposedExpiringItem))
                return true;
            else
                return ((item as DisposedExpiringItem).StockId.ToString().IndexOf(SearchDisposedExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as DisposedExpiringItem).Name.IndexOf(SearchDisposedExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as DisposedExpiringItem).ExpiryDate.IndexOf(SearchDisposedExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as DisposedExpiringItem).Quantity.ToString().IndexOf(SearchDisposedExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as DisposedExpiringItem).DateDisposed.IndexOf(SearchDisposedExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private bool expiringitemsviewfilter(object item)
        {
            if (String.IsNullOrEmpty(SearchExpiringItem))
                return true;
            else
                return ((item as ExpiringItem).Id.ToString().IndexOf(SearchExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as ExpiringItem).Name.IndexOf(SearchExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as ExpiringItem).SupplierName.IndexOf(SearchExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as ExpiringItem).Name.IndexOf(SearchExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as ExpiringItem).ExpirationDate.IndexOf(SearchExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as ExpiringItem).Quantity.ToString().IndexOf(SearchExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as ExpiringItem).Status.IndexOf(SearchExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private bool disposednonexpiringitemsviewfilter(object item)
        {
            if (String.IsNullOrEmpty(SearchDisposedNonExpiringItem))
                return true;
            else
                return ((item as DisposedNonExpiringItem).StockId.ToString().IndexOf(SearchDisposedNonExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as DisposedNonExpiringItem).Name.IndexOf(SearchDisposedNonExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as DisposedNonExpiringItem).Quantity.ToString().IndexOf(SearchDisposedNonExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as DisposedNonExpiringItem).DateDisposed.IndexOf(SearchDisposedNonExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private bool nonexpiringitemsviewfilter(object item)
        {
            if (String.IsNullOrEmpty(SearchNonExpiringItem))
                return true;
            else
                return ((item as NonExpiringItem).Id.ToString().IndexOf(SearchNonExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as NonExpiringItem).Name.IndexOf(SearchNonExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as NonExpiringItem).SupplierName.IndexOf(SearchNonExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as NonExpiringItem).Quantity.ToString().IndexOf(SearchNonExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as NonExpiringItem).Status.IndexOf(SearchNonExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0);
        }


        #endregion

        #region void methods
        private void addnewexpiringitem()
        {
            AddExpiringItemWindowView view = new AddExpiringItemWindowView();
            AddExpiringItemWindowViewModel viewmodel = new AddExpiringItemWindowViewModel();
            viewmodel.AccountId = this.AccountId;
            view.DataContext = viewmodel;
            view.ShowDialog();
        }

        private void addnewnonexpiringitem()
        {
            AddNonExpiringItemWindowView view = new AddNonExpiringItemWindowView();
            AddNonExpiringItemWindowViewModel viewmodel = new AddNonExpiringItemWindowViewModel();
            viewmodel.AccountId = this.AccountId;
            view.DataContext = viewmodel;
            view.ShowDialog();
        }

        private void editmode()
        {
            AddExpiringItemWindowView view = new AddExpiringItemWindowView();
            AddExpiringItemWindowViewModel viewModel = new AddExpiringItemWindowViewModel();
            viewModel.AccountId = this.AccountId;
            viewModel.SelectedExpiringItem = SelectedExpiringItem;
            viewModel.SelectedSupplier.Id = SelectedExpiringItem.SupplierId;
            viewModel.SelectedSupplier.Name = SelectedExpiringItem.SupplierName;
            viewModel.Name = SelectedExpiringItem.Name;
            viewModel.Months = SelectedExpiringItem.Months;
            viewModel.Days = SelectedExpiringItem.Days;
            viewModel.Years = SelectedExpiringItem.Years;
            viewModel.Quantity = SelectedExpiringItem.Quantity;
            view.DataContext = viewModel;
            view.ShowDialog();
            setup();
        }

        private void disposeitem()
        {
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "UPDATE `expiringitem` SET `status` = @status WHERE `id` = @id";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@status", "Unavailable");
                comm.Parameters.AddWithValue("@id", SelectedExpiringItem.Id);
                comm.ExecuteNonQuery();

                query = "INSERT INTO `disposedexpiringitem` VALUES (null,@expiringitem_id,@quantitydisposed,@datedisposed,@status)";
                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@expiringitem_id", SelectedExpiringItem.Id);
                comm.Parameters.AddWithValue("@quantitydisposed", SelectedExpiringItem.Quantity);
                comm.Parameters.AddWithValue("@datedisposed", DateTime.Today.ToShortDateString());
                comm.Parameters.AddWithValue("@status", "Active");
                comm.ExecuteNonQuery();

                TransactionLog transactionLog = new TransactionLog();
                transactionLog.Description = "Inventory";
                var date = DateTime.Now;
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
                transactionLog.Transactedin = date;
                transactionLog.Details = "Expiring Item No.:" + SelectedExpiringItem.Id + " has been disposed";
                transactionLog.AccountId = this.AccountId;

                query = "INSERT INTO `transactionlog` VALUES (null,@description,@transactedin,@details,@account_id)";
                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@description", transactionLog.Description);
                comm.Parameters.AddWithValue("@transactedin", transactionLog.Transactedin);
                comm.Parameters.AddWithValue("@details", transactionLog.Details);
                comm.Parameters.AddWithValue("@account_id", transactionLog.AccountId);
                comm.ExecuteNonQuery();

                MessageBox.Show("Expiring Item No.:" + SelectedExpiringItem.Id + " has been disposed");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "MySQL ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }
            setup();

        }

        private void editmode2()
        {
            AddNonExpiringItemWindowView view = new AddNonExpiringItemWindowView();
            AddNonExpiringItemWindowViewModel viewModel = new AddNonExpiringItemWindowViewModel();
            viewModel.AccountId = this.AccountId;
            viewModel.SelectedNonExpiringItem = SelectedNonExpiringItem;
            viewModel.SelectedSupplier.Id = SelectedNonExpiringItem.SupplierId;
            viewModel.SelectedSupplier.Name = SelectedNonExpiringItem.SupplierName;
            viewModel.Name = SelectedNonExpiringItem.Name;
            viewModel.Quantity = SelectedNonExpiringItem.Quantity;
            view.DataContext = viewModel;
            view.ShowDialog();
            setup();
        }

        private void disposeitem2()
        {
            DisposeConsumablesWindowView view = new DisposeConsumablesWindowView();
            DisposeConsumablesWindowViewModel viewModel = new DisposeConsumablesWindowViewModel();
            view.DataContext = viewModel;
            viewModel.ExistedQuantity = SelectedNonExpiringItem.Quantity;
            viewModel.MyWindow = view;
            view.ShowDialog();
            if (viewModel.IsAccepted == true)
            {
                MySqlConnection connection;
                String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
                connection = new MySqlConnection(connString);
                try
                {
                    connection.Open();
                    String query = "UPDATE `nonexpiringitem` SET `quantity` = `quantity` - @quantity WHERE `id` = @id";
                    MySqlCommand comm = new MySqlCommand(query, connection);
                    comm.Parameters.AddWithValue("@quantity", viewModel.Quantity);
                    comm.Parameters.AddWithValue("@id", SelectedNonExpiringItem.Id);
                    comm.ExecuteNonQuery();

                    query = "INSERT INTO `disposednonexpiringitem` VALUES (null,@nonexpiringitem_id,@quantitydisposed,@datedisposed,@status)";
                    comm = new MySqlCommand(query, connection);
                    comm.Parameters.AddWithValue("@nonexpiringitem_id", SelectedNonExpiringItem.Id);
                    comm.Parameters.AddWithValue("@quantitydisposed", viewModel.Quantity);
                    comm.Parameters.AddWithValue("@datedisposed", DateTime.Today.ToShortDateString());
                    comm.Parameters.AddWithValue("@status", "Active");
                    comm.ExecuteNonQuery();

                    TransactionLog transactionLog = new TransactionLog();
                    transactionLog.Description = "Inventory";
                    var date = DateTime.Now;
                    date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
                    transactionLog.Transactedin = date;
                    transactionLog.Details = "Non-Expiring Item No.:" + SelectedNonExpiringItem.Id + " has been disposed";
                    transactionLog.AccountId = this.AccountId;

                    query = "INSERT INTO `transactionlog` VALUES (null,@description,@transactedin,@details,@account_id)";
                    comm = new MySqlCommand(query, connection);
                    comm.Parameters.AddWithValue("@description", transactionLog.Description);
                    comm.Parameters.AddWithValue("@transactedin", transactionLog.Transactedin);
                    comm.Parameters.AddWithValue("@details", transactionLog.Details);
                    comm.Parameters.AddWithValue("@account_id", transactionLog.AccountId);
                    comm.ExecuteNonQuery();

                    MessageBox.Show("Non-Expiring Item No.:" + SelectedNonExpiringItem.Id + " has been disposed");
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message, "MySQL ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
                setup();
            }
        }


        private void canceldispose()
        {
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "UPDATE `expiringitem` SET `status` = @status WHERE `id` = @id";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@status", "Available");
                comm.Parameters.AddWithValue("@id", SelectedDisposedExpiringItem.StockId);
                comm.ExecuteNonQuery();
                comm.Parameters.Clear();

                query = "UPDATE `disposedexpiringitem` SET `status` = @status WHERE `id` = @id";
                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@id", SelectedDisposedExpiringItem.Id);
                comm.Parameters.AddWithValue("@status", "Inactive");
                comm.ExecuteNonQuery();
                comm.Parameters.Clear();

                TransactionLog transactionLog = new TransactionLog();
                transactionLog.Description = "Inventory";
                var date = DateTime.Now;
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
                transactionLog.Transactedin = date;
                transactionLog.Details = "Expiring Item No.:" + SelectedDisposedExpiringItem.StockId + "disposal is cancelled ";
                transactionLog.AccountId = this.AccountId;

                query = "INSERT INTO `transactionlog` VALUES (null,@description,@transactedin,@details,@account_id)";
                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@description", transactionLog.Description);
                comm.Parameters.AddWithValue("@transactedin", transactionLog.Transactedin);
                comm.Parameters.AddWithValue("@details", transactionLog.Details);
                comm.Parameters.AddWithValue("@account_id", transactionLog.AccountId);
                comm.ExecuteNonQuery();
                comm.Parameters.Clear();
                MessageBox.Show("Expiring Item No.:" + SelectedDisposedExpiringItem.StockId + "disposal is cancelled");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "MySQL ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }
            setup();
        }

        private void canceldispose2()
        {
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "UPDATE `nonexpiringitem` SET `quantity`= `quantity` + @quantity WHERE `id` = @id";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@quantity", SelectedDisposedNonExpiringItem.Quantity);
                comm.Parameters.AddWithValue("@id", SelectedDisposedNonExpiringItem.StockId);
                comm.ExecuteNonQuery();
                comm.Parameters.Clear();

                query = "UPDATE `disposednonexpiringitem` SET `status` = @status WHERE `id` = @id";
                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@id", SelectedDisposedNonExpiringItem.Id);
                comm.Parameters.AddWithValue("@status", "Inactive");
                comm.ExecuteNonQuery();
                comm.Parameters.Clear();

                TransactionLog transactionLog = new TransactionLog();
                transactionLog.Description = "Inventory";
                var date = DateTime.Now;
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
                transactionLog.Transactedin = date;
                transactionLog.Details = "Non-Expiring Item No.:" + SelectedDisposedNonExpiringItem.StockId + "disposal is cancelled ";
                transactionLog.AccountId = this.AccountId;

                query = "INSERT INTO `transactionlog` VALUES (null,@description,@transactedin,@details,@account_id)";
                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@description", transactionLog.Description);
                comm.Parameters.AddWithValue("@transactedin", transactionLog.Transactedin);
                comm.Parameters.AddWithValue("@details", transactionLog.Details);
                comm.Parameters.AddWithValue("@account_id", transactionLog.AccountId);
                comm.ExecuteNonQuery();
                comm.Parameters.Clear();
                MessageBox.Show("Non-Expiring Item No.:" + SelectedDisposedNonExpiringItem.StockId + "disposal is cancelled");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "MySQL ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }
            setup();
        }


        private void setup()
        {
            
            NonExpiringItems.Clear();
            DisposedNonExpiringItems.Clear();
            DisposedExpiringItems.Clear();
            ExpiringItems.Clear();
            NearExpiryCount = 0;
            ExpiredCount = 0;
            LowStocksCount = 0;
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "SELECT `expiringitem`.`id`,`supplier`.`id`,`supplier`.`name`,`expiringitem`.`name`,`expiringitem`.`months`,`expiringitem`.`days`,`expiringitem`.`years`,`expiringitem`.`expirationdate`,`expiringitem`.`quantity`,`expiringitem`.`status` FROM `expiringitem` INNER JOIN `supplier` ON `expiringitem`.`supplier_id`=`supplier`.`id`";
                MySqlCommand comm = new MySqlCommand(query, connection);
                MySqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    ExpiringItem newExpiringItem = new ExpiringItem();
                    newExpiringItem.Id = Convert.ToInt32(reader[0]);
                    newExpiringItem.SupplierId = Convert.ToInt32(reader[1]);
                    newExpiringItem.SupplierName = reader[2].ToString();
                    newExpiringItem.Name = reader[3].ToString();
                    newExpiringItem.Months = Convert.ToInt32(reader[4]);
                    newExpiringItem.Days = Convert.ToInt32(reader[5]);
                    newExpiringItem.Years = Convert.ToInt32(reader[6]);
                    newExpiringItem.ExpirationDate = reader[7].ToString();
                    newExpiringItem.Quantity = Convert.ToInt32(reader[8]);
                    newExpiringItem.Status = reader[9].ToString();
                    if (newExpiringItem.Status == "Unavailable")
                    {
                        newExpiringItem.Color = "White";
                    }
                    else if ((DateTime.Today.Year == Convert.ToDateTime(newExpiringItem.ExpirationDate).Year) && ((DateTime.Today.Month == Convert.ToDateTime(newExpiringItem.ExpirationDate).Month)) && (Convert.ToDateTime(newExpiringItem.ExpirationDate).Day - DateTime.Today.Day <= 0) && (newExpiringItem.Status == "Available"))
                    {
                        newExpiringItem.Color = "Red";
                        ExpiredCount += 1;
                    }
                    else if ((DateTime.Today.Year == Convert.ToDateTime(newExpiringItem.ExpirationDate).Year) && (DateTime.Today.Month == Convert.ToDateTime(newExpiringItem.ExpirationDate).Month) && (Convert.ToDateTime(newExpiringItem.ExpirationDate).Day - DateTime.Today.Day > 0) && (Convert.ToDateTime(newExpiringItem.ExpirationDate).Day - DateTime.Today.Day <= 10) && (newExpiringItem.Status == "Available"))
                    {
                        newExpiringItem.Color = "Yellow";
                        NearExpiryCount += 1;
                    }

                    else if (newExpiringItem.Quantity > 0 && newExpiringItem.Quantity < 10 && newExpiringItem.Status == "Available")
                    {
                        newExpiringItem.Color = "Cyan";
                        LowStocksCount += 1;
                    }
                    ExpiringItems.Add(newExpiringItem);
                }
                reader.Close();

                query = "SELECT `disposedexpiringitem`.`id`,`expiringitem`.`id`,`expiringitem`.`name`,`expiringitem`.`expirationdate`,`disposedexpiringitem`.`quantitydisposed`,`disposedexpiringitem`.`datedisposed` FROM `expiringitem` INNER JOIN `disposedexpiringitem` ON `expiringitem`.`id` = `disposedexpiringitem`.`expiringitem_id` WHERE `disposedexpiringitem`.`status` = 'Active'"; 
                comm = new MySqlCommand(query, connection);
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    DisposedExpiringItem newDisposedExpiringItem = new DisposedExpiringItem();
                    newDisposedExpiringItem.Id = Convert.ToInt32(reader[0]);
                    newDisposedExpiringItem.StockId = Convert.ToInt32(reader[1]);
                    newDisposedExpiringItem.Name = reader[2].ToString();
                    newDisposedExpiringItem.ExpiryDate = reader[3].ToString();
                    newDisposedExpiringItem.Quantity = Convert.ToInt32(reader[4]);
                    newDisposedExpiringItem.DateDisposed = reader[5].ToString();
                    DisposedExpiringItems.Add(newDisposedExpiringItem);
                }

                reader.Close();


                query = "SELECT `nonexpiringitem`.`id`,`supplier`.`id`,`supplier`.`name`,`nonexpiringitem`.`name`,`nonexpiringitem`.`quantity`,`nonexpiringitem`.`status` FROM `nonexpiringitem` INNER JOIN `supplier` ON `nonexpiringitem`.`supplier_id`=`supplier`.`id`";
                comm = new MySqlCommand(query, connection);
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    NonExpiringItem newNonExpiringItem = new NonExpiringItem();
                    newNonExpiringItem.Id = Convert.ToInt32(reader[0]);
                    newNonExpiringItem.SupplierId = Convert.ToInt32(reader[1]);
                    newNonExpiringItem.SupplierName = reader[2].ToString();
                    newNonExpiringItem.Name = reader[3].ToString();
                    newNonExpiringItem.Quantity = Convert.ToInt32(reader[4]);
                    newNonExpiringItem.Status = reader[5].ToString();
                    if (newNonExpiringItem.Status == "Unavailable")
                    {
                        newNonExpiringItem.Color = "White";
                    }
                    else if (newNonExpiringItem.Quantity > 0 && newNonExpiringItem.Quantity < 10 && newNonExpiringItem.Status == "Available")
                    {
                        newNonExpiringItem.Color = "Cyan";
                        LowStocksCountNonExpiringItem += 1;
                    }
                    NonExpiringItems.Add(newNonExpiringItem);
                }
                reader.Close();


                query = "SELECT `disposednonexpiringitem`.`id`,`nonexpiringitem`.`id`,`nonexpiringitem`.`name`,`disposednonexpiringitem`.`quantitydisposed`,`disposednonexpiringitem`.`datedisposed` FROM `nonexpiringitem` INNER JOIN `disposednonexpiringitem` ON `nonexpiringitem`.`id` = `disposednonexpiringitem`.`nonexpiringitem_id` WHERE `disposednonexpiringitem`.`status` = 'Active'";
                comm = new MySqlCommand(query, connection);
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    DisposedNonExpiringItem newDisposedNonExpiringItem = new DisposedNonExpiringItem();
                    newDisposedNonExpiringItem.Id = Convert.ToInt32(reader[0]);
                    newDisposedNonExpiringItem.StockId = Convert.ToInt32(reader[1]);
                    newDisposedNonExpiringItem.Name = reader[2].ToString();
                    newDisposedNonExpiringItem.Quantity = Convert.ToInt32(reader[3]);
                    newDisposedNonExpiringItem.DateDisposed = reader[4].ToString();
                    DisposedNonExpiringItems.Add(newDisposedNonExpiringItem);
                }
                reader.Close();

                query = "SELECT `name`,`quantityused`,`dateused` FROM `useditem`";
                comm = new MySqlCommand(query, connection);
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    UsedItem useditem = new UsedItem();
                    useditem.Name = reader[0].ToString();
                    useditem.Quantity = Convert.ToInt32(reader[1]);
                    useditem.DateUsed = reader[2].ToString();
                    UsedItems.Add(useditem);
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
        #endregion
    }
}
