using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (LoginWindow.AuthenticatedUser == null)
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Hide();
            }


            
            
        }

        private void AddPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            AddPasswordWindow addPasswordWindow = new AddPasswordWindow();
            addPasswordWindow.Show();
        }

        private void SearchPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            SearchPassword searchPasswordWindow = new SearchPassword();
            searchPasswordWindow.Show();
        }

        private void UpdatePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            UpdatePasswordWindow updatePasswordWindow = new UpdatePasswordWindow();
            updatePasswordWindow.Show();
        }

        private void DeletePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            DeletePasswordWindow deletePasswordWindow = new DeletePasswordWindow();
            deletePasswordWindow.Show();
        }
    }
}