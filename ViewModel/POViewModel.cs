using CrystalDecisions.CrystalReports.Engine;
using GalaSoft.MvvmLight.CommandWpf;
using MySql.Data.MySqlClient;
using SmileLineDentalClinic.DataSet;
using SmileLineDentalClinic.Model;
using SmileLineDentalClinic.Reports;
using SmileLineDentalClinic.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SmileLineDentalClinic.ViewModel
{
    public class POViewModel : ValidatableModel
    {
        public int AccountId { get; set; }

        private int stockid;

        public int StockId
        {
            get { return stockid; }
            set
            {
                stockid = value;
                RaisePropertyChanged("StockId");
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

        private string suppliername;

        public string SupplierName
        {
            get { return suppliername; }
            set
            {
                suppliername = value;
                RaisePropertyChanged("SupplierName");
            }
        }



        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                RaisePropertyChanged("Quantity");
            }
        }

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
            set
            {
                selectedexpiringitem = value;
                RaisePropertyChanged("SelectedExpiringItem");
                if (SelectedExpiringItem != null)
                {
                    SelectedNonExpiringItem = null;
                    IsExpiringItemChoosen = true;
                    StockId = SelectedExpiringItem.Id;
                    Name = SelectedExpiringItem.Name;
                    SupplierName = SelectedExpiringItem.SupplierName;
                    Type = "ExpiringItem";
                }
                else
                {
                    IsExpiringItemChoosen = false;
                }
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
                if (SelectedNonExpiringItem != null)
                {
                    SelectedExpiringItem = null;
                    IsExpiringItemChoosen = true;
                    StockId = SelectedNonExpiringItem.Id;
                    Name = SelectedNonExpiringItem.Name;
                    SupplierName = SelectedNonExpiringItem.SupplierName;
                    Type = "NonExpiringItem";
                }
                else
                {
                    IsExpiringItemChoosen = false;
                }
            }
        }

        private OrderedExpiringItem selectedorderedexpiringItemDetails;

        public OrderedExpiringItem SelectedOrderedExpiringItemDetails
        {
            get { return selectedorderedexpiringItemDetails; }
            set
            {
                selectedorderedexpiringItemDetails = value;
                RaisePropertyChanged("SelectedOrderedExpiringItemDetails");
            }
        }

        private OrderedNonExpiringItem selectedorderednonexpiringItemDetails;

        public OrderedNonExpiringItem SelectedOrderedNonExpiringItemDetails
        {
            get { return selectedorderednonexpiringItemDetails; }
            set
            {
                selectedorderednonexpiringItemDetails = value;
                RaisePropertyChanged("SelectedOrderedNonExpiringItemDetails");
            }
        }





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

        private bool isexpiringitemchoosen;

        public bool IsExpiringItemChoosen
        {
            get { return isexpiringitemchoosen; }
            set
            {
                isexpiringitemchoosen = value;
                RaisePropertyChanged("IsExpiringItemChoosen");
            }
        }

        private bool isnonexpiringitemchoosen;

        public bool IsNonExpiringItemChoosen
        {
            get { return isnonexpiringitemchoosen; }
            set
            {
                isnonexpiringitemchoosen = value;
                RaisePropertyChanged("IsNonExpiringItemChoosen");
            }
        }



        private string searchordereditems;

        public string SearchOrderedExpiringItems
        {
            get { return searchordereditems; }
            set
            {
                searchordereditems = value;
                RaisePropertyChanged("SearchOrderedExpiringItems");
                OrderedExpiringItemsView.Refresh();
            }
        }


        private string searchorderednonitems;

        public string SearchOrderedNonExpiringItems
        {
            get { return searchorderednonitems; }
            set
            {
                searchorderednonitems = value;
                RaisePropertyChanged("SearchOrderedNonExpiringItems");
                OrderedNonExpiringItemsView.Refresh();
            }
        }

        public ICollectionView SupplierView { get; set; }
        public ICollectionView OrderedExpiringItemsView { get; set; }
        public ICollectionView OrderedNonExpiringItemsView { get; set; }

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

        private int ordereditemindex;

        public int OrderedExpiringItemIndex
        {
            get { return ordereditemindex; }
            set
            {
                ordereditemindex = value;
                RaisePropertyChanged("OrderedExpiringItemIndex");
            }
        }

        private int orderednonitemindex;

        public int OrderedNonExpiringItemIndex
        {
            get { return orderednonitemindex; }
            set
            {
                orderednonitemindex = value;
                RaisePropertyChanged("OrderedNonExpiringItemIndex");
            }
        }





        public ICollectionView ExpiringItemsView { get; set; }
        public ICollectionView NonExpiringItemsView { get; set; }

        public OrderedExpiringItem OrderedExpiringItem { get; set; }
        public RelayCommand SelectSupplier { get; set; }
        public RelayCommand SelectSupplier2 { get; set; }
        public RelayCommand SavePO { get; set; }
        public RelayCommand SavePO2 { get; set; }
        public RelayCommand PrintPO { get; set; }
        public RelayCommand AddtoOrder { get; set; }
        public RelayCommand AddtoOrder2 { get; set; }
        public RelayCommand DeleteItem { get; set; }
        public RelayCommand ClearOrders { get; set; }
        public RelayCommand DeleteItem2 { get; set; }
        public RelayCommand ClearOrders2 { get; set; }
        public ObservableCollection<Supplier> Suppliers { get; set; }
        public ObservableCollection<ExpiringItem> ExpiringItems { get; set; }
        public ObservableCollection<NonExpiringItem> NonExpiringItems { get; set; }
        public ObservableCollection<OrderedNonExpiringItem> OrderedNonExpiringItems { get; set; }
        public ObservableCollection<OrderedExpiringItem> OrderedExpiringItems { get; set; }
        public ObservableCollection<OrderedExpiringItem> OrderedExpiringItemsDetails { get; set; }
        public ObservableCollection<OrderedNonExpiringItem> OrderedNonExpiringItemsDetails { get; set; }
        public POViewModel()
        {
            Quantity = 0;
            AccountId = 0;
            Suppliers = new ObservableCollection<Supplier>();
            ExpiringItems = new ObservableCollection<ExpiringItem>();
            NonExpiringItems = new ObservableCollection<NonExpiringItem>();
            OrderedExpiringItems = new ObservableCollection<OrderedExpiringItem>();
            OrderedExpiringItemsDetails = new ObservableCollection<OrderedExpiringItem>();
            OrderedNonExpiringItems = new ObservableCollection<OrderedNonExpiringItem>();
            OrderedNonExpiringItemsDetails = new ObservableCollection<OrderedNonExpiringItem>();
            SelectedSupplier = new Supplier();
            OrderedExpiringItem = new OrderedExpiringItem();
            setup();
            SupplierView = CollectionViewSource.GetDefaultView(Suppliers);
            SupplierView.Filter = supplierviewfilter;
            OrderedExpiringItemsView = CollectionViewSource.GetDefaultView(OrderedExpiringItemsDetails);
            OrderedExpiringItemsView.Filter = ordereditemsviewfilter;
            OrderedNonExpiringItemsView = CollectionViewSource.GetDefaultView(OrderedNonExpiringItemsDetails);
            OrderedNonExpiringItemsView.Filter = orderednonexpiringitemsviewfilter;
            ExpiringItemsView = CollectionViewSource.GetDefaultView(ExpiringItems);
            ExpiringItemsView.Filter = expiringitemsviewfilter;
            NonExpiringItemsView = CollectionViewSource.GetDefaultView(NonExpiringItems);
            NonExpiringItemsView.Filter = nonexpiringitemsviewfilter;
            SelectSupplier = new RelayCommand(selectsupplier, canselectsupplier);

            AddtoOrder = new RelayCommand(addtoorder, canaddtoorder);
            SavePO = new RelayCommand(savepo);
            DeleteItem = new RelayCommand(deleteitem);
            ClearOrders = new RelayCommand(clearorders);
            PrintPO = new RelayCommand(printpo);

        }

        private bool ordereditemsviewfilter(object item)
        {
            if (String.IsNullOrEmpty(SearchOrderedExpiringItems))
                return true;
            else
                return ((item as OrderedExpiringItem).Id.ToString().IndexOf(SearchOrderedExpiringItems, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as OrderedExpiringItem).Name.IndexOf(SearchOrderedExpiringItems, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as OrderedExpiringItem).Quantity.ToString().IndexOf(SearchOrderedExpiringItems, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as OrderedExpiringItem).DateTimeOrdered.IndexOf(SearchOrderedExpiringItems, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as OrderedExpiringItem).EmployeeName.IndexOf(SearchOrderedExpiringItems, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as OrderedExpiringItem).Status.IndexOf(SearchOrderedExpiringItems, StringComparison.OrdinalIgnoreCase) >= 0);

        }

        private bool orderednonexpiringitemsviewfilter(object item)
        {
            if (String.IsNullOrEmpty(SearchOrderedNonExpiringItems))
                return true;
            else
                return ((item as OrderedNonExpiringItem).Id.ToString().IndexOf(SearchOrderedNonExpiringItems, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as OrderedNonExpiringItem).Name.IndexOf(SearchOrderedNonExpiringItems, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as OrderedNonExpiringItem).Quantity.ToString().IndexOf(SearchOrderedNonExpiringItems, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as OrderedNonExpiringItem).DateTimeOrdered.IndexOf(SearchOrderedNonExpiringItems, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as OrderedNonExpiringItem).EmployeeName.IndexOf(SearchOrderedNonExpiringItems, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as OrderedNonExpiringItem).Status.IndexOf(SearchOrderedNonExpiringItems, StringComparison.OrdinalIgnoreCase) >= 0);

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

        private bool expiringitemsviewfilter(object item)
        {
            if (String.IsNullOrEmpty(SearchExpiringItem))
                return true;
            else
                return ((item as ExpiringItem).Id.ToString().IndexOf(SearchExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as ExpiringItem).SupplierName.IndexOf(SearchExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as ExpiringItem).Name.IndexOf(SearchExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as ExpiringItem).ExpirationDate.IndexOf(SearchExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as ExpiringItem).Quantity.ToString().IndexOf(SearchExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as ExpiringItem).Status.IndexOf(SearchExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private bool nonexpiringitemsviewfilter(object item)
        {
            if (String.IsNullOrEmpty(SearchNonExpiringItem))
                return true;
            else
                return ((item as NonExpiringItem).Id.ToString().IndexOf(SearchNonExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as NonExpiringItem).SupplierName.IndexOf(SearchNonExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as NonExpiringItem).Name.IndexOf(SearchNonExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as NonExpiringItem).Quantity.ToString().IndexOf(SearchNonExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as NonExpiringItem).Status.IndexOf(SearchNonExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0);
        }


        private void setup()
        {
            SelectedExpiringItem = null;
            Suppliers.Clear();
            OrderedExpiringItemsDetails.Clear();
            OrderedNonExpiringItemsDetails.Clear();
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "SELECT * FROM `supplier` where `status` = 'Active'";
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


                query = "SELECT `orderedexpiringitem`.`id`,`orderedexpiringitem`.`purchaseordernumber_id`,`orderedexpiringitem`.`quantityordered`,`orderedexpiringitem`.`datetimeordered`,`expiringitem`.`id`,`expiringitem`.`name`,`supplier`.`id`,`supplier`.`name`,`account`.`id`,`account`.`firstname`,`account`.`middlename`,`account`.`lastname`,`orderedexpiringitem`.`status` FROM `orderedexpiringitem` INNER JOIN `expiringitem` ON `orderedexpiringitem`.`expiringitem_id` = `expiringitem`.`id` INNER JOIN `supplier` ON `orderedexpiringitem`.`expiringitem_supplier_id` = `supplier`.`id` INNER JOIN `account` ON `orderedexpiringitem`.`account_id` = `account`.`id`";
                comm = new MySqlCommand(query, connection);
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    OrderedExpiringItem newOrdereditem = new OrderedExpiringItem();
                    newOrdereditem.No = Convert.ToInt32(reader[0]);
                    newOrdereditem.Id = Convert.ToInt32(reader[1]);
                    newOrdereditem.Quantity = Convert.ToInt32(reader[2]);
                    newOrdereditem.DateTimeOrdered = reader[3].ToString();
                    newOrdereditem.StockId = Convert.ToInt32(reader[4]);
                    newOrdereditem.Name = reader[5].ToString();
                    newOrdereditem.SupplierId = Convert.ToInt32(reader[6]);
                    newOrdereditem.Supplier = reader[7].ToString();
                    newOrdereditem.EmployeeId = Convert.ToInt32(reader[8]);
                    newOrdereditem.EmployeeName = reader[9].ToString() + " " + reader[10].ToString() + " " + reader[11].ToString();
                    newOrdereditem.Status = reader[12].ToString();
                    OrderedExpiringItemsDetails.Add(newOrdereditem);
                }

                reader.Close();

                query = "SELECT `orderednonexpiringitem`.`id`,`orderednonexpiringitem`.`purchaseordernumber_id`,`orderednonexpiringitem`.`quantityordered`,`orderednonexpiringitem`.`datetimeordered`,`nonexpiringitem`.`id`,`nonexpiringitem`.`name`,`supplier`.`id`,`supplier`.`name`,`account`.`id`,`account`.`firstname`,`account`.`middlename`,`account`.`lastname`,`orderednonexpiringitem`.`status` FROM `orderednonexpiringitem` INNER JOIN `nonexpiringitem` ON `orderednonexpiringitem`.`nonexpiringitem_id` = `nonexpiringitem`.`id` INNER JOIN `supplier` ON `orderednonexpiringitem`.`nonexpiringitem_supplier_id` = `supplier`.`id` INNER JOIN `account` ON `orderednonexpiringitem`.`account_id` = `account`.`id`";
                comm = new MySqlCommand(query, connection);
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    OrderedNonExpiringItem newOrdereditem = new OrderedNonExpiringItem();
                    newOrdereditem.No = Convert.ToInt32(reader[0]);
                    newOrdereditem.Id = Convert.ToInt32(reader[1]);
                    newOrdereditem.Quantity = Convert.ToInt32(reader[2]);
                    newOrdereditem.DateTimeOrdered = reader[3].ToString();
                    newOrdereditem.StockId = Convert.ToInt32(reader[4]);
                    newOrdereditem.Name = reader[5].ToString();
                    newOrdereditem.SupplierId = Convert.ToInt32(reader[6]);
                    newOrdereditem.Supplier = reader[7].ToString();
                    newOrdereditem.EmployeeId = Convert.ToInt32(reader[8]);
                    newOrdereditem.EmployeeName = reader[9].ToString() + " " + reader[10].ToString() + " " + reader[11].ToString();
                    newOrdereditem.Status = reader[12].ToString();
                    OrderedNonExpiringItemsDetails.Add(newOrdereditem);
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

        private void selectsupplier()
        {
            NonExpiringItems.Clear();
            ExpiringItems.Clear();
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "SELECT `expiringitem`.`id`,`supplier`.`name`,`expiringitem`.`name`,`expiringitem`.`expirationdate`,`expiringitem`.`quantity`,`expiringitem`.`status` FROM `expiringitem` INNER JOIN `supplier` ON `expiringitem`.`supplier_id`=`supplier`.`id` WHERE `expiringitem`.`status` = 'Available' AND `expiringitem`.`supplier_id` = @supplierid ORDER BY `expiringitem`.`quantity`";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@supplierid", SelectedSupplier.Id);
                MySqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    ExpiringItem newExpiringItem = new ExpiringItem();
                    newExpiringItem.Id = Convert.ToInt32(reader[0]);
                    newExpiringItem.SupplierName = reader[1].ToString();
                    newExpiringItem.Name = reader[2].ToString();
                    newExpiringItem.ExpirationDate = reader[3].ToString();
                    newExpiringItem.Quantity = Convert.ToInt32(reader[4]);
                    newExpiringItem.Status = reader[5].ToString();
                    ExpiringItems.Add(newExpiringItem);
                }
                reader.Close();

                query = "SELECT `nonexpiringitem`.`id`,`supplier`.`name`,`nonexpiringitem`.`name`,`nonexpiringitem`.`quantity`,`nonexpiringitem`.`status` FROM `nonexpiringitem` INNER JOIN `supplier` ON `nonexpiringitem`.`supplier_id`=`supplier`.`id` WHERE `nonexpiringitem`.`status` = 'Available' AND `nonexpiringitem`.`supplier_id` = @supplierid ORDER BY `nonexpiringitem`.`quantity`";
                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@supplierid", SelectedSupplier.Id);
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    NonExpiringItem newnonExpiringItem = new NonExpiringItem();
                    newnonExpiringItem.Id = Convert.ToInt32(reader[0]);
                    newnonExpiringItem.SupplierName = reader[1].ToString();
                    newnonExpiringItem.Name = reader[2].ToString();
                    newnonExpiringItem.Quantity = Convert.ToInt32(reader[3]);
                    newnonExpiringItem.Status = reader[4].ToString();
                    NonExpiringItems.Add(newnonExpiringItem);
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

        private bool canselectsupplier()
        {
            return OrderedExpiringItems.Count <= 0;
        }

        private void addtoorder()
        {
            OrderedExpiringItem newOrderedExpiringItem = new OrderedExpiringItem();
            newOrderedExpiringItem.StockId = StockId;
            newOrderedExpiringItem.Supplier = SupplierName;
            newOrderedExpiringItem.Name = Name;
            newOrderedExpiringItem.Quantity = Quantity;
            newOrderedExpiringItem.Type = Type;
            OrderedExpiringItems.Add(newOrderedExpiringItem);
        }

        private bool canaddtoorder()
        {
            if (SelectedExpiringItem != null)
                return IsItemExist(SelectedExpiringItem);
            if (SelectedNonExpiringItem != null)
                return IsItemExist(SelectedNonExpiringItem);
            return false;
        }

        private bool IsItemExist(ExpiringItem selecteditem)
        {
            if (OrderedExpiringItems.Count > 0)
            {
                foreach (OrderedExpiringItem OrderedExpiringItem in OrderedExpiringItems)
                {
                    if (OrderedExpiringItem.StockId == selecteditem.Id && OrderedExpiringItem.Type == Type)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool IsItemExist(NonExpiringItem selecteditem)
        {
            if (OrderedExpiringItems.Count > 0)
            {
                foreach (OrderedExpiringItem OrderedExpiringItem in OrderedExpiringItems)
                {
                    if (OrderedExpiringItem.StockId == selecteditem.Id && OrderedExpiringItem.Type == Type)
                    {
                        return false;
                    }
                }
            }
            return true;
        }




        private void savepo()
        {
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "INSERT INTO `purchaseordernumber` VALUES(null)";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.ExecuteNonQuery();
                query = "SELECT MAX(`id`) FROM `purchaseordernumber`";
                comm = new MySqlCommand(query, connection);
                var result = comm.ExecuteScalar();

                int orderno = Convert.ToInt32(result);

                var date = DateTime.Now;
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);

                foreach (OrderedExpiringItem OrderedExpiringItem in OrderedExpiringItems)
                {
                    if (OrderedExpiringItem.Type == "ExpiringItem")
                    {
                        query = "INSERT INTO `orderedexpiringitem` VALUES (null,@purchaseordernumber_id,@expiringitem_id,@expiringitem_supplier_id,@quantityordered,@datetimeordered,@account_id,@status)";
                        comm = new MySqlCommand(query, connection);

                        comm.Parameters.AddWithValue("@purchaseordernumber_id", orderno);
                        comm.Parameters.AddWithValue("@quantityordered", OrderedExpiringItem.Quantity);
                        comm.Parameters.AddWithValue("@datetimeordered", date);
                        comm.Parameters.AddWithValue("@expiringitem_id", OrderedExpiringItem.StockId);
                        comm.Parameters.AddWithValue("@expiringitem_supplier_id", SelectedSupplier.Id);
                        comm.Parameters.AddWithValue("@account_id", AccountId);
                        comm.Parameters.AddWithValue("@status", "Pending");
                        comm.ExecuteNonQuery();
                        comm.Parameters.Clear();
                    }
                    if (OrderedExpiringItem.Type == "NonExpiringItem")
                    {
                        query = "INSERT INTO `orderednonexpiringitem` VALUES (null,@purchaseordernumber_id,@nonexpiringitem_id,@nonexpiringitem_supplier_id,@quantityordered,@datetimeordered,@account_id,@status)";
                        comm = new MySqlCommand(query, connection);
                        comm.Parameters.AddWithValue("@purchaseordernumber_id", orderno);
                        comm.Parameters.AddWithValue("@quantityordered", OrderedExpiringItem.Quantity);
                        comm.Parameters.AddWithValue("@datetimeordered", date);
                        comm.Parameters.AddWithValue("@nonexpiringitem_id", OrderedExpiringItem.StockId);
                        comm.Parameters.AddWithValue("@nonexpiringitem_supplier_id", SelectedSupplier.Id);
                        comm.Parameters.AddWithValue("@account_id", AccountId);
                        comm.Parameters.AddWithValue("@status", "Pending");
                        comm.ExecuteNonQuery();
                        comm.Parameters.Clear();
                    }
                }

                TransactionLog transactionLog = new TransactionLog();
                transactionLog.Description = "Purchase Order";

                transactionLog.Transactedin = date;
                transactionLog.Details = "Added New Order: Order No. " + orderno;
                transactionLog.AccountId = this.AccountId;


                query = "INSERT INTO `transactionlog` VALUES (null,@description,@transactedin,@details,@account_id)";
                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@description", transactionLog.Description);
                comm.Parameters.AddWithValue("@transactedin", transactionLog.Transactedin);
                comm.Parameters.AddWithValue("@details", transactionLog.Details);
                comm.Parameters.AddWithValue("@account_id", transactionLog.AccountId);
                comm.ExecuteNonQuery();

                MessageBox.Show("New Order Added!");
                OrderedExpiringItems.Clear();
                ExpiringItems.Clear();
                NonExpiringItems.Clear();
                SelectedExpiringItem = null;
                SelectedNonExpiringItem = null;

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


        private void deleteitem()
        {
            OrderedExpiringItems.RemoveAt(OrderedExpiringItemIndex);
        }

        private void clearorders()
        {
            OrderedExpiringItems.Clear();
        }

        private void printpo()
        {
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";

            connection = new MySqlConnection(connString);
            try
            {
                int totalquantityordered;
                connection.Open();
                PurchaseOrder report = new PurchaseOrder();
                report.Load();
                CrystalReportView view = new CrystalReportView();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter();
                String query = "SELECT `name`,`quantityordered` FROM `poviewmain` WHERE `purchaseordernumber_id` = @id";
                  
                MySqlCommand comm = new MySqlCommand(query, connection);
                if (SelectedOrderedExpiringItemDetails != null)
                {
                    SelectedOrderedNonExpiringItemDetails = null;
                    comm.Parameters.AddWithValue("@id", SelectedOrderedExpiringItemDetails.Id);
                    da.SelectCommand = comm;
                    da.Fill(dt);
                    report.SetDataSource(dt);
                    comm.Parameters.Clear();

                    query = "SELECT SUM(`quantityordered`) FROM `poviewmain` WHERE `purchaseordernumber_id` = @id";
                    comm = new MySqlCommand(query, connection);
                    comm.Parameters.AddWithValue("@id", SelectedOrderedExpiringItemDetails.Id);
                    totalquantityordered = Convert.ToInt32(comm.ExecuteScalar());
                    report.SetParameterValue("totalquantity", totalquantityordered);
                    comm.Parameters.Clear();

                    

                    query = "SELECT * FROM `supplier` WHERE `id` = @id";
                    comm = new MySqlCommand(query, connection);
                    comm.Parameters.AddWithValue("@id", SelectedOrderedExpiringItemDetails.SupplierId);
                    MySqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        report.SetParameterValue("suppliername", reader[1].ToString());
                        report.SetParameterValue("representative", reader[2]);
                        report.SetParameterValue("address", reader[3]);
                        report.SetParameterValue("contactnumber", reader[4]);
                        report.SetParameterValue("emailaddress", reader[5]);
                    }
                    reader.Close();

                    query = "SELECT * FROM `account` WHERE `id` = @id";
                    comm = new MySqlCommand(query, connection);
                    comm.Parameters.AddWithValue("@id", this.AccountId);
                    reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        report.SetParameterValue("accountname", reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString());
                    }
                    reader.Close();

                    report.SetParameterValue("dateordered", Convert.ToDateTime(SelectedOrderedExpiringItemDetails.DateTimeOrdered).ToShortDateString());
                    report.SetParameterValue("orderno", SelectedOrderedExpiringItemDetails.Id);
                    comm.Parameters.Clear();
                    dt.Clear();
                }
                if (SelectedOrderedNonExpiringItemDetails != null)
                {
                    SelectedOrderedExpiringItemDetails = null;
                    comm.Parameters.AddWithValue("@id", SelectedOrderedNonExpiringItemDetails.Id);
                    da.SelectCommand = comm;
                    da.Fill(dt);
                    report.SetDataSource(dt);
                    comm.Parameters.Clear();

                    query = "SELECT SUM(`quantityordered`) FROM `poviewmain` WHERE `purchaseordernumber_id` = @id";
                    comm = new MySqlCommand(query, connection);
                    comm.Parameters.AddWithValue("@id", SelectedOrderedNonExpiringItemDetails.Id);
                    totalquantityordered = Convert.ToInt32(comm.ExecuteScalar());
                    report.SetParameterValue("totalquantity", totalquantityordered);
                    comm.Parameters.Clear();



                    query = "SELECT * FROM `supplier` WHERE `id` = @id";
                    comm = new MySqlCommand(query, connection);
                    comm.Parameters.AddWithValue("@id", SelectedOrderedNonExpiringItemDetails.SupplierId);
                    MySqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        report.SetParameterValue("suppliername", reader[1].ToString());
                        report.SetParameterValue("representative", reader[2]);
                        report.SetParameterValue("address", reader[3]);
                        report.SetParameterValue("contactnumber", reader[4]);
                        report.SetParameterValue("emailaddress", reader[5]);
                    }
                    reader.Close();

                    query = "SELECT * FROM `account` WHERE `id` = @id";
                    comm = new MySqlCommand(query, connection);
                    comm.Parameters.AddWithValue("@id", this.AccountId);
                    reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        report.SetParameterValue("accountname", reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString());
                    }
                    reader.Close();

                    report.SetParameterValue("dateordered", Convert.ToDateTime(SelectedOrderedNonExpiringItemDetails.DateTimeOrdered).ToShortDateString());
                    report.SetParameterValue("orderno", SelectedOrderedNonExpiringItemDetails.Id);
                    comm.Parameters.Clear();
                    dt.Clear();
                }

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
    }
}
