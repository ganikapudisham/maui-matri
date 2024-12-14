using Matri.Business;
using Matri.Model;
using MvvmHelpers;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using Syncfusion.Maui.Inputs;
using Matri.Helper;
using Matri.Abstract;

namespace AdminMatri.ViewModel
{
    public partial class PdfViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        IServiceManager _serviceManager;
        ISharedService _sharedService;

        private Command<SfComboBox> onReligionChangedCommand;

        public Command<SfComboBox> OnReligionChangedCommand
        {
            get { return onReligionChangedCommand; }
            set { onReligionChangedCommand = value; OnPropertyChanged(nameof(OnReligionChanged)); }
        }

        public PdfViewModel(IServiceManager serviceManager, ISharedService sharedService)
        {
            _serviceManager = serviceManager;
            _sharedService = sharedService;
            MDMaritalStatus = new ObservableRangeCollection<Master>();
            MDHeightsFrom = new ObservableRangeCollection<Master>();
            MDHeightsTo = new ObservableRangeCollection<Master>();
            MDLanguages = new ObservableRangeCollection<Master>();
            MDStates = new ObservableRangeCollection<Master>();

            MDReligions = new ObservableRangeCollection<Master>();
            MDAcademics = new ObservableRangeCollection<Master>();
            MDJobs = new ObservableRangeCollection<Master>();
            MDCountries = new ObservableRangeCollection<Master>();

            MDDenominations = new ObservableRangeCollection<Master>();
            MDCastes = new ObservableRangeCollection<Master>();

            MDGenders = new ObservableRangeCollection<Master>();

            Init();
        }

        [ObservableProperty]
        public int ageFrom = 18;

        [ObservableProperty]
        public int ageTo = 100;

        [ObservableProperty]
        public string district;

        [ObservableProperty]
        public bool withPhoto = false;

        private ObservableRangeCollection<Master> mdMaritalStatus;
        public ObservableRangeCollection<Master> MDMaritalStatus
        {
            get { return mdMaritalStatus; }
            set
            {
                mdMaritalStatus = value;
            }
        }

        private ObservableRangeCollection<Master> mdHeightsFrom;
        public ObservableRangeCollection<Master> MDHeightsFrom
        {
            get { return mdHeightsFrom; }
            set
            {
                mdHeightsFrom = value;
            }
        }

        private ObservableRangeCollection<Master> mdHeightsTo;
        public ObservableRangeCollection<Master> MDHeightsTo
        {
            get { return mdHeightsTo; }
            set
            {
                mdHeightsTo = value;
            }
        }

        private ObservableRangeCollection<Master> mdLanguages;
        public ObservableRangeCollection<Master> MDLanguages
        {
            get { return mdLanguages; }
            set
            {
                mdLanguages = value;
            }
        }

        private ObservableRangeCollection<Master> mdStates;
        public ObservableRangeCollection<Master> MDStates
        {
            get { return mdStates; }
            set
            {
                mdStates = value;
            }
        }

        private ObservableRangeCollection<Master> mdReligions;
        public ObservableRangeCollection<Master> MDReligions
        {
            get { return mdReligions; }
            set
            {
                mdReligions = value;
            }
        }

        private ObservableRangeCollection<Master> mdAcademics;
        public ObservableRangeCollection<Master> MDAcademics
        {
            get { return mdAcademics; }
            set
            {
                mdAcademics = value;
            }
        }

        private ObservableRangeCollection<Master> mdJobs;
        public ObservableRangeCollection<Master> MDJobs
        {
            get { return mdJobs; }
            set
            {
                mdJobs = value;
            }
        }

        private ObservableRangeCollection<Master> mdCountries;
        public ObservableRangeCollection<Master> MDCountries
        {
            get { return mdCountries; }
            set
            {
                mdCountries = value;
            }
        }

        private ObservableRangeCollection<Master> mdCastes;
        public ObservableRangeCollection<Master> MDCastes
        {
            get { return mdCastes; }
            set
            {
                mdCastes = value;
            }
        }

        private ObservableRangeCollection<Master> mdDenominations;
        public ObservableRangeCollection<Master> MDDenominations
        {
            get { return mdDenominations; }
            set
            {
                mdDenominations = value;
            }
        }

        private ObservableRangeCollection<Master> mdGenders;
        public ObservableRangeCollection<Master> MDGenders
        {
            get { return mdGenders; }
            set
            {
                mdGenders = value;
            }
        }

        [ObservableProperty]
        private Master selectedHeightFrom;

        [ObservableProperty]
        private Master selectedHeightTo;

        [ObservableProperty]
        private Master selectedResidingCountry;

        [ObservableProperty]
        private Master selectedJob;

        [ObservableProperty]
        private Master selectedEducation;

        [ObservableProperty]
        private Master selectedState;

        [ObservableProperty]
        private Master selectedDenomination;

        [ObservableProperty]
        private Master selectedCaste;

        [ObservableProperty]
        private Master selectedReligion;

        [ObservableProperty]
        private Master selectedMotherTongue;

        [ObservableProperty]
        private Master selectedMaritalStatus;

        [ObservableProperty]
        public bool showDenominations = false;

        [ObservableProperty]
        public bool isBusy = false;

        [ObservableProperty]
        public Master selectedGender;

        private void Init()
        {
            var masterData = _sharedService.GetValue<MDD>("MasterData");

            MDMaritalStatus.AddRange(masterData.MaritalStatuses);
            SelectedMaritalStatus = masterData.MaritalStatuses[0];

            MDHeightsTo.AddRange(masterData.Heights);
            MDHeightsFrom.AddRange(masterData.Heights);
            SelectedHeightFrom = masterData.Heights[0];
            SelectedHeightTo = masterData.Heights[0];

            MDLanguages.AddRange(masterData.Languages);
            SelectedMotherTongue = masterData.Languages[0];

            MDReligions.AddRange(masterData.Religions);
            SelectedReligion = masterData.Religions[0];

            MDStates.AddRange(masterData.IndianStates);
            SelectedState = masterData.IndianStates[0];

            MDAcademics.AddRange(masterData.Academics);
            SelectedEducation = masterData.Academics[0];

            MDJobs.AddRange(masterData.Jobs);
            SelectedJob = masterData.Jobs[0];

            MDCountries.AddRange(masterData.Countries);
            SelectedResidingCountry = masterData.Countries[0];

            MDCastes.AddRange(masterData.Castes);
            SelectedCaste = masterData.Castes[0];

            MDDenominations.AddRange(masterData.Denominations);
            SelectedDenomination = masterData.Denominations[0];

            var genders = new List<Master>();
            genders.Add(new Master { Id = "1", Name = "Male" });
            genders.Add(new Master { Id = "0", Name = "Female" });

            MDGenders.AddRange(genders);
            SelectedGender = genders[0];

        }

        [RelayCommand]
        public async Task Search()
        {
            var sessionToken = await SecureStorage.GetAsync("Token");

            if (string.IsNullOrWhiteSpace(AgeFrom.ToString()))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please Specify From Age", "Ok");
                return;
            }
            else if (string.IsNullOrWhiteSpace(AgeTo.ToString()))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please Specify To Age", "Ok");
                return;
            }
            //else if (string.IsNullOrWhiteSpace(SelectedHeightFrom.Value.ToString()))
            //{
            //    _userDialogs.Alert("Please Specify Starting Height");
            //    return;
            //}
            //else if (string.IsNullOrWhiteSpace(HeightTo.Value.ToString()))
            //{
            //    _userDialogs.Alert("Please Specify Ending Height");
            //    return;
            //}

            var searchParameters = new SearchParameters();
            
            
            searchParameters.PageSize = 10;
            searchParameters.StartPage = 1;
            searchParameters.Gender = SelectedGender.Id;
            searchParameters.AgeFrom = AgeFrom;
            searchParameters.AgeTo = AgeTo;
            searchParameters.HeightFrom = SelectedHeightFrom.Id;
            searchParameters.HeightTo = SelectedHeightTo.Id;
            searchParameters.MaritalStatus = SelectedMaritalStatus.Id;
            searchParameters.MotherTongue = SelectedMotherTongue.Id;
            searchParameters.Religion = SelectedReligion.Id;
            searchParameters.Caste = SelectedCaste.Id;
            searchParameters.SubCaste = SelectedCaste.Id;
            searchParameters.Denomination = SelectedDenomination.Id;
            searchParameters.State = SelectedState.Id;
            searchParameters.DistrictRegion = District;
            searchParameters.Education = SelectedEducation.Id;
            searchParameters.Job = SelectedJob.Id;
            searchParameters.ResidingCountry = SelectedResidingCountry.Id;
            searchParameters.Community = "SELECT";
            searchParameters.WithPhoto = WithPhoto == true ? 1 : 0;

            var searchParams = new Dictionary<string, object> { { "SearchParameters", searchParameters } };

            IsBusy = true;
            //await Shell.Current.GoToAsync("searchresults", searchParams);
            var profileIds = await _serviceManager.GetSearchedProfileIds(sessionToken, searchParameters);

            var my10List = profileIds;

            if (profileIds.Count > 10)
            {
                my10List = profileIds.OrderBy(x => (new Random()).Next()).Take(10).ToList();
            }

            var downloadPDF = await _serviceManager.GetPDF(sessionToken, my10List);

            IsBusy = false;
        }

        private void OnReligionChanged(SfComboBox cmbReligion)
        {
            //var ComboSelectedLabel1 = $"Selected Index of the item is: {comboBox1.SelectedIndex}";
        }
    }
}

