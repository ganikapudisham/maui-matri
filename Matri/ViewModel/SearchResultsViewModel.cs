using Matri.Business;
using CommunityToolkit.Mvvm.ComponentModel;
using Matri.FontsAwesome;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using Matri.Model;
using Matri.CustomExceptions;

namespace Matri.ViewModel
{
    public partial class SearchResultsViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject, IQueryAttributable
    {
        IServiceManager _serviceManager;

        public static int totalPages;
        public Guid LoggedInId = new Guid();
        public SearchResultsViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            PageSizeList.Add(new PageSize { Text = "10", Value = 10 });
            PageSizeList.Add(new PageSize { Text = "20", Value = 20 });
            PageSizeList.Add(new PageSize { Text = "50", Value = 50 });
            pPageSize = PageSizeList[0];
            //Task.Run(() => this.GetProfiles(1, pPageSize.Value)).Wait();
        }

        public ObservableRangeCollection<PageSize> PageSizeList { get; private set; } = new ObservableRangeCollection<PageSize>();

        [ObservableProperty]
        public string ePageNumber;

        [ObservableProperty]
        public PageSize pPageSize;

        [ObservableProperty]
        public string lNumberOfPages;

        [ObservableProperty]
        public MiniProfile selectedProfile;

        [ObservableProperty]
        public SearchParameters searchParametersF;

        [ObservableProperty]
        public bool showRecordsSection;
        [ObservableProperty]
        public bool showRecordsNotFoundSection;
        [ObservableProperty]
        public bool showPagingSection;

        public ObservableRangeCollection<MiniProfile> Profiles { get; private set; } = new ObservableRangeCollection<MiniProfile>();

        [RelayCommand]
        public async Task Cancel()
        {
            await Shell.Current.GoToAsync("///LandingPage");
        }

        [RelayCommand]
        private async Task Previous()
        {
            int ePN = Convert.ToInt32(EPageNumber);
            if (ePN >= 2 && ePN <= totalPages)
            {
                ePN = ePN - 1;
                await GetProfiles(ePN, Convert.ToInt16(PPageSize.Value));
                EPageNumber = ePN.ToString();
            }
        }

        [RelayCommand]
        private async Task Next()
        {
            int ePN = Convert.ToInt32(EPageNumber);
            if (ePN < totalPages)
            {
                ePN = ePN + 1;
                await GetProfiles(ePN, Convert.ToInt16(PPageSize.Value));
                EPageNumber = ePN.ToString();
            }
        }

        [RelayCommand]
        private async Task First()
        {
            await GetProfiles(1, Convert.ToInt16(PPageSize.Value));
            EPageNumber = "1";
        }

        [RelayCommand]
        private async Task Last()
        {
            int ePN = totalPages;
            await GetProfiles(ePN, Convert.ToInt16(PPageSize.Value));
            EPageNumber = ePN.ToString();
        }

        [RelayCommand]
        private async Task GoToPage()
        {
            int ePN = Convert.ToInt32(EPageNumber);
            if (ePN != 0 && ePN <= totalPages)
            {
                await GetProfiles(ePN, Convert.ToInt16(PPageSize.Value));
                EPageNumber = ePN.ToString();
            }
        }

        [RelayCommand]
        private async Task SelectedPageSize(Object obj)
        {
            if (obj != null && obj is AllProfilesViewModel)
            {
                var item = (AllProfilesViewModel)obj;

                int index = PageSizeList.ToList().FindIndex(a => a.Value == item.PPageSize.Value);
                PPageSize = PageSizeList[index];

                await GetProfiles(Convert.ToInt32(EPageNumber), Convert.ToInt16(item.PPageSize.Value));
            }
        }

        [RelayCommand]
        public async Task ViewProfile(Object obj)
        {

            if (obj != null && obj is MiniProfile)
            {
                var item = (MiniProfile)obj;

                var sessionToken = await SecureStorage.GetAsync("Token");
                var targetProfileId = item.Id;

                //log the current user as visitor for the tapped profile
                await _serviceManager.CreateProfileVisitor(new Guid(sessionToken), targetProfileId);

                var profileDetailsInput = new ProfileDetailsInput();
                profileDetailsInput.LoggedInId = new Guid(sessionToken);
                profileDetailsInput.TargetProfileId = targetProfileId;

                var profileDetailsParams = new Dictionary<string, object> { { "ProfileDetailsInput", profileDetailsInput } };

                await Shell.Current.GoToAsync("profiledetails", profileDetailsParams);
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            SearchParametersF = query[nameof(SearchParameters)] as SearchParameters;
            Task.Run(() => this.GetProfiles(1,10)).Wait();
        }

        public async Task GetProfiles(int pageNumber, int pageSize)
        {
            var token = await SecureStorage.GetAsync("Token");
            SearchParametersF.StartPage = pageNumber;
            SearchParametersF.PageSize = pageSize;
            try
            {
                Profiles.Clear();

                var dbProfilesWithPaging = await _serviceManager.Search(new Guid(token), SearchParametersF);
                var dbProfiles = dbProfilesWithPaging.Data;
                var modValue = dbProfilesWithPaging.MetaData.TotalRecords % pageSize;

                totalPages = dbProfilesWithPaging.MetaData.TotalRecords / pageSize;
                if (modValue != 0)
                {
                    totalPages += 1;
                }
                LNumberOfPages = $"/{totalPages}";
                EPageNumber = pageNumber.ToString();

                Profiles.AddRange(dbProfiles);

                if (dbProfilesWithPaging.MetaData.TotalRecords == 0)
                {
                    ShowRecordsNotFoundSection = true;
                    ShowRecordsSection = false;
                    ShowPagingSection = false;
                }
                else
                {
                    ShowRecordsNotFoundSection = false;
                    ShowRecordsSection = true;
                    ShowPagingSection = true;
                }
            }
            catch (MatriInternetException exception)
            {
               // await _userDialogs.AlertAsync(exception.Message);
            }
            catch (Exception ex)
            {

            }
        }
    }
}

