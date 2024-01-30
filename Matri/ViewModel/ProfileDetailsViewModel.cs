using Matri.Business;
using Matri.CustomExceptions;
using Matri.FontsAwesome;
using Matri.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using Newtonsoft.Json;
using Syncfusion.Maui.Carousel;
using System.Collections.ObjectModel;

namespace Matri.ViewModel
{
    public partial class ProfileDetailsViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject, IQueryAttributable
    {
        IServiceManager _serviceManager;

        private List<CarouselModel> _profilePhotos = new List<CarouselModel>();
        public ProfileDetailsViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            
        }

        [ObservableProperty]
        public Guid profileIdentifier;
        [ObservableProperty]
        public string websiteIdentifier;

        [ObservableProperty]
        public string aboutMe;
        [ObservableProperty]
        public string aboutPartner;
        [ObservableProperty]
        public string casteCriteria;
        [ObservableProperty]
        public string denominationCriteria;

        [ObservableProperty]
        public string firstName;
        [ObservableProperty]
        public string lastName;
        [ObservableProperty]
        public string birthDate;
        [ObservableProperty]
        public string age;
        [ObservableProperty]
        public string motherTongue;
        [ObservableProperty]
        public string maritalStatus;
        [ObservableProperty]
        public string profileCreatedBy;
        [ObservableProperty]
        public string lastLoggedIn;
        [ObservableProperty]
        public bool showHinduFields;

        [ObservableProperty]
        public string height;
        [ObservableProperty]
        public int weight;
        [ObservableProperty]
        public string bloodGroup;
        [ObservableProperty]
        public string physicalStatus;
        [ObservableProperty]
        public string bodyType;
        [ObservableProperty]
        public string complexion;

        [ObservableProperty]
        public string education;
        [ObservableProperty]
        public string incomeDetails;
        [ObservableProperty]
        public string propertyDetails;
        [ObservableProperty]
        public string employmentDetails;
        [ObservableProperty]
        public string annualIncome;
        [ObservableProperty]
        public string job;
        [ObservableProperty]
        public string jobType;
        [ObservableProperty]
        public string educationDetails;

        [ObservableProperty]
        public string diet;
        [ObservableProperty]
        public string smoke;
        [ObservableProperty]
        public string drink;

        [ObservableProperty]
        public string familyType;
        [ObservableProperty]
        public string familyValue;
        [ObservableProperty]
        public string familyStatus;
        [ObservableProperty]
        public string familyDetails;
        [ObservableProperty]
        public string brothersCount;
        [ObservableProperty]
        public string sistersCount;
        [ObservableProperty]
        public string aboutSisters;
        [ObservableProperty]
        public string aboutBrothers;
        [ObservableProperty]
        public string brothersMarried;
        [ObservableProperty]
        public string sistersMarried;
        [ObservableProperty]
        public string mothersOccupation;
        [ObservableProperty]
        public string fathersOccupation;

        [ObservableProperty]
        public string caste;
        [ObservableProperty]
        public string denomination;
        [ObservableProperty]
        public string religion;
        [ObservableProperty]
        public string birthPlace;
        [ObservableProperty]
        public string birthTime;
        [ObservableProperty]
        public string casteInformation;
        [ObservableProperty]
        public string chevvaiDosham;
        [ObservableProperty]
        public string gothram;
        [ObservableProperty]
        public string raasi;
        [ObservableProperty]
        public string star;
        [ObservableProperty]
        public string subCaste;
        [ObservableProperty]
        public string community;

        [ObservableProperty]
        public string nationality;
        [ObservableProperty]
        public string country;
        [ObservableProperty]
        public string state;
        [ObservableProperty]
        public string stateOfResidence;
        [ObservableProperty]
        public string city;
        [ObservableProperty]
        public string nativity;
        [ObservableProperty]
        public string residingTown;

        [ObservableProperty]
        public string likeIcon = FontAwesomeIcons.ThumbsUp;
        [ObservableProperty]
        public string blockIcon = FontAwesomeIcons.Ban;

        [ObservableProperty]
        public bool liked = false;
        [ObservableProperty]
        public bool blocked = false;

        public List<CarouselModel> ProfilePhotos
        {
            get { return _profilePhotos; }
            set { _profilePhotos = value; }
        }

        public async Task GetProfileDetails(Guid userToken, Guid profileToken)
        {
            var sessionToken = await SecureStorage.GetAsync("Token");
            var showHinduFields = await SecureStorage.GetAsync("ShowHinduFields");

            ShowHinduFields = Convert.ToBoolean(showHinduFields);

            var profileDetails = await _serviceManager.GetProfileById(userToken, profileToken);
            ProfilePhotos.Clear();

            foreach (var pt in profileDetails.Photos)
            {
                ProfilePhotos.Add(new CarouselModel( pt.Name ));
            }
            
            InitialiseAddress(profileDetails);
            InitialiseBasicDetails(profileDetails);
            InitialiseBreadAndButter(profileDetails);
            InitialiseDiet(profileDetails);
            InitialiseFamilyDetails(profileDetails);
            InitialisePartnerDetails(profileDetails);
            InitialisePhysical(profileDetails);
            InitialiseReligion(profileDetails);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var queryParam = query[nameof(ProfileDetailsInput)] as ProfileDetailsInput;
            var targetId = queryParam.TargetProfileId;
            var sourceId = queryParam.LoggedInId;
            Task.Run(() => this.GetProfileDetails(sourceId, targetId)).Wait();
        }

        public void InitialisePartnerDetails(Model.Profile profileDetails)
        {
            AboutPartner = profileDetails.AboutPartner;
            CasteCriteria = profileDetails.CasteCriteria == true ? "Mandatory" : "Not Mandatory";
            DenominationCriteria = profileDetails.DenominationCriteria == true ? "Mandatory" : "Not Mandatory";
        }

        public void InitialiseBasicDetails(Model.Profile profileDetails)
        {
            FirstName = $"{profileDetails.FirstName} {profileDetails.LastName}";
            WebsiteIdentifier = $"{profileDetails.WebsiteIdentifier}{profileDetails.Number}";
            Age = profileDetails.Age;
            BirthDate = profileDetails.BirthDate;
            MotherTongue = profileDetails.MotherTongue;
            ProfileCreatedBy = profileDetails.ProfileCreatedBy;
            LastLoggedIn = profileDetails.LastActiveOn;
        }

        public void InitialiseReligion(Model.Profile profileDetails)
        {
            Religion = profileDetails.Religion;
            Denomination = profileDetails.Denomination;
            BirthPlace = profileDetails.BirthPlace;
            BirthTime = profileDetails.BirthPlace;
            Caste = profileDetails.Caste;
            CasteInformation = profileDetails.CasteInformation;
            ChevvaiDosham = profileDetails.ChevvaiDosham;
            Gothram = profileDetails.Gothram;
            Raasi = profileDetails.Raasi;
            Star = profileDetails.Raasi;
            SubCaste = profileDetails.SubCaste;
            Community = profileDetails.Community;
        }

        public void InitialisePhysical(Model.Profile profileDetails)
        {
            AboutMe = profileDetails.AboutMe;
            Height = profileDetails.Height;
            Weight = profileDetails.Weight;
            BloodGroup = profileDetails.BloodGroup;
            PhysicalStatus = profileDetails.PhysicalStatus;
            BodyType = profileDetails.BodyType;
            Complexion = profileDetails.Complexion;
        }

        public void InitialiseBreadAndButter(Model.Profile profileDetails)
        {
            Education = profileDetails.Education;
            EducationDetails = profileDetails.EducationDetails;
            AnnualIncome = profileDetails.AnnualIncome;
            JobType = profileDetails.JobType;
            Job = profileDetails.Job;
            EmploymentDetails = profileDetails.EmploymentDetails;
            IncomeDetails = profileDetails.IncomeDetails;
            PropertyDetails = profileDetails.PropertyDetails;
        }

        public void InitialiseFamilyDetails(Model.Profile profileDetails)
        {
            FamilyType = profileDetails.FamilyType;
            FamilyValue = profileDetails.FamilyValue;
            FamilyStatus = profileDetails.FamilyStatus;
            FamilyDetails = profileDetails.FamilyDetails;
            BrothersCount = profileDetails.Brothers.ToString();
            SistersCount = profileDetails.Sisters.ToString();
            AboutSisters = profileDetails.AboutSisters;
            AboutBrothers = profileDetails.AboutBrothers;
            BrothersMarried = profileDetails.BrothersMarried.ToString();
            SistersMarried = profileDetails.SistersMarried.ToString();
            MothersOccupation = profileDetails.MothersOccupation;
            FathersOccupation = profileDetails.FathersOccupation;
        }

        public void InitialiseDiet(Model.Profile profileDetails)
        {
            Diet = profileDetails.Diet;
            Smoke = profileDetails.Smoke;
            Drink = profileDetails.Drink;
        }

        public void InitialiseAddress(Model.Profile profileDetails)
        {
            Nationality = profileDetails.Nationality;
            Country = profileDetails.Country;
            State = profileDetails.State;
            StateOfResidence = profileDetails.StateOfResidence;
            City = profileDetails.City;
            Nativity = profileDetails.Nativity;
            ResidingTown = profileDetails.ResidingTown;
        }

        [RelayCommand]
        public void Like()
        {
            Task.Run(async () => { await LikeCommandAsync(); });
        }

        private async Task LikeCommandAsync()
        {
            Liked = !Liked;

            if (Liked)
            {
                Blocked = false;
            }
            var sessionToken = await SecureStorage.GetAsync("Token");
            var request = new Request { To = ProfileIdentifier, Type = RequestAction.Liked.ToString() };
            await Mark(sessionToken, request);
        }

        [RelayCommand]
        public void Block()
        {
            Task.Run(async () => { await BlockCommandAsync(); });
        }

        private async Task BlockCommandAsync()
        {
            Blocked = !Blocked;

            if (Blocked)
            {
                Liked = false;
            }
            var sessionToken = await SecureStorage.GetAsync("Token");
            var request = new Request { To = ProfileIdentifier, Type = RequestAction.Block.ToString() };
            await Mark(sessionToken, request);
        }

        private async Task Mark(string sessionToken, Request request)
        {
            try
            {
                await _serviceManager.MarkProfile(new Guid(sessionToken), request);
            }
            catch (MatriInternetException exception)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", exception.Message, "OK");
            }
            catch (Exception exception)
            {
                var jsonResponse = exception.Message;
                var errorMessage = JsonConvert.DeserializeObject<MatriException>(jsonResponse);
                await Shell.Current.CurrentPage.DisplayAlert("Alert", errorMessage.Message, "OK");
            }
        }
    }
}

