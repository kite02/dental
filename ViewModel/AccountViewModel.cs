
using GalaSoft.MvvmLight.CommandWpf;
using MySql.Data.MySqlClient;

using SmileLineDentalClinic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace SmileLineDentalClinic.ViewModel
{

    public class AccountViewModel : ValidatableModel
    {

        public RelayCommand SaveUser { get; set; }
        public RelayCommand ClearText { get; set; }
        public RelayCommand SetAsActive { get; set; }
        public RelayCommand SetAsInactive { get; set; }
        public RelayCommand EditMode { get; set; }
        public RelayCommand EditAccount { get; set; }
        public RelayCommand<object> HandleGender { get; set; }
        public RelayCommand<object> HandleAccount { get; set; }
        //public RelayCommand<object> HandleStatus { get; set; }



        public ObservableCollection<User> Users { get; set; }

        public ICollectionView UsersView { get; set; }

        private string searchedtext;

        public string SearchedText
        {
            get { return searchedtext; }
            set
            {
                searchedtext = value;
                RaisePropertyChanged("SearchedText");
                UsersView.Refresh();
            }
        }

        private User selecteduser;

        public User SelectedUser
        {
            get { return selecteduser; }
            set
            {
                selecteduser = value;
                RaisePropertyChanged("SelectedUser");
            }
        }


        private string firstname;

        public string Firstname
        {
            get { return firstname; }
            set
            {
                firstname = value;
                RaisePropertyChanged("FirstName");
            }
        }

        private string middlename;

        public string Middlename
        {
            get { return middlename; }
            set
            {
                middlename = value;
                RaisePropertyChanged("Middlename");
            }
        }

        private string lastname;

        public string Lastname
        {
            get { return lastname; }
            set
            {
                lastname = value;

                RaisePropertyChanged("Lastname");
            }
        }

        private string username;

        [CustomValidation(typeof(AccountViewModel), "UsernameValidate")]
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                RaisePropertyChanged("Username");
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                RaisePropertyChanged("Password");
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





        public int AccountId { get; set; }

        public AccountViewModel()
        {

            Users = new ObservableCollection<User>();
            SelectedUser = new User();

            AccountId = 0;

            setup();
            UsersView = CollectionViewSource.GetDefaultView(Users);
            UsersView.Filter = UserFilter;
            SaveUser = new RelayCommand(saveUser, cansaveuser);
            //HandleGender = new RelayCommand<object>(param => handleGender(param));
            HandleAccount = new RelayCommand<object>(param => handleAccount(param));
            SetAsActive = new RelayCommand(setasactive, cansetasactive);
            SetAsInactive = new RelayCommand(setasinactive, cansetasinactive);
            EditMode = new RelayCommand(editmode);
            EditAccount = new RelayCommand(editaccount, caneditaccount);
            ClearText = new RelayCommand(cleartext);
            //HandleStatus = new RelayCommand<object>(param => handleStatus(param));
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(SearchedText))
                return true;
            else
                return ((item as User).Id.ToString().IndexOf(SearchedText, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as User).Firstname.IndexOf(SearchedText, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as User).Middlename.IndexOf(SearchedText, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as User).Lastname.IndexOf(SearchedText, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as User).Username.IndexOf(SearchedText, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as User).Type.IndexOf(SearchedText, StringComparison.OrdinalIgnoreCase) >= 0)
                     || ((item as User).Status.IndexOf(SearchedText, StringComparison.OrdinalIgnoreCase) >= 0);

        }

        public void setup()
        {
            Users.Clear();
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "SELECT * FROM `account`";
                MySqlCommand comm = new MySqlCommand(query, connection);
                MySqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    User newUser = new User();
                    newUser.Id = Convert.ToInt32(reader["id"]);
                    newUser.Firstname = reader["firstname"].ToString();
                    newUser.Middlename = reader["middlename"].ToString();
                    newUser.Lastname = reader["lastname"].ToString();
                    newUser.Username = reader["username"].ToString();
                    newUser.Password = reader["password"].ToString();
                    newUser.Type = reader["account_type"].ToString();
                    newUser.Status = reader["status"].ToString();
                    Users.Add(newUser);
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

        public void saveUser()
        {


            MessageBoxResult dialogResult = MessageBox.Show("Are you sure to add this new Account?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dialogResult == MessageBoxResult.Yes)
            {


                MySqlConnection connection;
                String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
                connection = new MySqlConnection(connString);
                try
                {
                    connection.Open();
                    String query = "INSERT INTO `account` VALUES (null,@firstname,@middlename,@lastname,@username,@password,@account_type,@status)";
                    MySqlCommand comm = new MySqlCommand(query, connection);
                    comm.Parameters.AddWithValue("@firstname", Firstname);
                    comm.Parameters.AddWithValue("@middlename", Middlename);
                    comm.Parameters.AddWithValue("@lastname", Lastname);
                    comm.Parameters.AddWithValue("@username", Username);
                    comm.Parameters.AddWithValue("@password", PasswordStorage.CreateHash(Password));
                    comm.Parameters.AddWithValue("@account_type", Type);
                    comm.Parameters.AddWithValue("@status", "Active");
                    comm.ExecuteNonQuery();

                    TransactionLog transactionLog = new TransactionLog();
                    transactionLog.Description = "Manage Account";
                    var date = DateTime.Now;
                    date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
                    transactionLog.Transactedin = date;
                    transactionLog.Details = "Added New Username: " + Username;
                    transactionLog.AccountId = this.AccountId;

                    query = "INSERT INTO `transactionlog` VALUES (null,@description,@transactedin,@details,@account_id)";
                    comm = new MySqlCommand(query, connection);
                    comm.Parameters.AddWithValue("@description", transactionLog.Description);
                    comm.Parameters.AddWithValue("@transactedin", transactionLog.Transactedin);
                    comm.Parameters.AddWithValue("@details", transactionLog.Details);
                    comm.Parameters.AddWithValue("@account_id", transactionLog.AccountId);
                    comm.ExecuteNonQuery();

                    MessageBox.Show("Account Added!");
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
                Firstname = String.Empty;
                Middlename = String.Empty;
                Lastname = String.Empty;
                Username = String.Empty;
                Password = String.Empty;
                Type = String.Empty;
            }
        }

        //public void handleGender(object parameter)
        //{
        //    User.Gender = (string)parameter;
        //}

        public void handleAccount(object parameter)
        {
            Type = (string)parameter;
        }

        //public void handleStatus(object parameter)
        //{
        //    User.Status = (string)parameter;
        //}


        public bool cansaveuser()
        {
            return !String.IsNullOrWhiteSpace(Firstname) && !String.IsNullOrWhiteSpace(Lastname) && !String.IsNullOrWhiteSpace(Username) && !String.IsNullOrWhiteSpace(Password) && !String.IsNullOrWhiteSpace(Type) && this.HasErrors == false;
        }

        private void cleartext()
        {
            Firstname = String.Empty;
            Middlename = String.Empty;
            Lastname = String.Empty;
            Username = String.Empty;
            Password = String.Empty;
            Type = String.Empty;
        }

        private void setasactive()
        {
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "UPDATE `account` SET `status` = @status WHERE `id` = @id";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@status", "Active");
                comm.Parameters.AddWithValue("@id", SelectedUser.Id);
                comm.ExecuteNonQuery();

                TransactionLog transactionLog = new TransactionLog();
                transactionLog.Description = "Manage Account";
                var date = DateTime.Now;
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
                transactionLog.Transactedin = date;
                transactionLog.Details = "Username:" + SelectedUser.Username + " marked as Active";
                transactionLog.AccountId = this.AccountId;

                query = "INSERT INTO `transactionlog` VALUES (null,@description,@transactedin,@details,@account_id)";
                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@description", transactionLog.Description);
                comm.Parameters.AddWithValue("@transactedin", transactionLog.Transactedin);
                comm.Parameters.AddWithValue("@details", transactionLog.Details);
                comm.Parameters.AddWithValue("@account_id", transactionLog.AccountId);
                comm.ExecuteNonQuery();

                MessageBox.Show("Username:" + SelectedUser.Username + " marked as Active");
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

        private void setasinactive()
        {
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "UPDATE `account` SET `status` = @status WHERE `id` = @id";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@status", "Inactive");
                comm.Parameters.AddWithValue("@id", SelectedUser.Id);
                comm.ExecuteNonQuery();

                TransactionLog transactionLog = new TransactionLog();
                transactionLog.Description = "Manage Account";
                var date = DateTime.Now;
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
                transactionLog.Transactedin = date;
                transactionLog.Details = "Username:" + SelectedUser.Username + " marked as Inactive";
                transactionLog.AccountId = this.AccountId;

                query = "INSERT INTO `transactionlog` VALUES (null,@description,@transactedin,@details,@account_id)";
                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@description", transactionLog.Description);
                comm.Parameters.AddWithValue("@transactedin", transactionLog.Transactedin);
                comm.Parameters.AddWithValue("@details", transactionLog.Details);
                comm.Parameters.AddWithValue("@account_id", transactionLog.AccountId);
                comm.ExecuteNonQuery();

                MessageBox.Show("Username:" + SelectedUser.Username + " marked as Inactive");
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
        private bool cansetasactive()
        {
            if (SelectedUser != null)
                return SelectedUser.Status == "Inactive";
            return false;
        }

        private bool cansetasinactive()
        {
            if (SelectedUser != null)
                return SelectedUser.Status == "Active";
            return false;
        }

        private void editmode()
        {
            if (SelectedUser != null)
            {
                Firstname = SelectedUser.Firstname;
                Middlename = SelectedUser.Middlename;
                Lastname = SelectedUser.Lastname;
                Username = SelectedUser.Username;
            }
        }

        private void editaccount()
        {
            Username = Username;
            MySqlConnection connection;
            String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
            connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                String query = "UPDATE `account` SET `firstname` = @firstname,`middlename` = @middlename,`lastname` = @lastname,`username` = @username,`password` = @password,`account_type` = @account_type WHERE `id` = @id";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@firstname", Firstname);
                comm.Parameters.AddWithValue("@middlename", Middlename);
                comm.Parameters.AddWithValue("@lastname", Lastname);
                comm.Parameters.AddWithValue("@username", Username);
                comm.Parameters.AddWithValue("@password", PasswordStorage.CreateHash(Password));
                comm.Parameters.AddWithValue("@account_type", Type);
                comm.Parameters.AddWithValue("@id", SelectedUser.Id);
                comm.ExecuteNonQuery();

                TransactionLog transactionLog = new TransactionLog();
                transactionLog.Description = "Manage Account";
                var date = DateTime.Now;
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
                transactionLog.Transactedin = date;
                transactionLog.Details = "Account Id: " + SelectedUser.Id + " is edited.";
                transactionLog.AccountId = this.AccountId;

                query = "INSERT INTO `transactionlog` VALUES (null,@description,@transactedin,@details,@account_id)";
                comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@description", transactionLog.Description);
                comm.Parameters.AddWithValue("@transactedin", transactionLog.Transactedin);
                comm.Parameters.AddWithValue("@details", transactionLog.Details);
                comm.Parameters.AddWithValue("@account_id", transactionLog.AccountId);
                comm.ExecuteNonQuery();

                MessageBox.Show("Account Id: " + SelectedUser.Id + " is edited.");
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

            Firstname = String.Empty;
            Middlename = String.Empty;
            Lastname = String.Empty;
            Username = String.Empty;
            Password = String.Empty;
            Type = String.Empty;
        }

        private bool caneditaccount()
        {
            return SelectedUser != null && this.HasErrors == false;
        }


        public static ValidationResult UsernameValidate(object obj, ValidationContext context)
        {

            var accountviewmodel = (AccountViewModel)context.ObjectInstance;
            if (accountviewmodel.Username != null)
            {
                MySqlConnection connection;
                String connString = "server=localhost;uid=root;pwd=1234;database=smilelinedentalcenter;";
                connection = new MySqlConnection(connString);
                try
                {
                    connection.Open();
                    String query = "SELECT COUNT(*) FROM `account` WHERE `username`= @username";
                    MySqlCommand comm = new MySqlCommand(query, connection);
                    comm.Parameters.AddWithValue("@username", accountviewmodel.Username);

                    int count = Convert.ToInt32(comm.ExecuteScalar());
                    if (accountviewmodel.SelectedUser != null)
                    {
                        if (count > 0 && accountviewmodel.Username != accountviewmodel.SelectedUser.Username)
                        {
                            return new ValidationResult("The username is already exists",
                            new List<string> { "Username" });
                        }
                    }
                    else
                    {
                        if (count > 0)
                        {
                            return new ValidationResult("The username is already exists",
                            new List<string> { "Username" });
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
            return ValidationResult.Success;

        }

    }
}
