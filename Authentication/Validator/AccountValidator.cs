using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TUBES_KPL.Authentication.Validator
{
    class AccountValidator
    {
        public static bool ValidateAccount(string username, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    throw new ArgumentException("Username and password cannot be empty.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            if (string.IsNullOrWhiteSpace(username) || username.Length < 3 || username.Length > 20 || !Regex.IsMatch(username, @"^[a-zA-Z0-9]+$"))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < 8 || !Regex.IsMatch(password, @"[A-Z]") || !Regex.IsMatch(password, @"[a-z]") || !Regex.IsMatch(password, @"\d"))
            {
                return false;
            }

            return true;
        }
    }
}
