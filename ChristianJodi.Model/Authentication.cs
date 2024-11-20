using System;
using System.Collections.Generic;
using System.Text;

namespace Matri.Model
{
    public class Authentication
    {
        public Authentication()
        {
        }
        
        public string UserName { get; set; }
        
        public string Password { get; set; }
        
        public string SessionToken { get; set; }
        
        public DateTime PermanentTokenTimeStamp { get; set; }
        
        public bool IsLockedOut { get; set; }
        
        public bool IsActivated { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public int TokenExpiryDuration { get; set; }

        public string StartPageUrl { get; set; }
        
        public bool IsAdminUser { get; set; }
        
        public bool SubscriptionActive { get; set; }
        
        public int userId { get; set; }
        
        public int FailedLoginAttempts { get; set; }
        
        public int PartnerId { get; set; }
    }
}
