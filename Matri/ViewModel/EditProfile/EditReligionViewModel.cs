using Prism.Commands;
using Newtonsoft.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using Matri.Business;
using Matri.Model;
namespace Matri.ViewModel
{
    public class EditReligionViewModel : ObservableObject
    {
        //public INC<bool> ShowDenominations = new NC<bool>(false);
        readonly IServiceManager _serviceManager;
        //private readonly IUserDialogs _userDialogs;
        //public INC<Profile> LoggedInUser = new NC<Profile>();

        //public INC<string> Star = new NC<string>();
        //public INC<string> Raasi = new NC<string>();
        //public INC<string> Gothram = new NC<string>();
        //public INC<string> ChevvaiDosham = new NC<string>();
        //public INC<string> CasteDetails = new NC<string>();
        //public INC<bool> CasteIsImportant = new NC<bool>();
        //public INC<bool> DenominationIsImportant = new NC<bool>();
        //public INC<bool> ShowHinduFields = new NC<bool>(false);

        //public DelegateCommand<object> SelectionChangedCommand { get; set; }
        public EditReligionViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            //_userDialogs = userDialog;

            //MDReligions = new SmartObservableCollection<Master>();
            //MDCastes = new SmartObservableCollection<Master>();
            //MDDenominations = new SmartObservableCollection<Master>();

            //SelectionChangedCommand = new DelegateCommand<object>(CmdReligionChanged);

            //var defaultMaster = new Master();
            //defaultMaster.Id = "SELECT";
            //defaultMaster.Name = "SELECT";

            //SelectedCommunity = defaultMaster;
            //SelectedSubCaste = defaultMaster;
            //SelectedReligion = defaultMaster;
            //SelectedCaste = defaultMaster;
            //SelectedDenomination = defaultMaster;
        }

        //public override void Prepare(Profile profile)
        //{
            //LoggedInUser.Value = profile;
            //Star.Value = profile.Star;
            //Raasi.Value = profile.Raasi;
            //Gothram.Value = profile.Gothram;
            //ChevvaiDosham.Value = profile.ChevvaiDosham;
            //CasteDetails.Value = profile.CasteInformation;
            //CasteIsImportant.Value = profile.CasteCriteria;
            //DenominationIsImportant.Value = profile.DenominationCriteria;
        //}

        //public override async Task Initialize()
        //{
            //await base.Initialize();

            //try
            //{
            //    var token = await SecureStorage.GetAsync("Token");
            //    var showHinduFields = await SecureStorage.GetAsync("ShowHinduFields");

            //    ShowHinduFields.Value = Convert.ToBoolean(showHinduFields);

            //    var masterDataReligions = await _serviceManager.GetMasterData(new Guid(token), "masterdata?type=religion");
            //    MDReligions.AddRange(masterDataReligions);

            //    var userReligion = masterDataReligions.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.Religion.ToLower()).FirstOrDefault();
            //    SelectedReligion = userReligion;

            //    MDCastes.Clear();
            //    var castes = await _serviceManager.GetMasterData(new Guid(token), $"castes?religion=hindu");
            //    MDCastes.AddRange(castes);
            //    var userCaste = castes.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.Caste.ToLower()).FirstOrDefault();
            //    SelectedCaste = userCaste;

            //    if (userReligion.Name.ToLower() == "hindu")
            //    {
            //        ShowDenominations.Value = false;
            //    }
            //    if (userReligion.Name.ToLower() == "christian")
            //    {
            //        MDDenominations.Clear();
            //        var denominations = await _serviceManager.GetMasterData(new Guid(token), "masterdata?type=denomination");
            //        MDDenominations.AddRange(denominations);

            //        var userDenomination = denominations.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.Denomination.ToLower()).FirstOrDefault();
            //        SelectedDenomination = userDenomination;
            //        ShowDenominations.Value = true;
            //    }

            //}
            //catch (Exception e)
            //{
            //    //logger.Debug(e.Message);
            //}
        //}

        public void CmdReligionChanged(object selectedObject)
        {
            //ShowCastes.Value = true;
            //ShowDenominations.Value = false;
            Task.Run(async () => { await CmdReligionChangedAsync(selectedObject); });
        }

        private async Task CmdReligionChangedAsync(object selectedObject)
        {
            //var selectedItem = selectedObject as Syncfusion.XForms.ComboBox.SelectionChangedEventArgs;
            //var religion = (selectedItem.Value as Master).Id;
            //var token = await SecureStorage.GetAsync("Token");

            //if (religion.ToLower() == "hindu")
            //{
            //    var castes = await _serviceManager.GetMasterData(new Guid(token), $"castes?religion={religion}");
            //    Device.BeginInvokeOnMainThread(() =>
            //    {
            //        MDCastes.Clear();
            //        MDCastes.AddRange(castes);
            //        var userCaste = castes.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.Caste.ToLower()).FirstOrDefault();
            //        SelectedCaste = userCaste;
            //        ShowDenominations.Value = false;
            //    });
            //    //ShowCastes.Value = true;
            //}
            //if (religion.ToLower() == "christian")
            //{
            //    var denominations = await _serviceManager.GetMasterData(new Guid(token), "masterdata?type=denomination");
            //    Device.BeginInvokeOnMainThread(() =>
            //    {
            //        MDDenominations.Clear();
            //        MDDenominations.AddRange(denominations);
            //        var userDenomination = denominations.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.Denomination.ToLower()).FirstOrDefault();
            //        SelectedDenomination = userDenomination;
            //        ShowDenominations.Value = true;
            //        //ShowCastes.Value = false;
            //    });
            //}
        }

        //private SmartObservableCollection<Master> mdReligions;
        //public SmartObservableCollection<Master> MDReligions
        //{
        //    get { return mdReligions; }
        //    set
        //    {
        //        mdReligions = value;
        //    }
        //}

        //private SmartObservableCollection<Master> mdCastes;
        //public SmartObservableCollection<Master> MDCastes
        //{
        //    get { return mdCastes; }
        //    set
        //    {
        //        mdCastes = value;
        //    }
        //}

        //private SmartObservableCollection<Master> mdDenominations;
        //public SmartObservableCollection<Master> MDDenominations
        //{
        //    get { return mdDenominations; }
        //    set
        //    {
        //        mdDenominations = value;
        //    }
        //}

        //private Master selectedCommunity;
        //public Master SelectedCommunity
        //{
        //    get
        //    {
        //        return selectedCommunity;
        //    }
        //    set
        //    {
        //        selectedCommunity = value;
        //        RaisePropertyChanged("SelectedCommunity");
        //    }
        //}

        //private Master selectedSubCaste;
        //public Master SelectedSubCaste
        //{
        //    get
        //    {
        //        return selectedSubCaste;
        //    }
        //    set
        //    {
        //        selectedSubCaste = value;
        //        RaisePropertyChanged("SelectedSubCaste");
        //    }
        //}

        //private Master selectedDenomination;
        //public Master SelectedDenomination
        //{
        //    get
        //    {
        //        return selectedDenomination;
        //    }
        //    set
        //    {
        //        selectedDenomination = value;
        //        RaisePropertyChanged("SelectedDenomination");
        //    }
        //}

        //private Master selectedCaste;
        //public Master SelectedCaste
        //{
        //    get
        //    {
        //        return selectedCaste;
        //    }
        //    set
        //    {
        //        selectedCaste = value;
        //        RaisePropertyChanged("SelectedCaste");
        //    }
        //}

        //private Master selectedReligion;
        //public Master SelectedReligion
        //{
        //    get
        //    {
        //        return selectedReligion;
        //    }
        //    set
        //    {
        //        selectedReligion = value;
        //        RaisePropertyChanged("SelectedReligion");
        //    }
        //}

        //public void CommandUpdate()
        //{
        //    Task.Run(async () => { await UpdateAsync(); });
        //}

        private async Task UpdateAsync()
        {
            //if (selectedReligion == null)
            //{
            //    await _userDialogs.AlertAsync("Please Specify Religion");
            //    return;
            //}

            //if (selectedCaste == null)
            //{
            //    await _userDialogs.AlertAsync("Please Specify Caste");
            //    return;
            //}

            //var sessionToken = await SecureStorage.GetAsync("Token");

            //var regligionDetails = new Profile
            //{
            //    Religion = SelectedReligion.Id,
            //    Denomination = SelectedDenomination.Id,
            //    Caste = SelectedCaste.Id,
            //    SubCaste = SelectedSubCaste.Id,
            //    Community = SelectedCommunity.Id,
            //    Star = Star.Value,
            //    Raasi = Raasi.Value,
            //    Gothram = Gothram.Value,
            //    ChevvaiDosham = ChevvaiDosham.Value,
            //    CasteInformation = CasteDetails.Value,
            //    CasteCriteria = CasteIsImportant.Value,
            //    DenominationCriteria = DenominationIsImportant.Value
            //};
            //try
            //{
            //    var status = await _serviceManager.UpdateReligionDetails(new Guid(sessionToken), regligionDetails);

            //    if (status)
            //    {
            //        await _userDialogs.AlertAsync("Religious Details Have Been Updated");
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
    }
}

