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
    /// <summary>
    /// Interaction logic for UpdatePasswordWindow.xaml
    /// </summary>
    public partial class UpdatePasswordWindow : Window
    {
        public UpdatePasswordWindow()
        {
            InitializeComponent();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string input = txtBox.Text;
            PasswordEntry entry = DataManager.SearchPasswordEntry(input);
            if (entry != null)
            {
                string message = $"Title: {entry.Title}\nUrl: {entry.Url}\nComment: {entry.Comment}";
                MessageBoxButton button = MessageBoxButton.OKCancel;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBoxResult result = MessageBox.Show(message, "Password Found, update password?", button, icon);
                if (result == MessageBoxResult.OK)
                {
                    txtUrl.Text = entry.Url;
                }
                else if (result == MessageBoxResult.Cancel)
                {                    
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Password not found");
            }

        }

        private void UpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Text == "")
            {
                MessageBox.Show("Password cannot be empty");
                return;
            }


            //update password
            string input = txtBox.Text;
            PasswordEntry entry = DataManager.SearchPasswordEntry(input);
            if (entry != null)
            {
                entry.encPassword = EncryptionHelper.EncryptString(txtPassword.Text);
                DataManager.Save();
                MessageBox.Show("Password updated");
            }
            else
            {
                MessageBox.Show("Password not found");
            }
        }
    }
}
