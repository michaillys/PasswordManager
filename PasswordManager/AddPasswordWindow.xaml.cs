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
    public partial class AddPasswordWindow : Window
    {
        public AddPasswordWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text;
            string password = PasswordTextBox.Text;
            string encryptedPassword = EncryptionHelper.EncryptString(password);
            string url = UrlTextBox.Text;
            string comment = CommentTextBox.Text;

            PasswordEntry newEntry = new PasswordEntry
            {
                Title = title,
                encPassword = encryptedPassword,
                Url = url,
                Comment = comment
            };

            DataManager.AddPasswordEntry(newEntry);
            DataManager.Save();
            this.Close();
        }
    }
}
