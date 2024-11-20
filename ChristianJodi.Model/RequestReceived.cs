using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;

namespace Matri.Model
{
    [DataContract]
    public class RequestReceived : BaseResource
    {

        public RequestReceived()
        {
            Photos = new List<FileMetadata>();
        }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public Guid SenderId { get; set; }

        [DataMember]
        public string SenderFirstName { get; set; }

        [DataMember]
        public IList<FileMetadata> Photos { get; set; }

        [DataMember]
        public string SentDate { get; set; }

        [DataMember]
        public int SenderNumber {get; set; }

        [DataMember]
        public string WebsiteIdentifier { get; set; }

        [DataMember]
        public IList<FileMetadata> ThumbNails { get; set; }

        [DataMember]
        public Uri ThumbNailUri { get; set; }
    }
}
