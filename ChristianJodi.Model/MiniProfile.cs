using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Matri.Model
{
    public class MiniProfile
    {
        public MiniProfile()
        {
            ThumbNails = new List<FileMetadata>();
        }

        public string Religion { get; set; }

        public string Height { get; set; }

        public string FirstName { get; set; }

        public string MotherTongue { get; set; }

        public string BirthDate { get; set; }

        public string Education { get; set; }

        public string Job { get; set; } 

        public Guid Id { get; set; }

        public IList<FileMetadata> Photos { get; set; }

        public string LastActiveOn { get; set; }

        public int Number { get; set; }

        [DataMember]
        public string WebsiteIdentifier { get; set; }

        [DataMember]
        public IList<FileMetadata> ThumbNails { get; set; }

        [DataMember]
        public string Caste { get; set; }

        [DataMember]
        public string Age { get; set; }

        [DataMember]
        public Uri ThumbNailUri { get; set; }

    }
}
