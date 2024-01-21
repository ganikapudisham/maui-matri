using System;
using System.Runtime.Serialization;

namespace ChristianJodi.CustomExceptions
{
   [DataContract]
    public class ChristianJodiException
    {
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string Error { get; set; }
    }
}

