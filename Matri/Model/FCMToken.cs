using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Matri.Model
{
    public class FCMToken
    {
        public FCMToken()
        {
        }

        public string Token { get; set; }

        public string CreatedOn { get; set; }

        public string LastUsedOn { get; set; }

    }
}
