using System;
using System.Runtime.Serialization;

namespace Matri.CustomExceptions
{
   [DataContract]
    public class MatriException
    {
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string Error { get; set; }
    }
}

