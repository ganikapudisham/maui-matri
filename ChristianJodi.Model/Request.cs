using System;
using System.Runtime.Serialization;

namespace ChristianJodi.Model
{
    [DataContract]
    public class Request : BaseResource
    {
        [DataMember]
        public Guid To { get; set; }

        [DataMember]
        public string Type { get; set; }
    }
}
