using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspecWeb.ViewModel
{
    public class LoginCredentials
    {
        public string Username { get; set; }

        public string Password { get; set; }
        public bool Jodjumchan { get; set; }
        public string ReturnUrl { get; set; }
    }
}
