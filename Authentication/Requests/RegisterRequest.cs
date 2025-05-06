using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUBES_KPL.Authentication.Config;

namespace TUBES_KPL.Authentication.Requests
{
    class RegisterRequest
    {
        public string Username { get; init; }
        public string Password { get; init; }
        public string Email { get; init; }
        public UserRoles Role { get; init; }
    }
}
