using System;
using System.Collections.Generic;
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

namespace PasswordManager
{
    public partial class LoginWindow : Window
    {
        public static User AuthenticatedUser { get; set; }
        public LoginWindow()
        {
            InitializeComponent();
        }

        public void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Prašome įvesti vartotojo vardą ir slaptažodį");
                return;
            }


            User user = new User(username, EncryptionHelper.HashPassword(password));
            if (!user.AuthenticateUser(username, password))
            {
                MessageBox.Show("Neteisingas vartotojo vardas arba slaptažodis");
                return;
            }

            AuthenticatedUser = user;

            DataManager.SetFilePathBasedOnUserName(username);
            EncryptionHelper.SetEncryptionKey(password);
            DataManager.Initialize();



            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        public void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }
    }
}