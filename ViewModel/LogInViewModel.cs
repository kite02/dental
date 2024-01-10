using GalaSoft.MvvmLight.Command;
using MySql.Data.MySqlClient;
using SmileLineDentalClinic.Model;
using SmileLineDentalClinic.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SmileLineDentalClinic.ViewModel
{
    public class LogInViewModel :  ValidatableModel
    { 


        public User User
        {
            get;
            set;
        }

        
        public RelayCommand<object> Login
        {
            get;
            set;
        }

        public RelayCommand Logout
        {
            get;
            set;
        }


        public LogInViewModel()
        {
        
            User = new User();
            Login = new RelayCommand<object>(param => login(param));
            Logout = new RelayCommand(logout);
            
        }

        private void logout()
        {
            Application.Current.MainWindow.Close();
        }

        public void login(object param)
        {
        
            string username = User.Username;
            var passwordBox = param as PasswordBox;


            MySqlConnection connection;
            String connString = "server=localhost;uid=root;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
           
            String query = "SELECT *   FROM `account` WHERE `username` = @username";
            MySqlCommand comm = new MySqlCommand(query, connection);
            comm.Parameters.AddWithValue("@username", User.Username);
            MySqlDataReader reader = comm.ExecuteReader();
            bool isFound = false;
            while (reader.Read())
            {
                
                if (reader["status"].ToString().Equals("Active") && reader["account_type"].ToString().Equals("Dentist") && PasswordStorage.VerifyPassword(passwordBox.Password, reader["password"].ToString()) == true)
                {
        
                    isFound = true;
                    User.Username = String.Empty;
                    passwordBox.Password = String.Empty;
                    Application.Current.MainWindow.Hide();
                    MainMenuView window = new MainMenuView();
                    Console.WriteLine(reader["id"].ToString());
                    MainMenuViewModel viewModel = new MainMenuViewModel(Convert.ToInt32(reader["id"]));
                    window.DataContext = viewModel;
                    window.ShowDialog();
                    Application.Current.MainWindow.ShowDialog();

                }
                if (reader["status"].ToString().Equals("Active") && reader["account_type"].ToString().Equals("Staff") && PasswordStorage.VerifyPassword(passwordBox.Password, reader["password"].ToString()) == true)
                {
                    isFound = true;
                    User.Username = String.Empty;
                    passwordBox.Password = String.Empty;
                    Application.Current.MainWindow.Hide();
                    StaffMainMenuView window = new StaffMainMenuView();
                    Console.WriteLine(reader["id"].ToString());
                    MainMenuViewModel viewModel = new MainMenuViewModel(Convert.ToInt32(reader["id"]));
                    window.DataContext = viewModel;
                    window.ShowDialog();
                    Application.Current.MainWindow.ShowDialog();

                }
            }
            
            if (isFound == false)
            {
        
                MessageBox.Show("Incorrect Username or password,please try again");
                User.Username = "";
                passwordBox.Password = "";
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        

    }
}
