using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Matri.Model
{
    public class Visitor
    {
        public Visitor()
        {
        }

        [DataMember]
        public string VisitedOn { get; set; }

        [DataMember]
        public string VisitedTimes { get; set; }

        [DataMember]
        public string WebsiteIdentifier { get; set; }

        [DataMember]
        public string BirthDate { get; set; }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public IList<FileMetadata> Photos { get; set; }

        [DataMember]
        public string LastActiveOn { get; set; }

        [DataMember]
        public int Number { get; set; }

        [DataMember]
        public IList<FileMetadata> ThumbNails { get; set; }

        [DataMember]
        public string FirstName { get; set; }
    }
}
