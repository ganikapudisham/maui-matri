using Matri.Business;
using Matri.Model;
using MvvmHelpers;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using Prism.Commands;

namespace Matri.ViewModel
{
    public partial class SearchAdvancedViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        IServiceManager _serviceManager;

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

        public int AgeFrom = 18;
        public int AgeTo = 100;

        public string District;
        public bool WithPhoto = false;

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
        private Master selectedSubCaste;

        [ObservableProperty]
        private Master selectedCommunity;

        [ObservableProperty]
        public bool showDenominations = false;

        private async Task InitialiseMasterData()
        {
            var token = await SecureStorage.GetAsync("Token");

            var masterDataMaritalStatus = await _serviceManager.GetMasterData(new Guid(token), "masterdata?type=marital");
            MDMaritalStatus.AddRange(masterDataMaritalStatus);

            var masterDataHeights = await _serviceManager.GetMasterData(new Guid(token), "masterdata?type=height");
            MDHeightsTo.AddRange(masterDataHeights);
            MDHeightsFrom.AddRange(masterDataHeights);

            var masterDataMotherTongues = await _serviceManager.GetMasterData(new Guid(token), "masterdata?type=languages");
            MDLanguages.AddRange(masterDataMotherTongues);

            var masterDataReligions = await _serviceManager.GetMasterData(new Guid(token), "masterdata?type=religion");
            MDReligions.AddRange(masterDataReligions);

            var masterDataStates = await _serviceManager.GetMasterData(new Guid(token), "masterdata?type=states");
            MDStates.AddRange(masterDataStates);

            var masterDataEducation = await _serviceManager.GetMasterData(new Guid(token), "masterdata?type=education");
            MDAcademics.AddRange(masterDataEducation);

            var masterDataJobs = await _serviceManager.GetMasterData(new Guid(token), "masterdata?type=jobs");
            MDJobs.AddRange(masterDataJobs);

            var masterDataCountries = await _serviceManager.GetMasterData(new Guid(token), "masterdata?type=countries");
            MDCountries.AddRange(masterDataCountries);

            //var castes = await _serviceManager.GetMasterData(new Guid(token), $"castes?religion=hindu");
            //MDCastes.AddRange(castes);

            //var denominations = await _serviceManager.GetMasterData(new Guid(token), $"castes?religion=christian");
            //MDDenominations.AddRange(denominations);
        }

        [RelayCommand]
        public async Task ReligionChanged(object selectedObject)
        {
            if (selectedObject != null && selectedObject is SearchAdvancedViewModel)
            {
                var item = (SearchAdvancedViewModel)selectedObject;

                var selectedReligion = item.SelectedReligion.Name;
                var token = await SecureStorage.GetAsync("Token");

                if (selectedReligion.ToLower() == "hindu")
                {
                    MDCastes.Clear();
                    var castes = await _serviceManager.GetMasterData(new Guid(token), $"castes?religion={selectedReligion}");
                    MDCastes.AddRange(castes);
                    ShowDenominations = false;
                }
                if (selectedReligion.ToLower() == "christian")
                {
                    MDDenominations.Clear();
                    var denominations = await _serviceManager.GetMasterData(new Guid(token), "masterdata?type=denomination");
                    MDDenominations.AddRange(denominations);
                    ShowDenominations = true;
                }

            }
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

            searchParamters.SubCaste = SelectedSubCaste.Id;
            searchParamters.Community = SelectedCommunity.Id;
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

            var searchParams = new Dictionary<string, object> { { "SearchParameters", searchParamters } };

            await Shell.Current.GoToAsync("searchresults", searchParams);
        }
    }
}

