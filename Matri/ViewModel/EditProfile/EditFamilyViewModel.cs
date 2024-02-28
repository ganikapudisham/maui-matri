using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Matri.Abstract;
using Matri.Business;
using Matri.CustomExceptions;
using Matri.Helper;
using Matri.Model;
using MvvmHelpers;
using Newtonsoft.Json;

namespace Matri.ViewModel
{
    public partial class EditFamilyViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        private readonly IServiceManager _serviceManager;
        private readonly ISharedService _sharedService;

        [ObservableProperty]
        public Profile profile;
        [ObservableProperty]
        public string aboutFather;
        [ObservableProperty]
        public string aboutMother;

        [ObservableProperty]
        public int numberOfBrothers;
        [ObservableProperty]
        public string aboutBrothers;
        [ObservableProperty]
        public int numberOfBrothersMarried;
        [ObservableProperty]
        public int numberOfSisters;
        [ObservableProperty]
        public string aboutSisters;
        [ObservableProperty]
        public int numberOfSistersMarried;
        [ObservableProperty]
        public string familyDetails;

        //[ObservableProperty]
        //public Master selectedFamilyType;
        [ObservableProperty]
        public Master selectedFamilyStatus;
        [ObservableProperty]
        public Master selectedFamilyValue;
        [ObservableProperty]
        public bool isBusy = true;

        //private ObservableRangeCollection<Master> mDFamilyTypes;
        //public ObservableRangeCollection<Master> MDFamilyTypes
        //{
        //    get { return mDFamilyTypes; }
        //    set
        //    {
        //        mDFamilyTypes = value;
        //    }
        //}

        private ObservableRangeCollection<Master> mDFamilyStatuses;
        public ObservableRangeCollection<Master> MDFamilyStatuses
        {
            get { return mDFamilyStatuses; }
            set
            {
                mDFamilyStatuses = value;
            }
        }

        private ObservableRangeCollection<Master> mDFamilyValues;
        public ObservableRangeCollection<Master> MDFamilyValues
        {
            get { return mDFamilyValues; }
            set
            {
                mDFamilyValues = value;
            }
        }


        public EditFamilyViewModel()
        {
            _serviceManager = ServiceHelper.GetService<IServiceManager>();
            _sharedService = ServiceHelper.GetService<ISharedService>();

            MDFamilyStatuses = new ObservableRangeCollection<Master>();
           // MDFamilyTypes = new ObservableRangeCollection<Master>();
            MDFamilyValues = new ObservableRangeCollection<Master>();

            var defaultMaster = new Master();
            defaultMaster.Id = "SELECT";
            defaultMaster.Name = "SELECT";

            SelectedFamilyStatus = defaultMaster;
            SelectedFamilyValue = defaultMaster;
            //SelectedFamilyType = defaultMaster;

            Init();
        }

        public void Init()
        {
            IsBusy = true;
            Profile = _sharedService.GetValue<Profile>("LoggedInUser");

            AboutFather = Profile.FathersOccupation;
            AboutMother = Profile.MothersOccupation;
            FamilyDetails = Profile.FamilyDetails;

            NumberOfBrothers = Profile.Brothers;
            AboutBrothers = Profile.AboutBrothers;
            NumberOfBrothersMarried = Profile.BrothersMarried;

            NumberOfSisters = Profile.Sisters;
            AboutSisters = Profile.AboutSisters;
            NumberOfSistersMarried = Profile.SistersMarried;

            var md = _sharedService.GetValue<MDD>("MasterData");

            MDFamilyValues.AddRange(md.FamilyValues);
            SelectedFamilyValue = md.FamilyValues.Where(mt => mt.Id.ToLower() == Profile.FamilyValue.ToLower()).FirstOrDefault();

            MDFamilyStatuses.AddRange(md.FamilyStatuses);
            SelectedFamilyStatus = md.FamilyStatuses.Where(mt => mt.Id.ToLower() == Profile.FamilyStatus.ToLower()).FirstOrDefault();

            //MDFamilyTypes.AddRange(md.FamilyTypes);
            //SelectedFamilyType = md.FamilyTypes.Where(mt => mt.Id.ToLower() == Profile.FamilyType.ToLower()).FirstOrDefault();
            IsBusy = false;
        }


        [RelayCommand]
        private async Task Update()
        {
            Profile.FamilyDetails = FamilyDetails;
            Profile.FathersOccupation = AboutFather;
            Profile.MothersOccupation = AboutMother;
            Profile.Brothers = NumberOfBrothers;
            Profile.AboutBrothers = AboutBrothers;
            Profile.BrothersMarried = NumberOfBrothersMarried;
            Profile.Sisters = NumberOfSisters;
            Profile.AboutSisters = AboutSisters;
            Profile.SistersMarried = NumberOfSistersMarried;
            Profile.FamilyStatus = SelectedFamilyStatus.Id;
            //Profile.FamilyType = SelectedFamilyType.Id;
            Profile.FamilyValue = SelectedFamilyValue.Id;

            try
            {
                var sessionToken = await SecureStorage.GetAsync("Token");
                var status = await _serviceManager.UpdateFamilyDetails(new Guid(sessionToken), Profile);

                if (status)
                {
                    await Shell.Current.CurrentPage.DisplayAlert("Alert", "Family Details Have Been Updated", "OK");
                }
                else
                {
                    await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please try again", "OK");
                }
            }
            catch (MatriInternetException exception)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", exception.Message, "OK");
            }
            catch (Exception exception)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", exception.Message, "OK");
            }
        }
    }
}

