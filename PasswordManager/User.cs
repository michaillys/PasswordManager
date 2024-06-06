using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    public class User
    {
        private string Username { get; set; }
        private string PasswordHash { get; set; }

        public User(string username, string passwordHash)
        {
            this.Username = username;
            this.PasswordHash = passwordHash;
        }

        public void SaveUser(User user)
        {
            // Save the user to csv file
            this.Username = user.Username;
            this.PasswordHash = user.PasswordHash;

            string path = "users.csv";
            string[] lines = { this.Username, this.PasswordHash };
            System.IO.File.WriteAllLines(path, lines);

        }

        public bool AuthenticateUser(string username, string password)
        {
            // Authenticate the user
            string path = "users.csv";
            string[] lines = System.IO.File.ReadAllLines(path);
            if (lines[0] == username)
            {
                return lines[1] == EncryptionHelper.HashPassword(password);
            }
            return false;
        }
    }
}
