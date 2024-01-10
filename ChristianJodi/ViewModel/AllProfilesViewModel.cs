using ChristianJodi.Business;
using ChristianJodi.CustomExceptions;
using ChristianJodi.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;

namespace ChristianJodi.ViewModel
{
    public partial class AllProfilesViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        IServiceManager _serviceManager;

        public static int totalPages;
        public Guid LoggedInId = new Guid();

        public AllProfilesViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            PageSizeList.Add(new PageSize { Text = "10", Value = 10 });
            PageSizeList.Add(new PageSize { Text = "20", Value = 20 });
            PageSizeList.Add(new PageSize { Text = "50", Value = 50 });
            pPageSize = PageSizeList[0];
            Task.Run(() => this.GetProfiles(1, pPageSize.Value)).Wait();
            //SelectedProfile = Profiles[0];
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


        public ObservableRangeCollection<MiniProfile> Profiles { get; private set; } = new ObservableRangeCollection<MiniProfile>();

        public ObservableRangeCollection<MiniProfile> dbProfilesWithPaging { get; private set; } = new ObservableRangeCollection<MiniProfile>();

        
        [RelayCommand]
        public async Task Cancel()
        {
            await Shell.Current.GoToAsync("///LandingPage");
        }

        public async Task<bool> GetProfiles(int pageNumber, int pageSize)
        {
            var token = await SecureStorage.GetAsync("Token");
            try
            {
                Profiles.Clear();

                var dbProfilesWithPaging = await _serviceManager.GetProfiles(new Guid(token), pageNumber, pageSize);
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
            }
            catch (ChristianJodiInternetException exception)
            {
                //await _userDialogs.AlertAsync(exception.Message);
            }
            catch (Exception ex)
            {

            }
            return true;
        }

        [RelayCommand]
        public void First()
        {
            Task.Run(async () => { await FirstAsync(); });
        }

        [RelayCommand]
        public void Previous()
        {
            Task.Run(async () => { await PreviousAsync(); });
        }

        [RelayCommand]
        public void Next()
        {
            Task.Run(async () => { await NextAsync(); });
        }

        [RelayCommand]
        public void Last()
        {
            Task.Run(async () => { await LastAsync(); });
        }

        [RelayCommand]
        public void GoToPage()
        {
            Task.Run(async () => { await GoToPageAsync(); });
        }

        private async Task PreviousAsync()
        {
            int ePN = Convert.ToInt32(EPageNumber);
            if (ePN >= 2 && ePN <= totalPages)
            {
                ePN= ePN - 1;
                await GetProfiles(ePN, Convert.ToInt16(PPageSize.Value));
                EPageNumber = ePN.ToString();
                //SelectedProfile = Profiles[0];
            }
        }

        private async Task NextAsync()
        {
            int ePN = Convert.ToInt32(EPageNumber);
            if (ePN < totalPages)
            {
                ePN = ePN + 1;
                await GetProfiles(ePN, Convert.ToInt16(PPageSize.Value));
                EPageNumber = ePN.ToString();
                //SelectedProfile = Profiles[0];
            }
        }

        private async Task FirstAsync()
        {
            await GetProfiles(1, Convert.ToInt16(PPageSize.Value));
            EPageNumber = "1";
            //SelectedProfile = Profiles[0];
        }

        private async Task LastAsync()
        {
            int ePN = totalPages;
            await GetProfiles(ePN, Convert.ToInt16(PPageSize.Value));
            EPageNumber = ePN.ToString();
            //SelectedProfile = Profiles[0];
        }

        private async Task GoToPageAsync()
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

                var profileDetailsParams = new Dictionary<string, object> {  { "ProfileDetailsInput", profileDetailsInput } };

                await Shell.Current.GoToAsync("profiledetails", profileDetailsParams);
            }
        }
    }
}
