using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ChristianJodi.Model.Email
{
    public class ContactUs
    {
        public ContactUs()
        { 
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
