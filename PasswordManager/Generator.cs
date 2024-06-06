using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordManager
{
    public static class PasswordGenerator
    {
        private static readonly Random random = new Random();

        public static string Generate(int length = 12)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

    public static class ClipboardHelper
    {
        public static void CopyToClipboard(string text)
        {
            Clipboard.SetText(text);
        }
    }
}
