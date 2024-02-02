using CommunityToolkit.Mvvm.ComponentModel;
using Matri.Business;
using Matri.CustomExceptions;
using Matri.Helper;
using Matri.Model;
using Newtonsoft.Json;

namespace Matri.ViewModel
{
    public class EditAcademicsViewModel : ObservableObject
    {

        IServiceManager _serviceManager;
        //private readonly IUserDialogs _userDialogs;
        //public INC<Profile> LoggedInUser = new NC<Profile>();
        //public INC<string> AcademicDetails = new NC<string>();
        //public INC<string> EmployementDetails = new NC<string>();
        //public INC<string> IncomeDetails = new NC<string>();
        //public INC<string> PropertyDetails = new NC<string>();
        public EditAcademicsViewModel()
        {
            _serviceManager = ServiceHelper.GetService<IServiceManager>();

            //MDAcademics = new SmartObservableCollection<Master>();
            //MDIncomes = new SmartObservableCollection<Master>();

            //MDSectors = new SmartObservableCollection<Master>();
            //MDIndustries = new SmartObservableCollection<Master>();

            //var defaultMaster = new Master();
            //defaultMaster.Id = "SELECT";
            //defaultMaster.Name = "SELECT";

            //SelectedAcademic = defaultMaster;
            //SelectedIncome = defaultMaster;
            //SelectedSector = defaultMaster;
            //SelectedIndustry = defaultMaster;
        }

        //public override void Prepare(Profile profile)
        //{
            //LoggedInUser.Value = profile;

            //EmployementDetails.Value = profile.EmploymentDetails;
            //IncomeDetails.Value = profile.IncomeDetails;
            //PropertyDetails.Value = profile.PropertyDetails;
            //AcademicDetails.Value = profile.EducationDetails;

        //}

        //public override async Task Initialize()
        //{
            ////await base.Initialize();
            ////var sessionToken = await SecureStorage.GetAsync("Token");
            ////try
            ////{
            ////    var mdAcademics = await _serviceManager.GetMasterData(new Guid(sessionToken), "masterdata?type=education");
            ////    MDAcademics.AddRange(mdAcademics);
            ////    var userAcademic = mdAcademics.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.Education.ToLower()).FirstOrDefault();
            ////    SelectedAcademic = userAcademic;

            ////    var mdIncomes = await _serviceManager.GetMasterData(new Guid(sessionToken), "masterdata?type=incomes");
            ////    MDIncomes.AddRange(mdIncomes);
            ////    var userIncome = mdIncomes.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.AnnualIncome.ToLower()).FirstOrDefault();
            ////    SelectedIncome = userIncome;

            ////    var mdSectors = await _serviceManager.GetMasterData(new Guid(sessionToken), "masterdata?type=jobtypes");
            ////    MDSectors.AddRange(mdSectors);
            ////    var userSector = mdSectors.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.JobType.ToLower()).FirstOrDefault();
            ////    SelectedSector = userSector;

            ////    var mdIndustries = await _serviceManager.GetMasterData(new Guid(sessionToken), "masterdata?type=jobs");
            ////    MDIndustries.AddRange(mdIndustries);
            ////    var userIndustry = mdIndustries.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.Job.ToLower()).FirstOrDefault();
            ////    SelectedIndustry = userIndustry;
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
            //if (SelectedAcademic == null)
            //{
            //    await _userDialogs.AlertAsync("Please Select Highest Academic Qualification");
            //    return;
            //}

            //if (SelectedIncome == null)
            //{
            //    await _userDialogs.AlertAsync("Please Specify Income");
            //    return;
            //}

            //var sessionToken = await SecureStorage.GetAsync("Token");

            //var acedemicDetails = new Profile
            //{
            //    EmploymentDetails = EmployementDetails.Value,
            //    IncomeDetails = IncomeDetails.Value,
            //    PropertyDetails = PropertyDetails.Value,
            //    EducationDetails = AcademicDetails.Value,
            //    Education = SelectedAcademic.Id,
            //    AnnualIncome = SelectedIncome.Id,
            //    Job = SelectedIndustry.Id,
            //    JobType = SelectedSector.Id
            //};
            //try
            //{
            //    var status = await _serviceManager.UpdateEducationDetails(new Guid(sessionToken), acedemicDetails);

            //    if (status)
            //    {
            //        await _userDialogs.AlertAsync("Physical Details Have Been Updated");
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

        //private SmartObservableCollection<Master> _mdAcademics;
        //public SmartObservableCollection<Master> MDAcademics
        //{
        //    get { return _mdAcademics; }
        //    set
        //    {
        //        _mdAcademics = value;
        //    }
        //}

        //private Master _selectedAcademic;
        //public Master SelectedAcademic
        //{
        //    get
        //    {
        //        return _selectedAcademic;
        //    }
        //    set
        //    {
        //        _selectedAcademic = value;
        //        RaisePropertyChanged("SelectedAcademic");
        //    }
        //}

        //private SmartObservableCollection<Master> _mdIncomes;
        //public SmartObservableCollection<Master> MDIncomes
        //{
        //    get { return _mdIncomes; }
        //    set
        //    {
        //        _mdIncomes = value;
        //    }
        //}

        //private Master _selectedIncome;
        //public Master SelectedIncome
        //{
        //    get
        //    {
        //        return _selectedIncome;
        //    }
        //    set
        //    {
        //        _selectedIncome = value;
        //        RaisePropertyChanged("SelectedIncome");
        //    }
        //}

        //private SmartObservableCollection<Master> _mdSectors;
        //public SmartObservableCollection<Master> MDSectors
        //{
        //    get { return _mdSectors; }
        //    set
        //    {
        //        _mdSectors = value;
        //    }
        //}

        //private Master _selectedSector;
        //public Master SelectedSector
        //{
        //    get
        //    {
        //        return _selectedSector;
        //    }
        //    set
        //    {
        //        _selectedSector = value;
        //        RaisePropertyChanged("SelectedSector");
        //    }
        //}

        //private SmartObservableCollection<Master> _mdIndustries;
        //public SmartObservableCollection<Master> MDIndustries
        //{
        //    get { return _mdIndustries; }
        //    set
        //    {
        //        _mdIndustries = value;
        //    }
        //}

        //private Master _selectedIndustry;
        //public Master SelectedIndustry
        //{
        //    get
        //    {
        //        return _selectedIndustry;
        //    }
        //    set
        //    {
        //        _selectedIndustry = value;
        //        RaisePropertyChanged("SelectedIndustry");
        //    }
        //}
    }
}

