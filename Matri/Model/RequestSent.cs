using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;

namespace Matri.Model
{
    [DataContract]
    public class RequestSent : BaseResource
    {

        public RequestSent()
        {
            ThumbNails = new List<FileMetadata>();
        }

        [DataMember]
        public Guid ReceiverId { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string ReceiverFirstName { get; set; }

        //[DataMember]
        //public IList<FileMetadata> Photos { get; set; }

        [DataMember]
        public string SentDate { get; set; }

        [DataMember]
        public string WebsiteIdentifier { get; set; }

        [DataMember]
        public int ReceiverNumber { get; set; }

        [DataMember]
        public IList<FileMetadata> ThumbNails { get; set; }

        [DataMember]
        public Uri ThumbNailUri { get; set; }
    }
}
