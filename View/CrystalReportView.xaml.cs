using MySql.Data.MySqlClient;
using SmileLineDentalClinic.Reports;
using SmileLineDentalClinic.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SmileLineDentalClinic.View
{
    /// <summary>
    /// Interaction logic for CrystalReportView.xaml
    /// </summary>
    public partial class CrystalReportView : Window
    {
        public CrystalReportView()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //MySqlConnection connection;
            //String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            //connection = new MySqlConnection(connString);
            //connection.Open();
            ////String query = "SELECT `stocks`.`id`,`stocks`.`name`,`purchaseorder`.`quantity` FROM `stocks` INNER JOIN `purchaseorder` ON `stocks`.`id` = `purchaseorder`.`stocks_id`";
            //String query = "SELECT `name`,`quantityordered` FROM `poview`";
            //MySqlDataAdapter adapter = new MySqlDataAdapter();
            //adapter.SelectCommand = new MySqlCommand(query, connection);
            //DataTable datatable = new DataTable();
            //adapter.Fill(datatable);

            //PurchaseOrder purchaseOrder = new PurchaseOrder();
            //purchaseOrder.Load();
            //purchaseOrder.SetDataSource(datatable);
            //Report.ViewerCore.ReportSource = purchaseOrder;
            //purchaseOrder.Refresh();
            
            
        }
    }
}
