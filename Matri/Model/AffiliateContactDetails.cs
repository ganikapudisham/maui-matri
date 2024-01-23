using System.Runtime.Serialization;

namespace Matri.Model
{
    [DataContract]
    public class AffiliateContactDetails
    {
        public AffiliateContactDetails()
        {
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Telephone1 { get; set; }

        [DataMember]
        public string Telephone2 { get; set; }

        [DataMember]
        public string Landline { get; set; }

        [DataMember]
        public string Website { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Address1 { get; set; }

        [DataMember]
        public string Address2 { get; set; }

        [DataMember]
        public string Address3 { get; set; }

        [DataMember]
        public string Pincode { get; set; }

        [DataMember]
        public string Latitude { get; set; }

        [DataMember]
        public string Longitude { get; set; }

    }
}
