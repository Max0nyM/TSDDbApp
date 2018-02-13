using System;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TSDDBApp
{
    class Utils
    {
        public static string GetMd5Hash(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString().ToLower();
            }
        }

        public static bool VerifyMd5Hash(string input, string hash)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                // Hash the input.
                string hashOfInput = GetMd5Hash(input);
                // Create a StringComparer an compare the hashes.
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                if (0 == comparer.Compare(hashOfInput, hash))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static void Error(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Info(string info)
        {
            MessageBox.Show(info, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }




        public static bool IsValid(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

  

        public static bool CheckUnameLength(string username)
        {
            if (username.Length >= 5)
            {
                return true;
            }
            return false;

        }
    }
}
