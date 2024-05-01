using System;
using System.Runtime.Serialization;

namespace Matri.Model
{
    [DataContract]
    public class Request : BaseResource
    {
        [DataMember]
        public Guid To { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public bool Value { get; set; }
    }
}
