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
    /// Interaction logic for SearchPassword.xaml
    /// </summary>
    public partial class SearchPassword : Window
    {
        public SearchPassword()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SearchForPassword_Click(object sender, RoutedEventArgs e)
        {
            string input = txtSearch.Text;
            PasswordEntry entry = DataManager.SearchPasswordEntry(input);
            if (entry != null)
            {
                string message = $"Title: {entry.Title}\nUrl: {entry.Url}\nComment: {entry.Comment}";
                MessageBoxButton button = MessageBoxButton.OKCancel;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBoxResult result = MessageBox.Show(message, "Password Found, show password?", button, icon);
                if (result == MessageBoxResult.OK)
                {
                    MessageBox.Show("Password: " + EncryptionHelper.DecryptString(entry.encPassword));
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    // close the window
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Password not found");
            }
        }

        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            //Display all password entries in txt.Block
            List<PasswordEntry> entries = DataManager.GetAllPasswordEntries();
            StringBuilder sb = new StringBuilder();
            foreach (PasswordEntry entry in entries)
            {
                sb.AppendLine($"Title: {entry.Title}\nUrl: {entry.Url}\nComment: {entry.Comment}\n");
            }
            txtBlock.Text = sb.ToString();
        }
    }
}
