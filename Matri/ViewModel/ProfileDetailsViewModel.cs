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
using Matri.Abstract;
using Matri.Helper;

namespace Matri.ViewModel
{
    public partial class ProfileDetailsViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject, IQueryAttributable
    {
        IServiceManager _serviceManager;
        ISharedService _sharedService;
        private string _deviceToken;

        private List<CarouselModel> _profilePhotos = new List<CarouselModel>();
        public ProfileDetailsViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            _sharedService = ServiceHelper.GetService<ISharedService>();

            if (Preferences.ContainsKey("DeviceToken"))
            {
                _deviceToken = Preferences.Get("DeviceToken", "");
            }
        }

        [ObservableProperty]
        public Guid profileIdentifier;
        [ObservableProperty]
        public string websiteIdentifier;

        [ObservableProperty]
        public string aboutMe;


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

        [ObservableProperty]
        public bool isBusy = true;

        /* Expectations */

        [ObservableProperty]
        public string expectedMaritalStatuses;

        [ObservableProperty]
        public string expectedAge;

        [ObservableProperty]
        public string expectedHeight;

        [ObservableProperty]
        public string expectedEducations;

        [ObservableProperty]
        public string expectedJobTypes;

        [ObservableProperty]
        public string expectedPhysicalStatuses;

        [ObservableProperty]
        public string expectedLanguages;

        [ObservableProperty]
        public string sameReligion;

        [ObservableProperty]
        public string sameCaste;

        [ObservableProperty]
        public string sameDenomination;

        [ObservableProperty]
        public string aboutPartner;

        /* */

        [ObservableProperty]
        public Profile profile;

        public List<CarouselModel> ProfilePhotos
        {
            get { return _profilePhotos; }
            set { _profilePhotos = value; OnPropertyChanged(nameof(ProfilePhotos)); }
        }

        public async Task GetProfileDetails(Guid userToken, Guid profileToken)
        {
            IsBusy = true;
            var showHinduFields = _sharedService.GetBool("ShowHinduFields");

            ShowHinduFields = Convert.ToBoolean(showHinduFields);

            var profileDetails = await _serviceManager.GetProfileById(userToken, profileToken);

            ProfilePhotos = new List<CarouselModel>();

            foreach (var pt in profileDetails.Photos)
            {
                ProfilePhotos.Add(new CarouselModel(pt.Name));
            }

            InitialiseAddress(profileDetails);
            InitialiseBasicDetails(profileDetails);
            InitialiseBreadAndButter(profileDetails);
            InitialiseFamilyDetails(profileDetails);
            InitialiseExpectationDetails(profileDetails);
            InitialisePhysical(profileDetails);
            InitialiseReligion(profileDetails);
            IsBusy = false;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var queryParam = query[nameof(ProfileDetailsInput)] as ProfileDetailsInput;
            var targetId = queryParam.TargetProfileId;
            var sourceId = queryParam.LoggedInId;
            ProfileIdentifier = targetId;
            Blocked = queryParam.Blocked;
            Liked = queryParam.Favourite;

            Task.Run(() => this.GetProfileDetails(sourceId, targetId));
        }

        public void InitialiseExpectationDetails(Model.Profile profileDetails)
        {
            var md = _sharedService.GetValue<MDD>("MasterData");

            SameReligion = profileDetails.Expectations.SameReligion == true ? "Yes" : "No";
            SameCaste = profileDetails.Expectations.SameCaste == true ? "Yes" : "No";
            SameDenomination = profileDetails.Expectations.SameDenomination == true ? "Yes" : "No";
            AboutPartner = profileDetails.Expectations.Expectations;

            ExpectedAge = $"{profileDetails.Expectations.AgeFrom} - {profileDetails.Expectations.AgeTo}";

            var htFrom = md.Heights.ToList().Find(x => x.Id == profileDetails.Expectations.HeightFrom);
            var htTo = md.Heights.ToList().Find(x => x.Id == profileDetails.Expectations.HeightTo);

            var bLimit = "";
            var tLimit = "";

            if (htFrom != null)
            {
                bLimit = htFrom.Name.Split('-')[0];
            }

            if (htTo != null)
            {
                tLimit = htTo.Name.Split('-')[0];
            }

            ExpectedHeight = $"{bLimit} - {tLimit}";

            string edus = "";
            foreach (var i in profileDetails.Expectations.Educations)
            {
                edus += i.Name + ", ";
            }
            ExpectedEducations = edus.TrimEnd(' ').TrimEnd(',');

            string jobTs = "";
            foreach (var i in profileDetails.Expectations.JobTypes)
            {
                jobTs += i.Name + ", ";
            }
            ExpectedJobTypes = jobTs.TrimEnd(' ').TrimEnd(',');

            string langs = "";
            foreach (var i in profileDetails.Expectations.Languages)
            {
                langs += i.Name + ", ";
            }
            ExpectedLanguages = langs.TrimEnd(' ').TrimEnd(',');

            string ps = "";
            foreach (var i in profileDetails.Expectations.PhysicalStatuses)
            {
                ps += i.Name + ", ";
            }
            ExpectedPhysicalStatuses = ps.TrimEnd(' ').TrimEnd(',');

            string ms = "";
            foreach (var i in profileDetails.Expectations.MaritalStatus)
            {
                ms += i.Name + ", ";
            }
            ExpectedMaritalStatuses = ms.TrimEnd(' ').TrimEnd(',');
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
        public async Task Like()
        {
            Liked = !Liked;

            if (Liked)
            {
                Blocked = false;
            }

            var sessionToken = await SecureStorage.GetAsync("Token");
            var request = new Request { To = ProfileIdentifier, Type = RequestAction.Favourite.ToString(), Value = Liked };
            await Mark(sessionToken, request);
        }


        [RelayCommand]
        public async Task Block()
        {
            Blocked = !Blocked;

            if (Blocked)
            {
                Liked = false;
            }

            var sessionToken = await SecureStorage.GetAsync("Token");
            var request = new Request { To = ProfileIdentifier, Type = RequestAction.Block.ToString(), Value = Blocked };
            await Mark(sessionToken, request);
        }


        private async Task Mark(string sessionToken, Request request)
        {
            IsBusy = true;
            try
            {
                var marked = await _serviceManager.MarkProfile(new Guid(sessionToken), request);

                var message = string.Empty;
                if (!marked)
                {
                    IsBusy = false;

                    //if (request.Type == RequestAction.Favourite.ToString())
                    //{
                    //    message = "Profile already added to favourites list";
                    //}
                    //else if (request.Type == RequestAction.Block.ToString())
                    //{
                    //    message = "Profile already blocked";
                    //}
                    if (request.Type == RequestAction.SendInterest.ToString())
                    {
                        message = "Interest already sent";
                    }
                    else if (request.Type == RequestAction.RequestPhoto.ToString())
                    {
                        message = "Photo already requested";
                    }

                    await Shell.Current.CurrentPage.DisplayAlert("Alert", message, "OK");
                }
                else
                {
                    Profile = _sharedService.GetValue<Profile>("LoggedInUser");
                    var recipientDeviceTokens = await _serviceManager.GetUserDeviceTokens(new Guid(sessionToken), request.To.ToString());

                    var notificationTitle = "";
                    var notificationBody = "";
                    var showNotification = false;

                    if (request.Type == RequestAction.RequestPhoto.ToString())
                    {
                        message = $"Photo request has been sent"; //TODO
                        notificationTitle = "Received Photo Request";
                        notificationBody = $"{Profile.WebsiteIdentifier}{Profile.Number} has requested you to add photo";
                        showNotification = true;
                        await Shell.Current.CurrentPage.DisplayAlert("Alert", message, "OK");
                    }
                    else if (request.Type == RequestAction.SendInterest.ToString())
                    {
                        message = $"Profile interest has been sent"; //TODO
                        notificationTitle = "Received Interest";
                        notificationBody = $"{Profile.WebsiteIdentifier}{Profile.Number} is interested in your profile";
                        showNotification = true;
                        await Shell.Current.CurrentPage.DisplayAlert("Alert", message, "OK");
                    }
                    else if (request.Type == RequestAction.Block.ToString() && request.Value)
                    {
                        message = $"Profile has been added to blocked list";
                        await Shell.Current.CurrentPage.DisplayAlert("Alert", message, "OK");
                    }
                    else if (request.Type == RequestAction.Favourite.ToString() && request.Value)
                    {
                        message = $"Profile has been added to favourites list";
                        await Shell.Current.CurrentPage.DisplayAlert("Alert", message, "OK");
                    }

                    if (showNotification && recipientDeviceTokens != null && recipientDeviceTokens.Count > 0)
                    {
                        await ServiceNotificationHelper.SendNotification(recipientDeviceTokens, notificationTitle,
                            notificationBody, Profile.ID.ToString());
                    }
                }

                IsBusy = false;
            }
            catch (MatriInternetException exception)
            {
                IsBusy = false;
                await Shell.Current.CurrentPage.DisplayAlert("Alert", exception.Message, "OK");
            }
            catch (Exception exception)
            {
                IsBusy = false;
                await Shell.Current.CurrentPage.DisplayAlert("Alert", exception.Message, "OK");
            }
        }

        [RelayCommand]
        public async Task RequestPhoto()
        {
            var sessionToken = await SecureStorage.GetAsync("Token");
            var request = new Request();
            request.To = ProfileIdentifier;
            request.Type = RequestAction.RequestPhoto.ToString();
            await Mark(sessionToken, request);
        }

        [RelayCommand]
        public async Task SendInterest()
        {
            var sessionToken = await SecureStorage.GetAsync("Token");
            var request = new Request();
            request.To = ProfileIdentifier;
            request.Type = RequestAction.SendInterest.ToString();
            await Mark(sessionToken, request);
        }
    }
}

