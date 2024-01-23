using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Matri.Model
{
    [DataContract]
    public class Settings
    {
        [DataMember]
        public string AppVersionUser { get; set; }
        public string AppVersionLatest { get; set; }
    }
}
