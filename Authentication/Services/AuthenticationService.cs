using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUBES_KPL.Authentication.Config;
using TUBES_KPL.Authentication.Model;
using TUBES_KPL.Authentication.Requests;
using TUBES_KPL.Authentication.Validator;

namespace TUBES_KPL.Authentication.Services
{
    public class AuthenticationService
    {
        private readonly IDictionary<string, UserData> _users;
        private readonly AuthenticationConfig _config;

        public AuthenticationService(AuthenticationConfig config)
        {
            _users = new Dictionary<string, UserData>();
            _config = config;
        }

        public bool Register(RegisterRequest request)
        {
            // mengecek apakah username sudah ada
            if (_users.ContainsKey(request.Username))
            {
                Console.WriteLine("Username already exists.");
                return false;
            }

            // validasi role
            if (!UserRoles.IsValid(request.Role))
            {
                Console.WriteLine("Invalid role specified.");
                return false;
            }

            // BELUM SELESAI!!!
        }

        public UserData Login(LoginRequest request)
        {
            // verify username ada

            // verify ypassword
            if (user.Password != request.Password)
            {
                Console.WriteLine("Incorrect password.");
                return null;
            }

            Console.WriteLine($"User {request.Username} logged in successfully.");
            return user;
        }
    }
}
