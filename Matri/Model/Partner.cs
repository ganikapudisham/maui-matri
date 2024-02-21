using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Matri.Model
{
    [DataContract]
    public class Partner
    {
        public Partner()
        {
            Educations = new List<Interest>();
            JobTypes = new List<Interest>();
            MaritalStatus = new List<Interest>();
            PhysicalStatuses = new List<Interest>();
            Complexions = new List<Interest>();
            Languages = new List<Interest>();
        }

        [DataMember]
        public string AgeFrom { get; set; }

        [DataMember]
        public string AgeTo { get; set; }

        [DataMember]
        public string HeightFrom { get; set; }

        [DataMember]
        public string HeightTo { get; set; }

        [DataMember]
        public IList<Interest> Educations { get; set; }

        [DataMember]
        public IList<Interest> JobTypes { get; set; }

        [DataMember]
        public string Expectations { get; set; }

        [DataMember]
        public IList<Interest> MaritalStatus { get; set; }

        [DataMember]
        public IList<Interest> PhysicalStatuses { get; set; }

        [DataMember]
        public IList<Interest> Complexions { get; set; }

        [DataMember]
        public IList<Interest> Languages { get; set; }

        [DataMember]
        public bool SameReligion { get; set; }

        [DataMember]
        public bool SameCaste { get; set; }

        [DataMember]
        public bool SameDenomination { get; set; }

    }
}
