using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ChristianJodi.Model
{
    public class SearchParameters
    {
        public SearchParameters()
        { 
        }

        [DataMember]
        public string SubCaste { get; set; }

        [DataMember]
        public string Community { get; set; }
        public int PageSize { get; set; }
        public int StartPage { get; set; }
        public string MaritalStatus { get; set; }
        public string MotherTongue { get; set; }
        public string Religion { get; set; }
        public string Caste { get; set; }
        public string Denomination { get; set; }
        public int WithPhoto { get; set; }
        public int AgeFrom { get; set; }
        public int AgeTo { get; set; }
        public string HeightFrom { get; set; }
        public string HeightTo { get; set; }
        public string State { get; set; }
        public string DistrictRegion { get; set; }
        public string Education { get; set; }
        public string Job { get; set; }
        public string ResidingCountry { get; set; }
    }
}
