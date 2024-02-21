using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Matri.Model
{
    public class Profile
    {
        public Profile()
        {
            Photos = new List<FileMetadata>();
            Horoscopes = new List<FileMetadata>();
            ThumbNails = new List<FileMetadata>();
            ProfilePhotos = new List<string>();
        }

        //public override string ToString()
        //{
        //    return FirstName + " " + LastName;
        //}

        public string AboutMe { get; set; }


        public string MaritalStatus { get; set; }


        public string BloodGroup { get; set; }

        public string Religion { get; set; }

        public string Height { get; set; }

        public string PhysicalStatus { get; set; }


        public string BodyType { get; set; }


        public string Complexion { get; set; }


        public string ProfileCreatedBy { get; set; }


        public int Weight { get; set; }


        public string Caste { get; set; }

        public string Diet { get; set; }


        public string Smoke { get; set; }


        public string Drink { get; set; }

        public string Nationality { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string StateOfResidence { get; set; }

        public string City { get; set; }

        public string MobileNumber { get; set; }


       // public Contact Contact { get; set; }

        public string Address { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string MotherTongue { get; set; }

        public string BirthDate { get; set; }

        public string Education { get; set; }

        public string EducationDetails { get; set; }

        public string JobType { get; set; }

        public string Job { get; set; }

        public string AnnualIncome { get; set; }

        public string EmploymentDetails { get; set; }

        public string BirthTime { get; set; }

        public string BirthPlace { get; set; }

        public string AboutBrothers { get; set; }

        public string AboutSisters { get; set; }

        public string Nativity { get; set; }

        public string WebsiteIdentifier { get; set; }

        public string SubCaste { get; set; }

        public string Star { get; set; }

        public string Raasi { get; set; }

        public string Gothram { get; set; }

        public string ChevvaiDosham { get; set; }

        public string CasteInformation { get; set; }

        public string FamilyType { get; set; }

        public string FamilyValue { get; set; }

        public string FamilyStatus { get; set; }

        public string FathersOccupation { get; set; }

        public string MothersOccupation { get; set; }

        public int Brothers { get; set; }

        public int Sisters { get; set; }

        public int BrothersMarried { get; set; }

        public int SistersMarried { get; set; }

        public string FamilyDetails { get; set; }

        public Guid ID { get; set; }

        public IList<FileMetadata> Photos { get; set; }

        public IList<string> ProfilePhotos { get; set; }

        public IList<FileMetadata> Horoscopes { get; set; }

        public string Community { get; set; }

        public string LastActiveOn { get; set; }

        public int Number { get; set; }

        public string Denomination { get; set; }

        public string IncomeDetails { get; set; }

        public string PropertyDetails { get; set; }

        public string CreatedOn { get; set; }

        public string ResidingTown { get; set; }

        public IList<FileMetadata> ThumbNails { get; set; }

        public Uri ThumbNailUri { get; set; }

        public string Age { get; set; }

        public bool Liked { get; set; }

        public bool Blocked { get; set; }

        public string EmailId { get; set; }

        [DataMember]
        public Partner Expectations { get; set; }
    }
}