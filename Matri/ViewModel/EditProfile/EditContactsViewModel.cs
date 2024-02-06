using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Matri.Abstract;
using Matri.Business;
using Matri.CustomExceptions;
using Matri.Helper;
using Matri.Model;
using MvvmHelpers;
using Newtonsoft.Json;
using System;

namespace Matri.ViewModel
{
    public partial class EditContactsViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        IServiceManager _serviceManager;
        ISharedService _sharedService;

        [ObservableProperty]
        public Profile profile;
        [ObservableProperty]
        public string district;
        [ObservableProperty]
        public string town;
        [ObservableProperty]
        public string nativity;
        [ObservableProperty]
        public string mobileNumber;
        [ObservableProperty]
        public string postalAddress;
        [ObservableProperty]
        public string emailId;

        [ObservableProperty]
        public bool isBusy;
        public EditContactsViewModel()
        {
            _serviceManager = ServiceHelper.GetService<IServiceManager>();
            _sharedService= ServiceHelper.GetService<ISharedService>();

            MDCountries = new ObservableRangeCollection<Master>();
            MDStates = new ObservableRangeCollection<Master>();

            var defaultMaster = new Master();
            defaultMaster.Id = "SELECT";
            defaultMaster.Name = "SELECT";

            SelectedCountryResidence = defaultMaster;
            SelectedCountryNative = defaultMaster;
            SelectedStateResidence = defaultMaster;
            SelectedStateNative = defaultMaster;

            Init();
        }

        private void Init()
        {
            IsBusy = true;
            Profile = _sharedService.GetValue<Profile>("LoggedInUser");
            var md = _sharedService.GetValue<MDD>("MasterData");

            District = Profile.City;
            Town = Profile.ResidingTown;
            Nativity = Profile.Nativity;
            MobileNumber = Profile.MobileNumber;
            PostalAddress = Profile.Address;
            EmailId = Profile.EmailId;

            MDCountries.AddRange(md.Countries);
            SelectedCountryNative = md.Countries.Where(mt => mt.Id.ToLower() == Profile.Nationality.ToLower()).FirstOrDefault();

            SelectedCountryResidence = md.Countries.Where(mt => mt.Id.ToLower() == Profile.Country.ToLower()).FirstOrDefault();

            MDStates.AddRange(md.IndianStates);
            SelectedStateNative = md.IndianStates.Where(mt => mt.Id.ToLower() == Profile.State.ToLower()).FirstOrDefault();

            SelectedStateResidence = md.IndianStates.Where(mt => mt.Id.ToLower() == Profile.StateOfResidence.ToLower()).FirstOrDefault();
        }



        private ObservableRangeCollection<Master> mDCountries;
        public ObservableRangeCollection<Master> MDCountries
        {
            get { return mDCountries; }
            set
            {
                mDCountries = value;
            }
        }

        private ObservableRangeCollection<Master> mDStates;
        public ObservableRangeCollection<Master> MDStates
        {
            get { return mDStates; }
            set
            {
                mDStates = value;
            }
        }

        [ObservableProperty]
        public Master selectedCountryResidence;

        [ObservableProperty]
        public Master selectedCountryNative;

        [ObservableProperty]
        public Master selectedStateResidence;
        
        [ObservableProperty]
        public Master selectedStateNative;

        [RelayCommand]
        public async Task Update()
        {
            Profile.Country = SelectedCountryResidence.Id;
            Profile.Nationality = SelectedCountryNative.Id;
            Profile.State = SelectedStateNative.Id;
            Profile.StateOfResidence = SelectedStateResidence.Id;
            Profile.City = District;
            Profile.ResidingTown = Town;
            Profile.MobileNumber = MobileNumber;
            Profile.Address = PostalAddress;
            Profile.Nativity = Nativity;
            Profile.EmailId = EmailId;

            try
            {
                var sessionToken = await SecureStorage.GetAsync("Token");
                var status = await _serviceManager.UpdateContactDetails(new Guid(sessionToken), Profile);

                if (status)
                {
                    await Shell.Current.CurrentPage.DisplayAlert("Alert", "Contact Details Have Been Updated", "OK");
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

