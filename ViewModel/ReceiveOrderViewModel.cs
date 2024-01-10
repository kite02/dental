using GalaSoft.MvvmLight.CommandWpf;
using MySql.Data.MySqlClient;
using SmileLineDentalClinic.Model;
using SmileLineDentalClinic.Reports;
using SmileLineDentalClinic.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SmileLineDentalClinic.ViewModel
{
    public class ReceiveOrderViewModel : ValidatableModel
    {
        public ObservableCollection<Supplier> Suppliers { get; set; }
        public ObservableCollection<OrderedExpiringItem> OrderedExpiringItems { get; set; }
        public ObservableCollection<ReceivedExpiringItem> ReceivedExpiringItems { get; set; }
        public ObservableCollection<ReceivedExpiringItem> ReceivedExpiringItemsDetails { get; set; }
        public ObservableCollection<OrderedNonExpiringItem> OrderedNonExpiringItems { get; set; }
        public ObservableCollection<ReceivedNonExpiringItem> ReceivedNonExpiringItems { get; set; }
        public ObservableCollection<ReceivedNonExpiringItem> ReceivedNonExpiringItemsDetails { get; set; }

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


        private OrderedExpiringItem selectedordereditem;

        public OrderedExpiringItem SelectedOrderedExpiringItem
        {
            get { return selectedordereditem; }
            set
            {
                selectedordereditem = value;
                RaisePropertyChanged("SelectedOrderedExpiringItem");
                if (SelectedOrderedExpiringItem != null)
                {
                    SelectedOrderedNonExpiringItem = null;
                    PONumber = SelectedOrderedExpiringItem.No;
                    PoId = SelectedOrderedExpiringItem.Id;
                    Name = SelectedOrderedExpiringItem.Name;
                    SupplierName = SelectedOrderedExpiringItem.Supplier;
                    Months = SelectedOrderedExpiringItem.Months;
                    Days = SelectedOrderedExpiringItem.Days;
                    Years = SelectedOrderedExpiringItem.Years;
                    StockId = SelectedOrderedExpiringItem.StockId;
                    SupplierId = SelectedOrderedExpiringItem.SupplierId;
                    Type = "OrderedExpiringItem";
                    IsOrderedExpiringItemChoosen = true;
                }
                else
                {
                    IsOrderedExpiringItemChoosen = false;
                }
            }
        }

        private OrderedNonExpiringItem selectedordereditem2;

        public OrderedNonExpiringItem SelectedOrderedNonExpiringItem
        {
            get { return selectedordereditem2; }
            set
            {
                selectedordereditem2 = value;
                RaisePropertyChanged("SelectedOrderedNonExpiringItem");
                if (SelectedOrderedNonExpiringItem != null)
                {
                    SelectedOrderedExpiringItem = null;
                    PONumber = SelectedOrderedNonExpiringItem.No;
                    PoId = SelectedOrderedNonExpiringItem.Id;
                    Name = SelectedOrderedNonExpiringItem.Name;
                    SupplierName = SelectedOrderedNonExpiringItem.Supplier;
                    StockId = SelectedOrderedNonExpiringItem.StockId;
                    SupplierId = SelectedOrderedNonExpiringItem.SupplierId;
                    Type = "OrderedNonExpiringItem";
                    IsOrderedExpiringItemChoosen = true;

                }
                else
                {
                    IsOrderedExpiringItemChoosen = false;
                }
            }
        }

        private int poid;

        public int PoId
        {
            get { return poid; }
            set
            {
                poid = value;
                RaisePropertyChanged("PoId");
            }
        }


        private int ponumber;

        public int PONumber
        {
            get { return ponumber; }
            set
            {
                ponumber = value;
                RaisePropertyChanged("PONumber");
            }
        }



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

        private int supplierid;

        public int SupplierId
        {
            get { return supplierid; }
            set
            {
                supplierid = value;
                RaisePropertyChanged("SupplierId");
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






        private int quantityordered;
        [CustomValidation(typeof(ReceiveOrderViewModel), "ReceiveOrderValidate")]
        public int Quantity
        {
            get { return quantityordered; }
            set
            {
                quantityordered = value;
                RaisePropertyChanged("Quantity");
            }
        }


        private decimal price;

        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                RaisePropertyChanged("Price");
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

        private int months;

        public int Months
        {
            get { return months; }
            set
            {
                months = value;
                RaisePropertyChanged("Months");
            }
        }

        private int days;

        public int Days
        {
            get { return days; }
            set
            {
                days = value;
                RaisePropertyChanged("Days");
            }
        }

        private int years;

        public int Years
        {
            get { return years; }
            set
            {
                years = value;
                RaisePropertyChanged("Years");
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


        private decimal totalprice;

        public decimal TotalCost
        {
            get { return totalprice; }
            set
            {
                totalprice = value;
                RaisePropertyChanged("TotalCost");
            }
        }



        private string deliverynumber;
        [CustomValidation(typeof(ReceiveOrderViewModel), "ReceiveOrderValidate")]
        public string DeliveryNo
        {
            get { return deliverynumber; }
            set
            {
                deliverynumber = value;
                RaisePropertyChanged("DeliveryNo");
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

        private ReceivedExpiringItem selectedreceivedexpiringitemdetail;

        public ReceivedExpiringItem SelectedReceivedExpiringItemDetail
        {
            get { return selectedreceivedexpiringitemdetail; }
            set { selectedreceivedexpiringitemdetail = value;
                RaisePropertyChanged("SelectedReceivedExpiringItemDetail");
            }
        }


        private ReceivedNonExpiringItem selectedreceivednonexpiringitemdetail;

        public ReceivedNonExpiringItem SelectedReceivedNonExpiringItemDetail
        {
            get { return selectedreceivednonexpiringitemdetail; }
            set
            {
                selectedreceivednonexpiringitemdetail = value;
                RaisePropertyChanged("SelectedReceivedNonExpiringItemDetail");
            }
        }





        public ICollectionView OrderedExpiringItemsView { get; set; }
        public ICollectionView ReceivedExpiringItemsView { get; set; }
        public ICollectionView OrderedNonExpiringItemsView { get; set; }
        public ICollectionView ReceivedNonExpiringItemsView { get; set; }

        private string searchreceiveditem;

        public string SearchReceivedExpiringItem
        {
            get { return searchreceiveditem; }
            set
            {
                searchreceiveditem = value;
                RaisePropertyChanged("SearchReceivedExpiringItem");
                ReceivedExpiringItemsView.Refresh();
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

        private string searchordereditems2;

        public string SearchOrderedNonExpiringItems
        {
            get { return searchordereditems2; }
            set
            {
                searchordereditems2 = value;
                RaisePropertyChanged("SearchOrderedNonExpiringItems");
                OrderedNonExpiringItemsView.Refresh();
            }
        }

        private int receiveditemindex;

        public int ReceivedExpiringItemIndex
        {
            get { return receiveditemindex; }
            set
            {
                receiveditemindex = value;
                RaisePropertyChanged("ReceivedExpiringItemIndex");
            }
        }

        private string searchreceiveditem2;

        public string SearchReceivedNonExpiringItem
        {
            get { return searchreceiveditem2; }
            set
            {
                searchreceiveditem2 = value;
                RaisePropertyChanged("SearchReceivedNonExpiringItem");
                ReceivedNonExpiringItemsView.Refresh();
            }
        }





        //private decimal profit;

        //public decimal Profit
        //{
        //    get { return profit; }
        //    set
        //    {
        //        profit = value;
        //        RaisePropertyChanged("Profit");
        //    }
        //}

        private bool isordereditemchoosen;

        public bool IsOrderedExpiringItemChoosen
        {
            get { return isordereditemchoosen; }
            set
            {
                isordereditemchoosen = value;
                RaisePropertyChanged("IsOrderedExpiringItemChoosen");
            }
        }







        public RelayCommand SelectSupplier { get; set; }
        public RelayCommand SaveReceivedOrder { get; set; }
        public RelayCommand PrintReceipt { get; set; }
        public RelayCommand AddtoReceive { get; set; }
        public RelayCommand SetAsPending { get; set; }
        public RelayCommand SetAsReceived { get; set; }
        public RelayCommand DeleteItem { get; set; }
        public RelayCommand ClearItems { get; set; }
        public RelayCommand PrintDR { get; set; }

        public ReceiveOrderViewModel()
        {
            Suppliers = new ObservableCollection<Supplier>();
            OrderedExpiringItems = new ObservableCollection<OrderedExpiringItem>();
            ReceivedExpiringItems = new ObservableCollection<ReceivedExpiringItem>();
            ReceivedExpiringItemsDetails = new ObservableCollection<ReceivedExpiringItem>();
            OrderedNonExpiringItems = new ObservableCollection<OrderedNonExpiringItem>();
            ReceivedNonExpiringItems = new ObservableCollection<ReceivedNonExpiringItem>();
            ReceivedNonExpiringItemsDetails = new ObservableCollection<ReceivedNonExpiringItem>();
            SelectedSupplier = new Supplier();
            SelectSupplier = new RelayCommand(selectsupplier, canselectsupplier);
            AddtoReceive = new RelayCommand(addtoreceive, canaddtoreceive);
            SaveReceivedOrder = new RelayCommand(savereceivedorder, cansavereceivedorder);
            DeleteItem = new RelayCommand(deleteitem);
            ClearItems = new RelayCommand(clearitems);
            PrintDR = new RelayCommand(printdr);
            TotalCost = 0;
            Quantity = 0;
            Price = 0;
            DeliveryNo = String.Empty;
            AccountId = 0;
            setup();
            OrderedExpiringItemsView = CollectionViewSource.GetDefaultView(OrderedExpiringItems);
            OrderedExpiringItemsView.Filter = ordereditemsviewfilter;
            OrderedNonExpiringItemsView = CollectionViewSource.GetDefaultView(OrderedNonExpiringItems);
            OrderedNonExpiringItemsView.Filter = ordereditemsviewfilter2;
            SupplierView = CollectionViewSource.GetDefaultView(Suppliers);
            SupplierView.Filter = supplierviewfilter;
            ReceivedExpiringItemsView = CollectionViewSource.GetDefaultView(ReceivedExpiringItemsDetails);
            ReceivedExpiringItemsView.Filter = receivedexpiringitemviewfilter;
            ReceivedNonExpiringItemsView = CollectionViewSource.GetDefaultView(ReceivedNonExpiringItemsDetails);
            ReceivedNonExpiringItemsView.Filter = receivedexpiringitemviewfilter2;
            //SetAsPending = new RelayCommand(setaspending, cansetaspending);
            //SetAsReceived = new RelayCommand(setasreceived, cansetasreceived);
        }



        private bool receivedexpiringitemviewfilter(object item)
        {
            if (String.IsNullOrEmpty(SearchReceivedExpiringItem))
                return true;
            else
                return ((item as ReceivedExpiringItem).Id.ToString().IndexOf(SearchReceivedExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as ReceivedExpiringItem).SupplierName.IndexOf(SearchReceivedExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as ReceivedExpiringItem).Name.IndexOf(SearchReceivedExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as ReceivedExpiringItem).Price.ToString().IndexOf(SearchReceivedExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as ReceivedExpiringItem).ExpirationDate.IndexOf(SearchReceivedExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)

                     || ((item as ReceivedExpiringItem).QuantityReceived.ToString().IndexOf(SearchReceivedExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as ReceivedExpiringItem).EmployeeName.IndexOf(SearchReceivedExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
            || ((item as ReceivedExpiringItem).Status.IndexOf(SearchReceivedExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private bool receivedexpiringitemviewfilter2(object item)
        {
            if (String.IsNullOrEmpty(SearchReceivedNonExpiringItem))
                return true;
            else
                return ((item as ReceivedNonExpiringItem).Id.ToString().IndexOf(SearchReceivedNonExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as ReceivedNonExpiringItem).SupplierName.IndexOf(SearchReceivedNonExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as ReceivedNonExpiringItem).Name.IndexOf(SearchReceivedNonExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as ReceivedNonExpiringItem).Price.ToString().IndexOf(SearchReceivedNonExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as ReceivedNonExpiringItem).ExpirationDate.IndexOf(SearchReceivedNonExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as ReceivedNonExpiringItem).QuantityReceived.ToString().IndexOf(SearchReceivedNonExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as ReceivedNonExpiringItem).EmployeeName.IndexOf(SearchReceivedNonExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0)
            || ((item as ReceivedNonExpiringItem).Status.IndexOf(SearchReceivedNonExpiringItem, StringComparison.OrdinalIgnoreCase) >= 0);
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

        private bool ordereditemsviewfilter2(object item)
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


        private void setup()
        {
            Suppliers.Clear();
            ReceivedExpiringItems.Clear();
            ReceivedExpiringItemsDetails.Clear();
            ReceivedNonExpiringItems.Clear();
            ReceivedNonExpiringItemsDetails.Clear();
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "SELECT * FROM `supplier`";
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

                query = "SELECT `receiveditemnumber`.`id`,`receivedexpiringitem`.`datereceived`,`orderedexpiringitem`.`id`,`orderedexpiringitem`.`purchaseordernumber_id`,`orderedexpiringitem`.`quantityordered`,`expiringitem`.`id`,`expiringitem`.`name`,`expiringitem`.`expirationdate`,`receivedexpiringitem`.`price`,`receivedexpiringitem`.`quantityreceived`,`supplier`.`id`,`supplier`.`name`,`receivedexpiringitem`.`subtotal`,`account`.`id`,`account`.`firstname`,`account`.`middlename`,`account`.`lastname` FROM `receivedexpiringitem` INNER JOIN `orderedexpiringitem` ON `receivedexpiringitem`.`orderedexpiringitem_id` = `orderedexpiringitem`.`id` INNER JOIN `expiringitem` ON `receivedexpiringitem`.`orderedexpiringitem_expiringitem_id` = `expiringitem`.`id` INNER JOIN `supplier` ON `receivedexpiringitem`.`orderedexpiringitem_expiringitem_supplier_id` = `supplier`.`id` INNER JOIN `account` ON `receivedexpiringitem`.`account_id` = `account`.`id` INNER JOIN `receiveditemnumber` ON `receivedexpiringitem`.`receiveditemnumber_id` = `receiveditemnumber`.`id` ";
                comm = new MySqlCommand(query, connection);
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    ReceivedExpiringItem newReceivedExpiringItem = new ReceivedExpiringItem();
                    newReceivedExpiringItem.Id = reader[0].ToString();
                    newReceivedExpiringItem.DateReceived = reader[1].ToString();
                    newReceivedExpiringItem.PoId = Convert.ToInt32(reader[2]);
                    newReceivedExpiringItem.PoNumber = Convert.ToInt32(reader[3]);
                    newReceivedExpiringItem.QuantityOrdered = Convert.ToInt32(reader[4]);
                    newReceivedExpiringItem.StockId = Convert.ToInt32(reader[5]);
                    newReceivedExpiringItem.Name = reader[6].ToString();
                    newReceivedExpiringItem.ExpirationDate = reader[7].ToString();
                    newReceivedExpiringItem.Price = Convert.ToDecimal(reader[8]);
                    newReceivedExpiringItem.QuantityReceived = Convert.ToInt32(reader[9]);
                    newReceivedExpiringItem.SupplierId = Convert.ToInt32(reader[10]);
                    newReceivedExpiringItem.SupplierName = reader[11].ToString();
                    newReceivedExpiringItem.Subtotal = Convert.ToDecimal(reader[12]);
                    newReceivedExpiringItem.EmployeeId = Convert.ToInt32(reader[13]);
                    newReceivedExpiringItem.EmployeeName = reader[14].ToString() + " " + reader[15].ToString() + " " + reader[16].ToString();
                    ReceivedExpiringItemsDetails.Add(newReceivedExpiringItem);
                }
                reader.Close();

                query = "SELECT `receiveditemnumber`.`id`,`receivednonexpiringitem`.`datereceived`,`orderednonexpiringitem`.`id`,`orderednonexpiringitem`.`purchaseordernumber_id`,`orderednonexpiringitem`.`quantityordered`,`nonexpiringitem`.`id`,`nonexpiringitem`.`name`,`receivednonexpiringitem`.`price`,`receivednonexpiringitem`.`quantityreceived`,`supplier`.`id`,`supplier`.`name`,`receivednonexpiringitem`.`subtotal`,`account`.`id`,`account`.`firstname`,`account`.`middlename`,`account`.`lastname` FROM `receivednonexpiringitem` INNER JOIN `orderednonexpiringitem` ON `receivednonexpiringitem`.`orderednonexpiringitem_id` = `orderednonexpiringitem`.`id` INNER JOIN `nonexpiringitem` ON `receivednonexpiringitem`.`orderednonexpiringitem_nonexpiringitem_id` = `nonexpiringitem`.`id` INNER JOIN `supplier` ON `receivednonexpiringitem`.`orderednonexpiringitem_nonexpiringitem_supplier_id` = `supplier`.`id` INNER JOIN `account` ON `receivednonexpiringitem`.`account_id` = `account`.`id` INNER JOIN `receiveditemnumber` ON `receivednonexpiringitem`.`receiveditemnumber_id` = `receiveditemnumber`.`id` ";
                comm = new MySqlCommand(query, connection);
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    ReceivedNonExpiringItem newReceivedExpiringItem = new ReceivedNonExpiringItem();
                    newReceivedExpiringItem.Id = reader[0].ToString();
                    newReceivedExpiringItem.DateReceived = reader[1].ToString();
                    newReceivedExpiringItem.PoId = Convert.ToInt32(reader[2]);
                    newReceivedExpiringItem.PoNumber = Convert.ToInt32(reader[3]);
                    newReceivedExpiringItem.QuantityOrdered = Convert.ToInt32(reader[4]);
                    newReceivedExpiringItem.StockId = Convert.ToInt32(reader[5]);
                    newReceivedExpiringItem.Name = reader[6].ToString();
                    newReceivedExpiringItem.Price = Convert.ToDecimal(reader[7]);
                    newReceivedExpiringItem.QuantityReceived = Convert.ToInt32(reader[8]);
                    newReceivedExpiringItem.SupplierId = Convert.ToInt32(reader[9]);
                    newReceivedExpiringItem.SupplierName = reader[10].ToString();
                    newReceivedExpiringItem.Subtotal = Convert.ToDecimal(reader[11]);
                    newReceivedExpiringItem.EmployeeId = Convert.ToInt32(reader[12]);
                    newReceivedExpiringItem.EmployeeName = reader[13].ToString() + " " + reader[14].ToString() + " " + reader[15].ToString();
                    ReceivedNonExpiringItemsDetails.Add(newReceivedExpiringItem);
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
            SelectedOrderedExpiringItem = null;
            SelectedOrderedNonExpiringItem = null;
            OrderedNonExpiringItems.Clear();
            OrderedExpiringItems.Clear();
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "SELECT `orderedexpiringitem`.`id`,`orderedexpiringitem`.`purchaseordernumber_id`,`orderedexpiringitem`.`quantityordered`,`orderedexpiringitem`.`datetimeordered`,`expiringitem`.`id`,`expiringitem`.`name`,`expiringitem`.`months`,`expiringitem`.`days`,`expiringitem`.`years`,`supplier`.`id`,`supplier`.`name`,`account`.`id`,`account`.`firstname`,`account`.`middlename`,`account`.`lastname`,`orderedexpiringitem`.`status` FROM `orderedexpiringitem` INNER JOIN `expiringitem` ON `orderedexpiringitem`.`expiringitem_id` = `expiringitem`.`id` INNER JOIN `supplier` ON `orderedexpiringitem`.`expiringitem_supplier_id` = `supplier`.`id` INNER JOIN `account` ON `orderedexpiringitem`.`account_id` = `account`.`id` WHERE `orderedexpiringitem`.`expiringitem_supplier_id` = @supplierid AND `orderedexpiringitem`.`status` = 'Pending'";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@supplierid", SelectedSupplier.Id);
                MySqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    OrderedExpiringItem newOrdereditem = new OrderedExpiringItem();
                    newOrdereditem.Id = Convert.ToInt32(reader[0]);
                    newOrdereditem.No = Convert.ToInt32(reader[1]);
                    newOrdereditem.Quantity = Convert.ToInt32(reader[2]);
                    newOrdereditem.DateTimeOrdered = reader[3].ToString();
                    newOrdereditem.StockId = Convert.ToInt32(reader[4]);
                    newOrdereditem.Name = reader[5].ToString();
                    newOrdereditem.Months = Convert.ToInt32(reader[6]);
                    newOrdereditem.Days = Convert.ToInt32(reader[7]);
                    newOrdereditem.Years = Convert.ToInt32(reader[8]);
                    newOrdereditem.SupplierId = Convert.ToInt32(reader[9]);
                    newOrdereditem.Supplier = reader[10].ToString();
                    newOrdereditem.EmployeeId = Convert.ToInt32(reader[11]);
                    newOrdereditem.EmployeeName = reader[12].ToString() + " " + reader[13].ToString() + " " + reader[14].ToString();
                    newOrdereditem.Status = reader[15].ToString();
                    OrderedExpiringItems.Add(newOrdereditem);
                }

                reader.Close();

                query = "SELECT `orderednonexpiringitem`.`id`,`orderednonexpiringitem`.`purchaseordernumber_id`,`orderednonexpiringitem`.`quantityordered`,`orderednonexpiringitem`.`datetimeordered`,`nonexpiringitem`.`id`,`nonexpiringitem`.`name`,`supplier`.`id`,`supplier`.`name`,`account`.`id`,`account`.`firstname`,`account`.`middlename`,`account`.`lastname`,`orderednonexpiringitem`.`status` FROM `orderednonexpiringitem` INNER JOIN `nonexpiringitem` ON `orderednonexpiringitem`.`nonexpiringitem_id` = `nonexpiringitem`.`id` INNER JOIN `supplier` ON `orderednonexpiringitem`.`nonexpiringitem_supplier_id` = `supplier`.`id` INNER JOIN `account` ON `orderednonexpiringitem`.`account_id` = `account`.`id` WHERE `orderednonexpiringitem`.`nonexpiringitem_supplier_id` = @supplierid AND `orderednonexpiringitem`.`status` = 'Pending'";

                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@supplierid", SelectedSupplier.Id);
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    OrderedNonExpiringItem newOrdereditem = new OrderedNonExpiringItem();
                    newOrdereditem.Id = Convert.ToInt32(reader[0]);
                    newOrdereditem.No = Convert.ToInt32(reader[1]);
                    newOrdereditem.Quantity = Convert.ToInt32(reader[2]);
                    newOrdereditem.DateTimeOrdered = reader[3].ToString();
                    newOrdereditem.StockId = Convert.ToInt32(reader[4]);
                    newOrdereditem.Name = reader[5].ToString();
                    newOrdereditem.SupplierId = Convert.ToInt32(reader[6]);
                    newOrdereditem.Supplier = reader[7].ToString();
                    newOrdereditem.EmployeeId = Convert.ToInt32(reader[8]);
                    newOrdereditem.EmployeeName = reader[9].ToString() + " " + reader[10].ToString() + " " + reader[11].ToString();
                    newOrdereditem.Status = reader[12].ToString();
                    OrderedNonExpiringItems.Add(newOrdereditem);
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
            return ReceivedExpiringItems.Count <= 0;
        }





        private bool canaddtoreceive()
        {
            if (SelectedOrderedExpiringItem != null)
                return this.HasErrors == false && SelectedOrderedExpiringItem.Status != "Received" && IsItemExist(SelectedOrderedExpiringItem);
            if (SelectedOrderedNonExpiringItem != null)
                return this.HasErrors == false && SelectedOrderedNonExpiringItem.Status != "Received" && IsItemExist(SelectedOrderedNonExpiringItem);
            return false;
        }

        private bool IsItemExist(OrderedExpiringItem orderedItem)
        {
            if (ReceivedExpiringItems.Count > 0)
            {
                foreach (ReceivedExpiringItem ReceiveItem in ReceivedExpiringItems)
                {
                    if (ReceiveItem.PoId == orderedItem.Id && ReceiveItem.Type == Type)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        private bool IsItemExist(OrderedNonExpiringItem orderedItem)
        {
            if (ReceivedExpiringItems.Count > 0)
            {
                foreach (ReceivedExpiringItem ReceiveItem in ReceivedExpiringItems)
                {
                    if (ReceiveItem.PoId == orderedItem.Id && ReceiveItem.Type == Type)
                    {
                        return false;
                    }
                }
            }
            return true;
        }





        private void deleteitem()
        {
            TotalCost = TotalCost - ReceivedExpiringItems[ReceivedExpiringItemIndex].Subtotal;
            ReceivedExpiringItems.RemoveAt(ReceivedExpiringItemIndex);

        }

        private void clearitems()
        {
            ReceivedExpiringItems.Clear();
            TotalCost = 0;
        }




        private void addtoreceive()
        {
            ReceivedExpiringItem newReceivedExpiringItem = new ReceivedExpiringItem();
            newReceivedExpiringItem.No = PONumber;
            newReceivedExpiringItem.PoId = PoId;
            newReceivedExpiringItem.Name = Name;
            newReceivedExpiringItem.Months = Months;
            newReceivedExpiringItem.Days = Days;
            newReceivedExpiringItem.Years = Years;
            newReceivedExpiringItem.StockId = StockId;
            newReceivedExpiringItem.SupplierId = SupplierId;
            newReceivedExpiringItem.Price = Price;
            newReceivedExpiringItem.QuantityReceived = Quantity;
            newReceivedExpiringItem.Subtotal = newReceivedExpiringItem.Price * newReceivedExpiringItem.QuantityReceived;
            newReceivedExpiringItem.Type = Type;
            ReceivedExpiringItems.Add(newReceivedExpiringItem);
            TotalCost += newReceivedExpiringItem.Subtotal;
        }



        private bool cansavereceivedorder()
        {
            return this.HasErrors == false;
        }

        private void savereceivedorder()
        {
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                var date = DateTime.Now;
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);

                String query = "INSERT INTO `receiveditemnumber` VALUES (null)";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.ExecuteNonQuery();

                query = "SELECT MAX(`id`) FROM `receiveditemnumber`";
                comm = new MySqlCommand(query, connection);
                var result = comm.ExecuteScalar();
                int deliveryno = Convert.ToInt32(result);



                foreach (ReceivedExpiringItem ReceivedExpiringItem in ReceivedExpiringItems)
                {
                    if (ReceivedExpiringItem.Type == "OrderedExpiringItem")
                    {

                        query = "INSERT INTO `receivedexpiringitem` VALUES (null,@deliverynumber,@datereceived,@orderedexpiringitem_id,@orderedexpiringitem_expiringitem_id,@orderedexpiringitem_expiringitem_supplier_id,@price,@quantityreceived,@subtotal,@account_id)";

                        comm = new MySqlCommand(query, connection);

                        comm.Parameters.AddWithValue("@deliverynumber", deliveryno);
                        comm.Parameters.AddWithValue("@datereceived", date);
                        comm.Parameters.AddWithValue("@orderedexpiringitem_id", ReceivedExpiringItem.PoId);
                        comm.Parameters.AddWithValue("@orderedexpiringitem_expiringitem_id", ReceivedExpiringItem.StockId);
                        comm.Parameters.AddWithValue("@orderedexpiringitem_expiringitem_supplier_id", ReceivedExpiringItem.SupplierId);
                        comm.Parameters.AddWithValue("@price", ReceivedExpiringItem.Price);
                        comm.Parameters.AddWithValue("@quantityreceived", ReceivedExpiringItem.QuantityReceived);
                        comm.Parameters.AddWithValue("@subtotal", ReceivedExpiringItem.Subtotal);
                        comm.Parameters.AddWithValue("@account_id", this.AccountId);
                        comm.ExecuteNonQuery();
                        comm.Parameters.Clear();

                        query = "SELECT COUNT(*) FROM `expiringitem` WHERE `expirationdate` = @expirationdate AND `id` = @id";
                        comm = new MySqlCommand(query, connection);
                        comm.Parameters.AddWithValue("@expirationdate", DateTime.Today.AddMonths(ReceivedExpiringItem.Months).AddDays(ReceivedExpiringItem.Days).AddYears(ReceivedExpiringItem.Years).ToShortDateString());
                        comm.Parameters.AddWithValue("@id", ReceivedExpiringItem.StockId);
                        int count = Convert.ToInt32(comm.ExecuteScalar());
                        comm.Parameters.Clear();


                        if (count < 1)
                        {
                            query = "INSERT INTO `expiringitem` VALUES(null,@supplier_id,@name,@months,@days,@years,@expirationdate,@quantity,@status)";
                            comm = new MySqlCommand(query, connection);
                            comm.Parameters.AddWithValue("@supplier_id", ReceivedExpiringItem.SupplierId);
                            comm.Parameters.AddWithValue("@name", ReceivedExpiringItem.Name);
                            comm.Parameters.AddWithValue("@months", ReceivedExpiringItem.Months);
                            comm.Parameters.AddWithValue("@days", ReceivedExpiringItem.Days);
                            comm.Parameters.AddWithValue("@years", ReceivedExpiringItem.Years);
                            comm.Parameters.AddWithValue("@expirationdate", DateTime.Today.AddMonths(ReceivedExpiringItem.Months).AddDays(ReceivedExpiringItem.Days).AddYears(ReceivedExpiringItem.Years).ToShortDateString());
                            comm.Parameters.AddWithValue("@quantity", ReceivedExpiringItem.QuantityReceived);
                            comm.Parameters.AddWithValue("@status", "Available");
                            comm.ExecuteNonQuery();
                            comm.Parameters.Clear();
                        }

                        else
                        {

                            query = "UPDATE `expiringitem` SET `quantity` = `quantity` + @quantity WHERE `id` = @id";
                            comm = new MySqlCommand(query, connection);
                            comm.Parameters.AddWithValue("@id", ReceivedExpiringItem.StockId);
                            comm.Parameters.AddWithValue("@quantity", ReceivedExpiringItem.QuantityReceived);
                            comm.ExecuteNonQuery();
                            comm.Parameters.Clear();
                        }

                        query = "UPDATE `orderedexpiringitem` SET `status` = @status WHERE `id` = @id";
                        comm = new MySqlCommand(query, connection);
                        comm.Parameters.AddWithValue("@status", "Received");
                        comm.Parameters.AddWithValue("@id", ReceivedExpiringItem.PoId);
                        comm.ExecuteNonQuery();
                        comm.Parameters.Clear();

                        query = "INSERT `expensesalesreport` VALUES (null,@name,@price,@quantity,@subtotal,@date)";
                        comm = new MySqlCommand(query, connection);
                        comm.Parameters.AddWithValue("@name", ReceivedExpiringItem.Name);
                        comm.Parameters.AddWithValue("@price", ReceivedExpiringItem.Price);
                        comm.Parameters.AddWithValue("@quantity", ReceivedExpiringItem.QuantityReceived);
                        comm.Parameters.AddWithValue("@subtotal", ReceivedExpiringItem.Subtotal);
                        comm.Parameters.AddWithValue("@date", DateTime.Today.ToShortDateString());
                        comm.ExecuteNonQuery();
                        comm.Parameters.Clear();
                    }
                    if (ReceivedExpiringItem.Type == "OrderedNonExpiringItem")
                    {
                        query = "INSERT INTO `receivednonexpiringitem` VALUES (null,@deliverynumber,@datereceived,@orderednonexpiringitem_id,@orderednonexpiringitem_nonexpiringitem_id,@orderednonexpiringitem_nonexpiringitem_supplier_id,@quantityreceived,@price,@subtotal,@account_id)";
                        comm = new MySqlCommand(query, connection);
                        comm.Parameters.AddWithValue("@deliverynumber", deliveryno);
                        comm.Parameters.AddWithValue("@datereceived", date);
                        comm.Parameters.AddWithValue("@orderednonexpiringitem_id", ReceivedExpiringItem.PoId);
                        comm.Parameters.AddWithValue("@orderednonexpiringitem_nonexpiringitem_id", ReceivedExpiringItem.StockId);
                        comm.Parameters.AddWithValue("@orderednonexpiringitem_nonexpiringitem_supplier_id", ReceivedExpiringItem.SupplierId);
                        comm.Parameters.AddWithValue("@quantityreceived", ReceivedExpiringItem.QuantityReceived);
                        comm.Parameters.AddWithValue("@price", ReceivedExpiringItem.Price);
                        comm.Parameters.AddWithValue("@subtotal", ReceivedExpiringItem.Subtotal);
                        comm.Parameters.AddWithValue("@account_id", this.AccountId);
                        comm.ExecuteNonQuery();
                        comm.Parameters.Clear();

                        query = "UPDATE `nonexpiringitem` SET `quantity` = `quantity`+ @quantity WHERE `id`=@id";
                        comm = new MySqlCommand(query, connection);
                        comm.Parameters.AddWithValue("@quantity", ReceivedExpiringItem.QuantityReceived);
                        comm.Parameters.AddWithValue("@id", ReceivedExpiringItem.StockId);
                        comm.ExecuteNonQuery();
                        comm.Parameters.Clear();

                        query = "UPDATE `orderednonexpiringitem` SET `status` = @status WHERE `id` = @id";
                        comm = new MySqlCommand(query, connection);
                        comm.Parameters.AddWithValue("@status", "Received");
                        comm.Parameters.AddWithValue("@id", ReceivedExpiringItem.PoId);
                        comm.ExecuteNonQuery();
                        comm.Parameters.Clear();

                        query = "INSERT `expensesalesreport` VALUES (null,@name,@price,@quantity,@subtotal,@date)";
                        comm = new MySqlCommand(query, connection);
                        comm.Parameters.AddWithValue("@name", ReceivedExpiringItem.Name);
                        comm.Parameters.AddWithValue("@price", ReceivedExpiringItem.Price);
                        comm.Parameters.AddWithValue("@quantity", ReceivedExpiringItem.QuantityReceived);
                        comm.Parameters.AddWithValue("@subtotal", ReceivedExpiringItem.Subtotal);
                        comm.Parameters.AddWithValue("@date", DateTime.Today.ToShortDateString());
                        comm.ExecuteNonQuery();
                        comm.Parameters.Clear();
                    }
                }

                TransactionLog transactionLog = new TransactionLog();
                transactionLog.Description = "Received Order";

                transactionLog.Transactedin = date;
                transactionLog.Details = "Added New Delivery No.: Delivery No. " + DeliveryNo;
                transactionLog.AccountId = this.AccountId;


                query = "INSERT INTO `transactionlog` VALUES (null,@description,@transactedin,@details,@account_id)";
                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@description", transactionLog.Description);
                comm.Parameters.AddWithValue("@transactedin", transactionLog.Transactedin);
                comm.Parameters.AddWithValue("@details", transactionLog.Details);
                comm.Parameters.AddWithValue("@account_id", transactionLog.AccountId);
                comm.ExecuteNonQuery();
                MessageBox.Show("New Delivery No. Added!");
                DeliveryNo = String.Empty;
                Quantity = 0;
                Price = 0;
                TotalCost = 0;

                OrderedExpiringItems.Clear();
                ReceivedExpiringItems.Clear();
                SelectedOrderedExpiringItem = null;
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
        }

        private void printdr()
        {
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";

            connection = new MySqlConnection(connString);
            try
            {
               
                connection.Open();
                DeliveryReceipt report = new DeliveryReceipt();
                report.Load();
                CrystalReportView view = new CrystalReportView();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter();
                String query = "SELECT `name`,`quantityordered`,`price`,`quantityreceived`,`subtotal` FROM `drviewmain` WHERE `receiveditemnumber_id` = @id";
                //" WHERE `purchaseordernumber_id` = @id";
                decimal totalcost;
                MySqlCommand comm = new MySqlCommand(query, connection);
                if (SelectedReceivedExpiringItemDetail != null)
                {
                    comm.Parameters.AddWithValue("@id", SelectedReceivedExpiringItemDetail.Id);
                    da.SelectCommand = comm;
                    da.Fill(dt);
                    report.SetDataSource(dt);
                    comm.Parameters.Clear();

                    query = "SELECT SUM(`subtotal`) FROM `drviewmain` WHERE `receiveditemnumber_id` = @id";
                    comm = new MySqlCommand(query, connection);
                    comm.Parameters.AddWithValue("@id", SelectedReceivedExpiringItemDetail.Id);
                    totalcost = Convert.ToDecimal(comm.ExecuteScalar());
                    report.SetParameterValue("totalcost", totalcost);
                    comm.Parameters.Clear();



                    query = "SELECT * FROM `supplier` WHERE `id` = @id";
                    comm = new MySqlCommand(query, connection);
                    comm.Parameters.AddWithValue("@id", SelectedReceivedExpiringItemDetail.SupplierId);
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

                    //query = "SELECT * FROM `account` WHERE `id` = @id";
                    //comm = new MySqlCommand(query, connection);
                    //comm.Parameters.AddWithValue("@id", this.AccountId);
                    //reader = comm.ExecuteReader();
                    //while (reader.Read())
                    //{
                    //    report.SetParameterValue("accountname", reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString());
                    //}
                    //reader.Close();

                    report.SetParameterValue("datereceived", Convert.ToDateTime(SelectedReceivedExpiringItemDetail.DateReceived).ToShortDateString());
                    report.SetParameterValue("deliveryno", SelectedReceivedExpiringItemDetail.Id);
                    report.SetParameterValue("accountname", SelectedReceivedExpiringItemDetail.EmployeeName);
                    comm.Parameters.Clear();

                }
                if (SelectedReceivedNonExpiringItemDetail != null)
                {
                    comm.Parameters.AddWithValue("@id", SelectedReceivedNonExpiringItemDetail.Id);
                    da.SelectCommand = comm;
                    da.Fill(dt);
                    report.SetDataSource(dt);
                    comm.Parameters.Clear();

                    query = "SELECT SUM(`subtotal`) FROM `drviewmain` WHERE `receiveditemnumber_id` = @id";
                    comm = new MySqlCommand(query, connection);
                    comm.Parameters.AddWithValue("@id", SelectedReceivedNonExpiringItemDetail.Id);
                    totalcost = Convert.ToDecimal(comm.ExecuteScalar());
                    report.SetParameterValue("totalcost", totalcost);
                    comm.Parameters.Clear();



                    query = "SELECT * FROM `supplier` WHERE `id` = @id";
                    comm = new MySqlCommand(query, connection);
                    comm.Parameters.AddWithValue("@id", SelectedReceivedNonExpiringItemDetail.SupplierId);
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


                    //query = "SELECT * FROM `account` WHERE `id` = @id";
                    //comm = new MySqlCommand(query, connection);
                    //comm.Parameters.AddWithValue("@id", this.AccountId);
                    //reader = comm.ExecuteReader();
                    //while (reader.Read())
                    //{
                    //    report.SetParameterValue("accountname", reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString());
                    //}
                    //reader.Close();

                    report.SetParameterValue("datereceived", Convert.ToDateTime(SelectedReceivedNonExpiringItemDetail.DateReceived).ToShortDateString());
                    report.SetParameterValue("deliveryno", SelectedReceivedNonExpiringItemDetail.Id);
                    report.SetParameterValue("accountname", SelectedReceivedNonExpiringItemDetail.EmployeeName);   
                    comm.Parameters.Clear();
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



        

        //private void setaspending()
        //{
        //    MySqlConnection connection;
        //    String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
        //    connection = new MySqlConnection(connString);
        //    connection.Open();
        //    String query = "UPDATE `orderedexpiringitem` SET `status` = @status WHERE `id` = @id";
        //    MySqlCommand comm = new MySqlCommand(query, connection);
        //    comm.Parameters.AddWithValue("@status", "Pending");
        //    comm.Parameters.AddWithValue("@id", SelectedOrderedExpiringItem.No);
        //    comm.ExecuteNonQuery();

        //    TransactionLog transactionLog = new TransactionLog();
        //    transactionLog.Description = "Received Order";
        //    var date = DateTime.Now;
        //    date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
        //    transactionLog.Transactedin = date;
        //    transactionLog.Details = "Ordered Item:" + SelectedOrderedExpiringItem.Name + " marked as Pending";
        //    transactionLog.AccountId = this.AccountId;

        //    query = "INSERT INTO `transactionlog` VALUES (null,@description,@transactedin,@details,@account_id)";
        //    comm = new MySqlCommand(query, connection);
        //    comm.Parameters.AddWithValue("@description", transactionLog.Description);
        //    comm.Parameters.AddWithValue("@transactedin", transactionLog.Transactedin);
        //    comm.Parameters.AddWithValue("@details", transactionLog.Details);
        //    comm.Parameters.AddWithValue("@account_id", transactionLog.AccountId);
        //    comm.ExecuteNonQuery();

        //    MessageBox.Show("Ordered Item:" + SelectedOrderedExpiringItem.Name + " marked as Pending");
        //    selectsupplier();
        //}

        //private void setasreceived()
        //{
        //    MySqlConnection connection;
        //    String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
        //    connection = new MySqlConnection(connString);
        //    connection.Open();
        //    String query = "UPDATE `orderedexpiringitem` SET `status` = @status WHERE `id` = @id";
        //    MySqlCommand comm = new MySqlCommand(query, connection);
        //    comm.Parameters.AddWithValue("@status", "Received");
        //    comm.Parameters.AddWithValue("@id", SelectedOrderedExpiringItem.No);
        //    comm.ExecuteNonQuery();

        //    TransactionLog transactionLog = new TransactionLog();
        //    transactionLog.Description = "Receive Order";
        //    var date = DateTime.Now;
        //    date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
        //    transactionLog.Transactedin = date;
        //    transactionLog.Details = "Ordered Item:" + SelectedOrderedExpiringItem.Name + " marked as Received";
        //    transactionLog.AccountId = this.AccountId;

        //    query = "INSERT INTO `transactionlog` VALUES (null,@description,@transactedin,@details,@account_id)";
        //    comm = new MySqlCommand(query, connection);
        //    comm.Parameters.AddWithValue("@description", transactionLog.Description);
        //    comm.Parameters.AddWithValue("@transactedin", transactionLog.Transactedin);
        //    comm.Parameters.AddWithValue("@details", transactionLog.Details);
        //    comm.Parameters.AddWithValue("@account_id", transactionLog.AccountId);
        //    comm.ExecuteNonQuery();

        //    MessageBox.Show("Ordered Item:" + SelectedOrderedExpiringItem.Name + " marked as Received");
        //    selectsupplier();

        //}
        //private bool cansetaspending()
        //{
        //    if (SelectedOrderedExpiringItem != null)
        //        return SelectedOrderedExpiringItem.Status == "Received" && IsItemExist(SelectedOrderedExpiringItem);
        //    return false;
        //}

        //private bool cansetasreceived()
        //{
        //    if (SelectedOrderedExpiringItem != null)
        //        return SelectedOrderedExpiringItem.Status == "Pending";
        //    return false;
        //}


        public static ValidationResult ReceiveOrderValidate(object obj, ValidationContext context)
        {

            var receiveorderviewmodel = (ReceiveOrderViewModel)context.ObjectInstance;
            if (receiveorderviewmodel.SelectedOrderedExpiringItem != null)
            {
                if (receiveorderviewmodel.SelectedOrderedExpiringItem.Quantity < receiveorderviewmodel.Quantity)
                {
                    return new ValidationResult("Received Quantity is greater than Ordered Quantity",
                    new List<string> { "Quantity" });
                }
            }



            //MySqlConnection connection;
            //String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            //connection = new MySqlConnection(connString);
            //try
            //{
            //    connection.Open();
            //    String query = "SELECT COUNT(*) FROM `receiveditemnumber` WHERE `code` = @code";
            //    MySqlCommand comm = new MySqlCommand(query, connection);
            //    comm.Parameters.AddWithValue("@code", receiveorderviewmodel.DeliveryNo);
            //    int count = Convert.ToInt32(comm.ExecuteScalar());
            //    if (count > 0)
            //    {
            //        return new ValidationResult("This number already exists.",
            //        new List<string> { "DeliveryNo" });
            //    }
            //}
            //catch (MySqlException ex)
            //{
            //    MessageBox.Show(ex.Message, "MySQL ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            //finally
            //{
            //    connection.Close();
            //}

            return ValidationResult.Success;

        }
        
    }
}
