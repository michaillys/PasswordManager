using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;

namespace PasswordManager
{
    public static class DataManager
    {
        private static string filePath = "passwords.txt";
        private static List<PasswordEntry> PasswordEntries = new List<PasswordEntry>();

        public static string FilePath
        {
            get { return filePath; }
            private set { filePath = value; }
        }

        public static void SetFilePathBasedOnUserName(string userName)
        {
            FilePath = $"{userName}_passwords.csv";
        }

        public static void Initialize()
        {
            if (File.Exists(filePath))
            {
                string encryptedContent = File.ReadAllText(filePath);
                string decryptedContent = EncryptionHelper.DecryptString(encryptedContent);
                PasswordEntries = decryptedContent.Split('\n')
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .Select(line => line.Split(','))
                    .Select(parts => new PasswordEntry
                    {
                        Title = parts[0],
                        encPassword = parts[1],
                        Url = parts[2],
                        Comment = parts[3]
                    })
                    .ToList();
            }
            else
            {
                File.Create(filePath).Close();
            }
        }

        public static void Save()
        {
            string content = string.Join("\n", PasswordEntries.Select(e => $"{e.Title},{e.encPassword},{e.Url},{e.Comment}"));
            string encryptedContent = EncryptionHelper.EncryptString(content);
            File.WriteAllText(filePath, encryptedContent);
        }

        public static void AddPasswordEntry(PasswordEntry entry)
        {
            PasswordEntries.Add(entry);
            Save();
        }

        public static PasswordEntry SearchPasswordEntry(string title)
        {
            return PasswordEntries.FirstOrDefault(e => e.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        public static List<PasswordEntry> GetAllPasswordEntries()
        {
            //return PasswordEntries Title, Url, Comment but not the password
            
            return PasswordEntries.Select(e => new PasswordEntry
            {
                Title = e.Title,
                Url = e.Url,
                Comment = e.Comment
            }).ToList();


        }

        public static void UpdatePasswordEntry(string title, PasswordEntry updatedEntry)
        {
            var entry = PasswordEntries.FirstOrDefault(e => e.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (entry != null)
            {
                entry.encPassword = updatedEntry.encPassword;
                entry.Url = updatedEntry.Url;
                entry.Comment = updatedEntry.Comment;
                Save();
            }
        }

        public static void DeletePasswordEntry(string title)
        {
            var entry = PasswordEntries.FirstOrDefault(e => e.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (entry != null)
            {
                PasswordEntries.Remove(entry);
                Save();
            }
        }
    }
}
