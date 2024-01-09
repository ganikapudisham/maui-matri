using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ChristianJodi.Model
{
    public class Mini
    {
        public Mini()
        { 
        }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string WebsiteIdentifier { get; set; }

        [DataMember]
        public string ThumbNail { get; set; }

        [DataMember]
        public Guid ID { get; set; }
    }
}
