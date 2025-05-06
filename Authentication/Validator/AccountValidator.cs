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
                if (string.IsNullOrWhiteSpace(username) || username.Length < 3 || !Regex.IsMatch(username, @"^[a-zA-Z0-9]+$"))
                {
                    throw new ArgumentException("Username tidak bisa kosong.");
                }

                if (string.IsNullOrWhiteSpace(password) || password.Length <= 8 || !Regex.IsMatch(password, @"[A-Z]") || !Regex.IsMatch(password, @"[a-z]") || !Regex.IsMatch(password, @"\d")) {
                    throw new ArgumentException("Password harus minimal panjang 8 karakter dan memiliki paling sedikit satu huruf kapital, satu huruf kecil, and satu digit.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}
