using Matri.Business;
using Matri.Model;
using MvvmHelpers;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using Syncfusion.Maui.Inputs;

namespace Matri.ViewModel
{
    public partial class SearchAdvancedViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        IServiceManager _serviceManager;

        private Command<SfComboBox> onReligionChangedCommand;

        public Command<SfComboBox> OnReligionChangedCommand
        {
            get { return onReligionChangedCommand; }
            set { onReligionChangedCommand = value; OnPropertyChanged(nameof(OnReligionChanged)); }
        }

        public SearchAdvancedViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
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

            Task.Run(async () => { await InitialiseMasterData(); });
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

        private async Task InitialiseMasterData()
        {
            var token = await SecureStorage.GetAsync("Token");

            var masterData = await _serviceManager.GetMasterData(new Guid(token));
            
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
        }

        [RelayCommand]
        public async Task Search()
        {
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

            var searchParamters = new SearchParameters();

            searchParamters.SubCaste = SelectedCaste.Id;
            searchParamters.PageSize = 10;
            searchParamters.StartPage = 1;
            searchParamters.AgeFrom = AgeFrom;
            searchParamters.AgeTo = AgeTo;
            searchParamters.HeightFrom = SelectedHeightFrom.Id;
            searchParamters.HeightTo = SelectedHeightTo.Id;
            searchParamters.MaritalStatus = SelectedMaritalStatus.Id;
            searchParamters.MotherTongue = SelectedMotherTongue.Id;
            searchParamters.Religion = SelectedReligion.Id;
            searchParamters.Caste = SelectedCaste.Id;
            searchParamters.Denomination = SelectedDenomination.Id;
            searchParamters.WithPhoto = WithPhoto == true ? 1 : 0;
            searchParamters.State = SelectedState.Id;
            searchParamters.DistrictRegion = District;
            searchParamters.Education = SelectedEducation.Id;
            searchParamters.Job = SelectedJob.Id;
            searchParamters.ResidingCountry = SelectedResidingCountry.Id;
            searchParamters.Community = "SELECT";

            var searchParams = new Dictionary<string, object> { { "SearchParameters", searchParamters } };

            await Shell.Current.GoToAsync("searchresults", searchParams);
        }

        private void OnReligionChanged(SfComboBox cmbReligion)
        {
            //var ComboSelectedLabel1 = $"Selected Index of the item is: {comboBox1.SelectedIndex}";
        }
    }
}

