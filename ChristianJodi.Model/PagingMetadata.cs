using System;
using System.Xml.Serialization;

namespace Matri.Model
{
    public class PagingMetadata
    {
        public int TotalRecords { get; set; }

        public string FirstPageUrl { get; set; }

        public string LastPageUrl { get; set; }

        public string PreviousPageUrl { get; set; }

        public string NextPageUrl { get; set; }
    }
}
