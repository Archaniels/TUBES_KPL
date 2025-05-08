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
            // BELUM SELESAI
        }

        public UserData Login(LoginRequest request)
        {
            // BELUM SELESAI
        }
    }
}
