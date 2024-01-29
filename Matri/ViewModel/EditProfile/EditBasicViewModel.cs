using CommunityToolkit.Mvvm.ComponentModel;
using Matri.Business;
using Matri.CustomExceptions;
using Matri.Model;
using Newtonsoft.Json;

namespace Matri.ViewModel
{
    public class EditBasicViewModel : ObservableObject
    {
        IServiceManager _serviceManager;
        //private readonly IUserDialogs _userDialogs;

        //public INC<Profile> LoggedInUser = new NC<Profile>();
        //public INC<string> BirthPlace = new NC<string>();
        //public INC<string> BirthTime = new NC<string>();
        //public INC<string> AboutMe = new NC<string>();
        //public INC<string> FirstName = new NC<string>();
        //public INC<string> LastName = new NC<string>();
        //public INC<bool> ShowHinduFields = new NC<bool>(false);

        //private Master selectedMotherTongue;
        //public Master SelectedMotherTongue
        //{
        //    get
        //    {
        //        return selectedMotherTongue;
        //    }
        //    set
        //    {
        //        selectedMotherTongue = value;
        //        RaisePropertyChanged("SelectedMotherTongue");
        //    }
        //}

        //private SmartObservableCollection<Master> mdLanguages;
        //public SmartObservableCollection<Master> MDLanguages
        //{
        //    get { return mdLanguages; }
        //    set
        //    {
        //        mdLanguages = value;
        //    }
        //}

        //private SmartObservableCollection<Master> mdMaritalStatus;
        //public SmartObservableCollection<Master> MDMaritalStatus
        //{
        //    get { return mdMaritalStatus; }
        //    set
        //    {
        //        mdMaritalStatus = value;
        //    }
        //}

        //private Master selectedMaritalStatus;
        //public Master SelectedMaritalStatus
        //{
        //    get
        //    {
        //        return selectedMaritalStatus;
        //    }
        //    set
        //    {
        //        selectedMaritalStatus = value;
        //        RaisePropertyChanged("SelectedMaritalStatus");
        //    }
        //}


        public EditBasicViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            //_userDialogs = userDialog;
            //MDLanguages = new SmartObservableCollection<Master>();
            //MDMaritalStatus = new SmartObservableCollection<Master>();

            //var defaultMaster = new Master();
            //defaultMaster.Id = "SELECT";
            //defaultMaster.Name = "SELECT";

            //SelectedMotherTongue = defaultMaster;
            //SelectedMaritalStatus = defaultMaster;

            //BDDates = new SmartObservableCollection<Master>();
            //BDYears = new SmartObservableCollection<Master>();
            //BDMonths = new SmartObservableCollection<Master>();
        }

        //public override void Prepare(Profile profile)
        //{
            //LoggedInUser.Value = profile;
            //FirstName.Value = profile.FirstName;
            //LastName.Value = profile.LastName;
            //BirthTime.Value = profile.BirthTime;
            //BirthPlace.Value = profile.BirthPlace;
            //AboutMe.Value = profile.AboutMe;


            //for (var i = 1; i < 32; i++)
            //{
            //    BDDates.Add(new Master { Id = i.ToString(), Name = i.ToString() });
            //}
            //var currentYear = DateTime.Now.Year;
            //var UpperLimit = currentYear - 19; // 18 years back
            //var LowerLimit = UpperLimit - 70;
            //for (var i = UpperLimit; i > LowerLimit; i--)
            //{
            //    BDYears.Add(new Master { Id = i.ToString(), Name = i.ToString() });
            //}

            //var months = new List<string>();
            //months.Add("Jan");
            //months.Add("Feb");
            //months.Add("Mar");
            //months.Add("Apr");
            //months.Add("May");
            //months.Add("Jun");
            //months.Add("Jul");
            //months.Add("Aug");
            //months.Add("Sep");
            //months.Add("Oct");
            //months.Add("Nov");
            //months.Add("Dec");

            //for (int i = 0; i < 12; i++)
            //{
            //    var id = i + 1;
            //    BDMonths.Add(new Master { Id = id.ToString(), Name = months[i] });
            //}

            //var birthDate = profile.BirthDate.Split('-');
            //selectedBirthDate = BDDates.Where(bd => bd.Name == birthDate[2]).FirstOrDefault();
            //SelectedBirthMonth = BDMonths.Where(bd => bd.Name == birthDate[1]).FirstOrDefault();
            //SelectedBirthYear = bdYears.Where(bd => bd.Name == birthDate[0]).FirstOrDefault();
        //}

        //public override async Task Initialize()
        //{
            //await base.Initialize();
            //var sessionToken = await SecureStorage.GetAsync("Token");
            //var showHinduFields = await SecureStorage.GetAsync("ShowHinduFields");

            //ShowHinduFields.Value = Convert.ToBoolean(showHinduFields);

            //try
            //{
            //    var masterDataMotherTongues = await _serviceManager.GetMasterData(new Guid(sessionToken), "masterdata?type=languages");
            //    MDLanguages.AddRange(masterDataMotherTongues);

            //    var userMotherTongue = masterDataMotherTongues.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.MotherTongue.ToLower()).FirstOrDefault();
            //    SelectedMotherTongue = userMotherTongue;

            //    var masterDataMaritalStatus = await _serviceManager.GetMasterData(new Guid(sessionToken), "masterdata?type=marital");
            //    MDMaritalStatus.AddRange(masterDataMaritalStatus);

            //    var userMaritalStatus = masterDataMaritalStatus.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.MaritalStatus.ToLower()).FirstOrDefault();
            //    SelectedMaritalStatus = userMaritalStatus;
            //}
            //catch (Exception e)
            //{
            //}
        //}

        //private SmartObservableCollection<Master> bdDates;
        //public SmartObservableCollection<Master> BDDates
        //{
        //    get { return bdDates; }
        //    set
        //    {
        //        bdDates = value;
        //    }
        //}

        //private SmartObservableCollection<Master> bdMonths;
        //public SmartObservableCollection<Master> BDMonths
        //{
        //    get { return bdMonths; }
        //    set
        //    {
        //        bdMonths = value;
        //    }
        //}

        //private SmartObservableCollection<Master> bdYears;
        //public SmartObservableCollection<Master> BDYears
        //{
        //    get { return bdYears; }
        //    set
        //    {
        //        bdYears = value;
        //    }
        //}

        //private Master selectedBirthDate;
        //public Master SelectedBirthDate
        //{
        //    get
        //    {
        //        return selectedBirthDate;
        //    }
        //    set
        //    {
        //        selectedBirthDate = value;
        //        RaisePropertyChanged("SelectedBirthDate");
        //    }
        //}

        //private Master selectedBirthMonth;
        //public Master SelectedBirthMonth
        //{
        //    get
        //    {
        //        return selectedBirthMonth;
        //    }
        //    set
        //    {
        //        selectedBirthMonth = value;
        //        RaisePropertyChanged("SelectedBirthMonth");
        //    }
        //}

        //private Master selectedBirthYear;
        //public Master SelectedBirthYear
        //{
        //    get
        //    {
        //        return selectedBirthYear;
        //    }
        //    set
        //    {
        //        selectedBirthYear = value;
        //        RaisePropertyChanged("SelectedBirthYear");
        //    }
        //}

        public void CommandUpdate()
        {
            Task.Run(async () => { await UpdateAsync(); });
        }

        private async Task UpdateAsync()
        {
            //if (string.IsNullOrWhiteSpace(FirstName.Value))
            //{
            //    await _userDialogs.AlertAsync("Please Provide First Name");
            //    return;
            //}

            //if (string.IsNullOrWhiteSpace(LastName.Value))
            //{
            //    await _userDialogs.AlertAsync("Please Provide Last Name");
            //    return;
            //}

            //if (selectedMaritalStatus==null)
            //{
            //    await _userDialogs.AlertAsync("Please Provide Marital Status");
            //    return;
            //}

            //if (SelectedMotherTongue == null)
            //{
            //    await _userDialogs.AlertAsync("Please Specify Mother Tongue");
            //    return;
            //}

            //var firstName = FirstName.Value;
            //var lastName = LastName.Value;
            //var aboutMe = AboutMe.Value;
            //var birthPlace = BirthPlace.Value;
            //var birthTime = BirthTime.Value;

            //var year = Convert.ToInt16(selectedBirthYear.Name);
            //var month = Convert.ToInt16(selectedBirthMonth.Id);
            //var date = Convert.ToInt16(selectedBirthDate.Name);

            //var BirthDateInApiFormat = $"{year}-{month:00}-{date:00}";

            //var sessionToken = await SecureStorage.GetAsync("Token");

            //var basicDetails = new Profile { FirstName = firstName, LastName = lastName, AboutMe = aboutMe, 
            //    BirthTime = birthTime, BirthPlace = birthPlace,MaritalStatus=SelectedMaritalStatus.Id, 
            //    MotherTongue=SelectedMotherTongue.Id, BirthDate= BirthDateInApiFormat};
            //try
            //{
            //    var status = await _serviceManager.UpdateBasicDetails(new Guid(sessionToken), basicDetails);

            //    if (status)
            //    {
            //        await _userDialogs.AlertAsync("Basic Details Have Been Updated");
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

