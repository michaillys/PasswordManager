using System.Configuration;
using System.Data;
using System.Windows;

namespace PasswordManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Subscribe to the ProcessExit event
            //AppDomain.CurrentDomain.ProcessExit += new EventHandler(EncryptionHelper.OnApplicationExit);
        }
    }

}
