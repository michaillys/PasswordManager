using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.IO;

namespace PasswordManager
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {

            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            //check if user exists
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Prašome įvesti vartotojo vardą ir slaptažodį");
                return;
            }

            if (File.Exists("users.csv"))
            {
                string[] lines = System.IO.File.ReadAllLines("users.csv");
                if (lines[0] == username)
                {
                    MessageBox.Show("Vartotojas su tokiu vardu jau egzistuoja");
                    return;
                }
            }

            string passwordHash = EncryptionHelper.HashPassword(password);

            User user = new User(username, passwordHash);
            user.SaveUser(user);                       

            DataManager.SetFilePathBasedOnUserName(username);
            //create file
            File.Create(DataManager.FilePath).Close();
            //EncryptionHelper.EncryptString(DataManager.FilePath);
            //EncryptionHelper.EncryptFile(DataManager.FilePath, password);



            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        
    }
}

