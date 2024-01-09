using System;
using System.Runtime.Serialization;

namespace ChristianJodi.CustomExceptions
{
    [DataContract]
    public class ChristianJodiInternetException : Exception
    {
        [DataMember]
        public string Message { get; set; }
    }
}

