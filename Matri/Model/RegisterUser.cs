using System;
using System.Collections.Generic;
using System.Text;

namespace Matri.Model
{
    public class RegisterUser
    {
        public RegisterUser()
        {
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Gender { get; set; }
        public string Birthdate { get; set; }
        public string AffiliatePortal { get; set; }
    }
}
