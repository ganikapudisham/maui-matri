using CommunityToolkit.Mvvm.ComponentModel;
using Matri.Business;
using Matri.CustomExceptions;
using Matri.Model;
using Newtonsoft.Json;

namespace Matri.ViewModels
{
    public class EditContactViewModel : ObservableObject
    {
        IServiceManager _serviceManager;
        //private readonly IUserDialogs _userDialogs;
        //public INC<Profile> LoggedInUser = new NC<Profile>();
        //public INC<string> District = new NC<string>();
        //public INC<string> Town = new NC<string>();
        //public INC<string> Nativity = new NC<string>();
        //public INC<string> MobileNumber = new NC<string>();
        //public INC<string> PostalAddress = new NC<string>();
        //public INC<string> EmailId = new NC<string>();
        public EditContactViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            //_userDialogs = userDialog;

            //MDCountries = new SmartObservableCollection<Master>();
            //MDStates = new SmartObservableCollection<Master>();

            //var defaultMaster = new Master();
            //defaultMaster.Id = "SELECT";
            //defaultMaster.Name = "SELECT";

            //SelectedCountryResidence = defaultMaster;
            //SelectedCountryNative = defaultMaster;
            //SelectedStateResidence = defaultMaster;
            //SelectedStateNative = defaultMaster;
        }

        //public override void Prepare(Profile profile)
        //{
            //LoggedInUser.Value = profile;

            //District.Value = profile.City;
            //Town.Value = profile.ResidingTown;
            //Nativity.Value = profile.Nativity;
            //MobileNumber.Value = profile.MobileNumber;
            //PostalAddress.Value = profile.Address;
            //EmailId.Value = profile.EmailId;
        //}

        //private SmartObservableCollection<Master> _MDCountries;
        //public SmartObservableCollection<Master> MDCountries
        //{
        //    get { return _MDCountries; }
        //    set
        //    {
        //        _MDCountries = value;
        //    }
        //}

        //private Master _selectedCountryResidence;
        //public Master SelectedCountryResidence
        //{
        //    get
        //    {
        //        return _selectedCountryResidence;
        //    }
        //    set
        //    {
        //        _selectedCountryResidence = value;
        //        RaisePropertyChanged("SelectedCountryResidence");
        //    }
        //}

        //private Master _selectedCountryNative;
        //public Master SelectedCountryNative
        //{
        //    get
        //    {
        //        return _selectedCountryNative;
        //    }
        //    set
        //    {
        //        _selectedCountryNative = value;
        //        RaisePropertyChanged("SelectedCountryNative");
        //    }
        //}

        //private SmartObservableCollection<Master> _MDStates;
        //public SmartObservableCollection<Master> MDStates
        //{
        //    get { return _MDStates; }
        //    set
        //    {
        //        _MDStates = value;
        //    }
        //}

        //private Master _selectedStateResidence;
        //public Master SelectedStateResidence
        //{
        //    get
        //    {
        //        return _selectedStateResidence;
        //    }
        //    set
        //    {
        //        _selectedStateResidence = value;
        //        RaisePropertyChanged("SelectedStateResidence");
        //    }
        //}

        //private Master _selectedStateNative;
        //public Master SelectedStateNative
        //{
        //    get
        //    {
        //        return _selectedStateNative;
        //    }
        //    set
        //    {
        //        _selectedStateNative = value;
        //        RaisePropertyChanged("SelectedStateNative");
        //    }
        //}

        //public override async Task Initialize()
        //{
            //await base.Initialize();
            //var sessionToken = await SecureStorage.GetAsync("Token");
            //try
            //{
            //    var mdCountries = await _serviceManager.GetMasterData(new Guid(sessionToken), "masterdata?type=countries");
            //    MDCountries.AddRange(mdCountries);
            //    var userCountryNative = mdCountries.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.Nationality.ToLower()).FirstOrDefault();
            //    SelectedCountryNative = userCountryNative;

            //    var userCountryResidence = mdCountries.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.Country.ToLower()).FirstOrDefault();
            //    SelectedCountryResidence = userCountryResidence;

            //    var mdStates = await _serviceManager.GetMasterData(new Guid(sessionToken), "masterdata?type=states");
            //    MDStates.AddRange(mdStates);
            //    var userStateNative = mdStates.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.State.ToLower()).FirstOrDefault();
            //    SelectedStateNative = userStateNative;

            //    var userStateResidence = mdStates.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.StateOfResidence.ToLower()).FirstOrDefault();
            //    SelectedStateResidence = userStateResidence;

            //}
            //catch (Exception e)
            //{

            //}
        //}

        public void CommandUpdate()
        {
            Task.Run(async () => { await UpdateAsync(); });
        }

        private async Task UpdateAsync()
        {
            //var contactDetails = new Profile
            //{
            //    Country = SelectedCountryResidence.Id,
            //    Nationality = SelectedCountryNative.Id,
            //    State = SelectedStateNative.Id,
            //    StateOfResidence = SelectedStateResidence.Id,
            //    City = District.Value,
            //    ResidingTown = Town.Value,
            //    MobileNumber = MobileNumber.Value,
            //    Address = PostalAddress.Value,
            //    Nativity = Nativity.Value,
            //    EmailId = EmailId.Value
            //};

            //try
            //{
            //    var sessionToken = await SecureStorage.GetAsync("Token");
            //    var status = await _serviceManager.UpdateContactDetails(new Guid(sessionToken), contactDetails);

            //    if (status)
            //    {
            //        await _userDialogs.AlertAsync("Contact Details Have Been Updated");
            //    }
            //}
            //catch (MatriInternetException exception)
            //{
            //    await _userDialogs.AlertAsync(exception.Message);
            //}
            //catch (Exception exception)
            //{
            //    var jsonResponse = exception.Message;
            //    var errorMessage = JsonConvert.DeserializeObject<MatriException>(jsonResponse);
            //    await _userDialogs.AlertAsync(errorMessage.Message);
            //}
        }

    }
}

