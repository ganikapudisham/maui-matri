using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Matri.Model
{
    [DataContract]
    public class MDD
    {
        public MDD()
        {
            MaritalStatuses = new List<Master>();
            Heights = new List<Master>();
            Languages = new List<Master>();
            IndianStates = new List<Master>();
            Academics = new List<Master>();
            Jobs = new List<Master>();
            Countries = new List<Master>();
            Religions = new List<Master>();
            Castes = new List<Master>();
            Denominations = new List<Master>();
            PhysicalStatuses = new List<Master>();
            BodyTypes = new List<Master>();
            Complexions = new List<Master>();
            JobTypes = new List<Master>();
            Incomes = new List<Master>();
            ProfileCreatedBy = new List<Master>();
        }

        [DataMember]
        public IList<Master> MaritalStatuses { get; set; }

        [DataMember]
        public IList<Master> Heights { get; set; }

        [DataMember]
        public IList<Master> Languages { get; set; }

        [DataMember]
        public IList<Master> IndianStates { get; set; }

        [DataMember]
        public IList<Master> Academics { get; set; }

        [DataMember]
        public IList<Master> Jobs { get; set; }

        [DataMember]
        public IList<Master> Countries { get; set; }

        [DataMember]
        public IList<Master> Religions { get; set; }

        [DataMember]
        public IList<Master> Castes { get; set; }

        [DataMember]
        public IList<Master> Denominations { get; set; }

        [DataMember]
        public IList<Master> PhysicalStatuses { get; set; }

        [DataMember]
        public IList<Master> BodyTypes { get; set; }

        [DataMember]
        public IList<Master> Complexions { get; set; }

        [DataMember]
        public IList<Master> JobTypes { get; set; }

        [DataMember]
        public IList<Master> Incomes { get; set; }

        [DataMember]
        public IList<Master> ProfileCreatedBy { get; set; }
    }
}
