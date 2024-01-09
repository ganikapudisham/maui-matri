using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace ChristianJodi.Model
{
    [DataContract]
    public class FileMetadata : BaseResource
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ThumbNail { get; set; }

        [DataMember]
        public bool ThumbNailAvailable { get; set; }
    }
}
