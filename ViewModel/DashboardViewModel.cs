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

namespace SmileLineDentalClinic.ViewModel
{
    public class DashboardViewModel : ValidatableModel
    {



        public RelayCommand GotoReceiveOrder { get; set; }
        public RelayCommand GotoPO { get; set; }
        public RelayCommand GotoAppointments { get; set; }
        public RelayCommand GotoBilling { get; set; }


        public ObservableCollection<ExpiryNotification> ExpiryNotifications { get; set; }
        public ObservableCollection<StocksNotification> StocksNotifications { get; set; }
        public ObservableCollection<AppointmentNotification> AppointmentNotifications { get; set; }
        public ObservableCollection<BillingNotification> BillingNotifications { get; set; }

        public string FullName { get; set; }

        public DashboardViewModel()
        {

            FullName = String.Empty;
            ExpiryNotifications = new ObservableCollection<ExpiryNotification>();
            StocksNotifications = new ObservableCollection<StocksNotification>();
            AppointmentNotifications = new ObservableCollection<AppointmentNotification>();
            BillingNotifications = new ObservableCollection<BillingNotification>();
            GotoReceiveOrder = null;
            GotoPO = null;
            GotoAppointments = null;
            GotoBilling = null;
            setup();
        }

        private void setup()
        {
            ExpiryNotifications.Clear();
            StocksNotifications.Clear();
            BillingNotifications.Clear();
            AppointmentNotifications.Clear();

            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();


                String query = "SELECT * FROM `expiringitem` WHERE status = 'Available'";
                MySqlCommand comm = new MySqlCommand(query, connection);
                MySqlDataReader reader = comm.ExecuteReader();
                ExpiringItem newExpiringItem = new ExpiringItem();
                while (reader.Read())
                {
                    newExpiringItem.Id = Convert.ToInt32(reader[0]);
                    newExpiringItem.Name = reader[2].ToString();
                    newExpiringItem.ExpirationDate = reader[6].ToString();
                    newExpiringItem.Quantity = Convert.ToInt32(reader[7]);
                    newExpiringItem.Status = reader[8].ToString();
                    if (newExpiringItem.Quantity <= 0 && newExpiringItem.Status == "Available")
                    {

                        StocksNotification StocksNotification = new StocksNotification();
                        StocksNotification.Status = "Out of Stock";
                        StocksNotification.Name = newExpiringItem.Name;
                        StocksNotification.Quantity = newExpiringItem.Quantity;
                        StocksNotifications.Add(StocksNotification);
                    }
                    else if ((DateTime.Today.Year == Convert.ToDateTime(newExpiringItem.ExpirationDate).Year) && (DateTime.Today.Month == Convert.ToDateTime(newExpiringItem.ExpirationDate).Month) && (Convert.ToDateTime(newExpiringItem.ExpirationDate).Day - DateTime.Today.Day <= 0) && (newExpiringItem.Status == "Available"))
                    {
                        ExpiryNotification ExpiryNotification = new ExpiryNotification();
                        ExpiryNotification.Status = "Expired";
                        ExpiryNotification.Name = newExpiringItem.Name;
                        ExpiryNotification.ExpirationDate = newExpiringItem.ExpirationDate;
                        ExpiryNotification.Daysleft = Convert.ToDateTime(newExpiringItem.ExpirationDate).Day - DateTime.Today.Day;
                        ExpiryNotifications.Add(ExpiryNotification);
                    }
                    else if ((DateTime.Today.Year == Convert.ToDateTime(newExpiringItem.ExpirationDate).Year) && (DateTime.Today.Month == Convert.ToDateTime(newExpiringItem.ExpirationDate).Month) && (Convert.ToDateTime(newExpiringItem.ExpirationDate).Day - DateTime.Today.Day > 0) && (Convert.ToDateTime(newExpiringItem.ExpirationDate).Day - DateTime.Today.Day <= 10) && (newExpiringItem.Status == "Available"))
                    {
                        ExpiryNotification ExpiryNotification = new ExpiryNotification();
                        ExpiryNotification.Status = "Near Expiry";
                        ExpiryNotification.Name = newExpiringItem.Name;
                        ExpiryNotification.ExpirationDate = newExpiringItem.ExpirationDate;
                        ExpiryNotification.Daysleft = Convert.ToDateTime(newExpiringItem.ExpirationDate).Day - DateTime.Today.Day;
                        ExpiryNotifications.Add(ExpiryNotification);
                    }
                    
                    else if (newExpiringItem.Quantity > 0 && newExpiringItem.Quantity < 10 && newExpiringItem.Status == "Available")
                    {
                        StocksNotification StocksNotification = new StocksNotification();
                        StocksNotification.Status = "Low Stock";
                        StocksNotification.Name = newExpiringItem.Name;
                        StocksNotification.Quantity = newExpiringItem.Quantity;
                        StocksNotifications.Add(StocksNotification);
                    }
                }
                reader.Close();
                
                query = "SELECT `patient`.`firstname`,`patient`.`middlename`,`patient`.`lastname`,`patient`.`mobilenumber`,`appointment`.`date`,`appointment`.`time`,`appointment`.`status` FROM `patient` INNER JOIN `appointment` ON `patient`.`id` = `appointment`.`patient_id` WHERE `appointment`.`status` = 'Reserved'";
                comm = new MySqlCommand(query, connection);
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    AppointmentNotification newAppointmentnotification = new AppointmentNotification();
                    newAppointmentnotification.PatientName = reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString();
                    newAppointmentnotification.MobileNumber = reader[3].ToString();
                    newAppointmentnotification.Date = reader[4].ToString();
                    newAppointmentnotification.Time = reader[5].ToString();
                    newAppointmentnotification.DaysLeft = Convert.ToInt32((Convert.ToDateTime(newAppointmentnotification.Date) - DateTime.Today).TotalDays);
                    if (newAppointmentnotification.DaysLeft > -1 && newAppointmentnotification.DaysLeft < 4)
                    {
                        AppointmentNotifications.Add(newAppointmentnotification);
                    }
                }
                reader.Close();




                List<DateTime> DateList = new List<DateTime>();
                query = "SELECT DISTINCT `datetopay` FROM `servicebilling`";
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


                        query = "SELECT `servicebilling`.`id`,`patient`.`id`,`patient`.`firstname`,`patient`.`middlename`,`patient`.`lastname`,`patient`.`mobilenumber`,`service`.`id`,`service`.`name`,`servicebilling`.`tooth`,`servicebilling`.`cost`,`servicebilling`.`additionalpay`,`servicebilling`.`amounttopay`,`servicebilling`.`balance`,`servicebilling`.`datetopay` FROM `servicebilling` INNER JOIN `patient` ON `servicebilling`.`patient_id` = `patient`.`id` INNER JOIN `service` ON `servicebilling`.`service_id` = `service`.`id` WHERE `servicebilling`.`datetopay` = @datetopay AND `servicebilling`.`amounttopay` > '0'";
                        comm = new MySqlCommand(query, connection);
                        comm.Parameters.AddWithValue("@datetopay", date.ToShortDateString());
                        reader = comm.ExecuteReader();
                        while (reader.Read())
                        {
                            BillingNotification newBillingNotification = new BillingNotification();
                            newBillingNotification.PatientName = reader[2].ToString() + " " + reader[3].ToString() + " " + reader[4].ToString();
                            newBillingNotification.MobileNumber = reader[5].ToString();
                            newBillingNotification.Name = reader[7].ToString();
                            newBillingNotification.DateToPay = reader[13].ToString();
                            newBillingNotification.AmountToPay = Convert.ToDecimal(reader[11]);
                            newBillingNotification.Balance = Convert.ToDecimal(reader[12]);
                            BillingNotifications.Add(newBillingNotification);
                        }
                        reader.Close();
                        comm.Parameters.Clear();
                    }

                }
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
