using CommunityToolkit.Mvvm.ComponentModel;
using Matri.Business;
using Matri.Helper;
using Matri.Model;
using Newtonsoft.Json;

namespace Matri.ViewModel
{
    public class EditFamilyViewModel : ObservableObject
    {
        IServiceManager _serviceManager;
        //private readonly IUserDialogs _userDialogs;

        //public INC<Profile> LoggedInUser = new NC<Profile>();
        //public INC<string> AboutFather = new NC<string>();
        //public INC<string> AboutMother = new NC<string>();

        //public INC<int> NumberOfBrothers = new NC<int>();
        //public INC<string> AboutBrothers = new NC<string>();
        //public INC<int> NumberOfBrothersMarried = new NC<int>();

        //public INC<int> NumberOfSisters = new NC<int>();
        //public INC<string> AboutSisters = new NC<string>();
        //public INC<int> NumberOfSistersMarried = new NC<int>();

        //public INC<string> FamilyDetails = new NC<string>();
        public EditFamilyViewModel()
        {
            _serviceManager = ServiceHelper.GetService<IServiceManager>();
            //_userDialogs = userDialog;

            //MDFamilyStatuses = new SmartObservableCollection<Master>();
            //MDFamilyTypes = new SmartObservableCollection<Master>();
            //MDFamilyValues = new SmartObservableCollection<Master>();

            //var defaultMaster = new Master();
            //defaultMaster.Id = "SELECT";
            //defaultMaster.Name = "SELECT";

            //SelectedFamilyStatus = defaultMaster;
            //SelectedFamilyValue = defaultMaster;
            //SelectedFamilyType = defaultMaster;

        }

        //public override void Prepare(Profile profile)
        //{
            //LoggedInUser.Value = profile;

            //AboutFather.Value = profile.FathersOccupation;
            //AboutMother.Value = profile.MothersOccupation;
            //FamilyDetails.Value = profile.FamilyDetails;

            //NumberOfBrothers.Value = profile.Brothers;
            //AboutBrothers.Value = profile.AboutBrothers;
            //NumberOfBrothersMarried.Value = profile.BrothersMarried;

            //NumberOfSisters.Value = profile.Sisters;
            //AboutSisters.Value = profile.AboutSisters;
            //NumberOfSistersMarried.Value = profile.SistersMarried;
        //}

        public void CommandUpdate()
        {
            Task.Run(async () => { await UpdateAsync(); });
        }

        private async Task UpdateAsync()
        {
            //var familyDetails = new Profile
            //{
            //    FamilyDetails = FamilyDetails.Value,
            //    FathersOccupation = AboutFather.Value,
            //    MothersOccupation = AboutMother.Value,
            //    Brothers = NumberOfBrothers.Value,
            //    AboutBrothers = AboutBrothers.Value,
            //    BrothersMarried = NumberOfBrothersMarried.Value,
            //    Sisters = NumberOfSisters.Value,
            //    AboutSisters = AboutSisters.Value,
            //    SistersMarried=NumberOfSistersMarried.Value,
            //    FamilyStatus=SelectedFamilyStatus.Id,
            //    FamilyType=SelectedFamilyType.Id,
            //    FamilyValue=SelectedFamilyValue.Id
            //};
            //try
            //{
            //    var sessionToken = await SecureStorage.GetAsync("Token");
            //    var status = await _serviceManager.UpdateFamilyDetails(new Guid(sessionToken), familyDetails);

            //    if (status)
            //    {
            //        await _userDialogs.AlertAsync("Family Details Have Been Updated");
            //    }
            //}
            //catch (MatriInternetException exception)
            //{
            //    await _userDialogs.AlertAsync(exception.Message);
            //}
            //catch (Exception exception)
            //{
            //    var jsonResponse = exception.Message;
            //    var errorMessage = JsonConvert.DeserializeObject<JioMatriException>(jsonResponse);
            //    await _userDialogs.AlertAsync(errorMessage.Message);
            //}

        }

        //private SmartObservableCollection<Master> _MDFamilyTypes;
        //public SmartObservableCollection<Master> MDFamilyTypes
        //{
        //    get { return _MDFamilyTypes; }
        //    set
        //    {
        //        _MDFamilyTypes = value;
        //    }
        //}

        //private Master _selectedFamilyType;
        //public Master SelectedFamilyType
        //{
        //    get
        //    {
        //        return _selectedFamilyType;
        //    }
        //    set
        //    {
        //        _selectedFamilyType = value;
        //        RaisePropertyChanged("SelectedFamilyType");
        //    }
        //}

        //private SmartObservableCollection<Master> _MDFamilyStatuses;
        //public SmartObservableCollection<Master> MDFamilyStatuses
        //{
        //    get { return _MDFamilyStatuses; }
        //    set
        //    {
        //        _MDFamilyStatuses = value;
        //    }
        //}

        //private Master _selectedFamilyStatus;
        //public Master SelectedFamilyStatus
        //{
        //    get
        //    {
        //        return _selectedFamilyStatus;
        //    }
        //    set
        //    {
        //        _selectedFamilyStatus = value;
        //        RaisePropertyChanged("SelectedFamilyStatus");
        //    }
        //}

        //private SmartObservableCollection<Master> _MDFamilyValues;
        //public SmartObservableCollection<Master> MDFamilyValues
        //{
        //    get { return _MDFamilyValues; }
        //    set
        //    {
        //        _MDFamilyValues = value;
        //    }
        //}

        //private Master _selectedFamilyValue;
        //public Master SelectedFamilyValue
        //{
        //    get
        //    {
        //        return _selectedFamilyValue;
        //    }
        //    set
        //    {
        //        _selectedFamilyValue = value;
        //        RaisePropertyChanged("SelectedFamilyValue");
        //    }
        //}

        //public override async Task Initialize()
        //{
        //    await base.Initialize();
        //    var sessionToken = await SecureStorage.GetAsync("Token");
        //    try
        //    {
        //        var mdFamilyValues = await _serviceManager.GetMasterData(new Guid(sessionToken), "masterdata?type=familyvalue");
        //        MDFamilyValues.AddRange(mdFamilyValues);
        //        var userFamilyValue = mdFamilyValues.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.FamilyValue.ToLower()).FirstOrDefault();
        //        SelectedFamilyValue = userFamilyValue;

        //        var mdFamilyStatuses = await _serviceManager.GetMasterData(new Guid(sessionToken), "masterdata?type=familystatus");
        //        MDFamilyStatuses.AddRange(mdFamilyStatuses);
        //        var userFamilyStatus = mdFamilyStatuses.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.FamilyStatus.ToLower()).FirstOrDefault();
        //        SelectedFamilyStatus = userFamilyStatus;

        //        var mdFamilyTypes = await _serviceManager.GetMasterData(new Guid(sessionToken), "masterdata?type=familytype");
        //        MDFamilyTypes.AddRange(mdFamilyTypes);
        //        var userFamilyType = mdFamilyTypes.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.FamilyType.ToLower()).FirstOrDefault();
        //        SelectedFamilyType = userFamilyType;
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}
    }
}

