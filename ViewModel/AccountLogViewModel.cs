using MySql.Data.MySqlClient;
using SmileLineDentalClinic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmileLineDentalClinic.ViewModel
{
    public class AccountLogViewModel : ValidatableModel
    {
        public ObservableCollection<AccountLog> AccountLogs { get; set; }



        public AccountLogViewModel()
        {
            AccountLogs = new ObservableCollection<AccountLog>();
            setup();
        }

        private void setup()
        {
            AccountLogs.Clear();

            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "SELECT `accountlog`.`id`,`account`.`id`,`account`.`firstname`,`account`.`middlename`,`account`.`lastname`,`accountlog`.`date_time_logged_in`,`accountlog`.`date_time_logged_out` FROM `account` INNER JOIN `accountlog` ON `account`.`id`=`accountlog`.`account_id` ORDER BY `accountlog`.`id`";
                MySqlCommand comm = new MySqlCommand(query, connection);
                MySqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    AccountLog newAccountLog = new AccountLog();
                    newAccountLog.AccountId = Convert.ToInt32(reader[1]);
                    newAccountLog.Firstname = reader[2].ToString();
                    newAccountLog.Middlename = reader[3].ToString();
                    newAccountLog.Lastname = reader[4].ToString();
                    newAccountLog.DateTimeLoggedIn = Convert.ToDateTime(reader[5]);
                    newAccountLog.DateTimeLoggedOut = Convert.ToDateTime(reader[6]);
                    AccountLogs.Add(newAccountLog);
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
    }
}
