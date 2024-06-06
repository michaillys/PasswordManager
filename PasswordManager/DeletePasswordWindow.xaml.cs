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
    /// Interaction logic for DeletePasswordWindow.xaml
    /// </summary>
    public partial class DeletePasswordWindow : Window
    {
        public DeletePasswordWindow()
        {
            InitializeComponent();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string input = txtDeleteBox.Text;
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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string input = txtDeleteBox.Text;
            PasswordEntry entry = DataManager.SearchPasswordEntry(input);
            if (entry != null)
            {
                DataManager.DeletePasswordEntry(entry.Title);
                MessageBox.Show("Password deleted");
                this.Close();
            }
            else
            {
                MessageBox.Show("Password not found");
            }

        }
    }
}
