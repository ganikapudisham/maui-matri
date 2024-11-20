using System;
using System.Runtime.Serialization;

namespace Matri.CustomExceptions
{
    [DataContract]
    public class MatriInternetException : Exception
    {
        [DataMember]
        public string Message { get; set; }
    }
}

